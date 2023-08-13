import httpx
from fastapi import HTTPException
from app.logger import logger
from config import THIRD_PARTY_BASE_URL, API_KEY

HEADERS = {"X-API-Key": API_KEY}


async def parse_and_raise_error(response):
    """Parse the error response and raise an HTTPException."""
    try:
        error_data = response.json()
        title = error_data.get("title", "Unknown error")
        status = error_data.get("status", response.status_code)
        detail = error_data.get("detail", "No detail provided")

        logger.error("Error from 3rd party API. Title: %s, Detail: %s", title, detail)

        # Raise an HTTPException
        raise HTTPException(status_code=status, detail=detail)

    except ValueError as exc:  # If the response body is not JSON or unexpected format
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
    async with httpx.AsyncClient() as client:
        response = await client.get(endpoint, headers=HEADERS, params=params)
        if response.status_code != 200:
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
    async with httpx.AsyncClient() as client:
        response = await client.get(endpoint, headers=HEADERS)
        if response.status_code != 200:
            await parse_and_raise_error(response)

        return response.json()
