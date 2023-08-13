import logging
import httpx
import random
import pybreaker
from fastapi import HTTPException
from tenacity import (
    before_sleep_log,
    retry,
    retry_if_exception_type,
    stop_after_attempt,
)
from tenacity.wait import wait_combine, wait_fixed
from app.logger import logger
from config import THIRD_PARTY_BASE_URL, API_KEY

HEADERS = {"X-API-Key": API_KEY}

MAX_RETRIES = 3
BASE_WAIT_TIME = 2

breaker = pybreaker.CircuitBreaker(fail_max=3, reset_timeout=20)


def wait_decorrelated_jitter_exp(
    base_delay=1, max_delay=10, max_tries=5, exponent_base=2
):
    """
    Decorrelated jitter backoff strategy
    """

    def decorrelated_jitter_exp(retry_state):  # added the retry_state parameter
        previous_attempt_number = (
            retry_state.attempt_number
        )  # get attempt number from retry_state
        exp_delay = base_delay * (exponent_base**previous_attempt_number)
        wait_time = random.uniform(base_delay, min(exp_delay, max_delay))
        return wait_time

    return wait_combine(wait_fixed(base_delay), decorrelated_jitter_exp)


@breaker
@retry(
    wait=wait_decorrelated_jitter_exp(max_tries=MAX_RETRIES),
    retry=retry_if_exception_type(httpx.HTTPError),
    stop=stop_after_attempt(MAX_RETRIES),
    before_sleep=before_sleep_log(logger, logging.DEBUG),
    reraise=True,
)
async def make_request(url, method="GET", headers=None, params=None):
    async with httpx.AsyncClient() as client:
        if method == "GET":
            response = await client.get(url, headers=headers, params=params)
        return response.json()


async def parse_and_raise_error(response):
    """Parse the error response and raise an HTTPException."""
    try:
        error_data = response.json()
        title = error_data.get("title", "Unknown error")
        status = error_data.get("status", response.status_code)
        detail = error_data.get("detail", "No detail provided")

        logger.error("Error from 3rd party API. Title: %s, Detail: %s", title, detail)

        raise HTTPException(status_code=status, detail=detail)

    except ValueError as exc:
        raise HTTPException(
            status_code=response.status_code,
            detail="Unexpected error from 3rd party API.",
        ) from exc


async def get_scoreboard(league, since, until):
    """
    Retrieves the scoreboard for a given league within a specified time range.

    Args:
        league (League): The league for which to retrieve the scoreboard.
        since (str): The start date of the time range in ISO format (YYYY-MM-DD).
        until (str): The end date of the time range in ISO format (YYYY-MM-DD).

    Returns:
        dict: The scoreboard data in JSON format.
    """
    endpoint = f"{THIRD_PARTY_BASE_URL}/{league.value}/scoreboard"
    params = {"since": since, "until": until}
    response = await make_request(endpoint, headers=HEADERS, params=params)
    if response.status_code >= 400:
        await parse_and_raise_error(response)
    return response.json()


async def get_team_rankings(league):
    """
    Retrieves the team rankings for a given league.

    Args:
        league (League): The league for which to retrieve team rankings.

    Returns:
        A JSON object containing the team rankings data.
    """
    endpoint = f"{THIRD_PARTY_BASE_URL}/{league.value}/team-rankings"
    response = await make_request(endpoint, headers=HEADERS)
    if response.status_code >= 400:
        await parse_and_raise_error(response)

    return response.json()
