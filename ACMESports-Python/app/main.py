import asyncio
from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from fastapi.exceptions import RequestValidationError
from starlette.exceptions import HTTPException
from app.exception_handlers import (
    request_validation_exception_handler,
    http_exception_handler,
    unhandled_exception_handler,
)
from app.middleware import log_request_middleware
from app.api.api_client import get_scoreboard, get_team_rankings
from app.data_transformer import transform_events
from app.schemas import EventsRequest, EventsResponse
from app.logger import logger

from config import THIRD_PARTY_BASE_URL

app = FastAPI()

app.middleware("http")(log_request_middleware)
app.add_exception_handler(RequestValidationError, request_validation_exception_handler)
app.add_exception_handler(HTTPException, http_exception_handler)
app.add_exception_handler(Exception, unhandled_exception_handler)


origins = [
    THIRD_PARTY_BASE_URL,
]

app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)


@app.post("/events", response_model=EventsResponse)
async def get_events(request: EventsRequest):
    """
    Get events for a given league and date range.

    Args:
        request: The request object containing the league and date range.

    Returns:
        An EventsResponse object containing the events for the given league and date range.
    """
    league = request.league
    start_date = request.startDate
    end_date = request.endDate
    logger.info(
        "Getting events for league: %s, start_date: %s, end_date: %s",
        league,
        start_date,
        end_date,
    )

    scoreboard_data, rankings_data = await asyncio.gather(
        get_scoreboard(league, start_date, end_date), get_team_rankings(league)
    )

    transformed_data = transform_events(scoreboard_data, rankings_data)

    return EventsResponse(events=transformed_data)
