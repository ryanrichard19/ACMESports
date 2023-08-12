import asyncio
from fastapi import FastAPI
from jsonschema import ValidationError
from app.api.api_client import get_scoreboard, get_team_rankings
from app.data_transformer import transform_events
from app.schemas import EventsRequest, EventsResponse
from fastapi.middleware.cors import CORSMiddleware
from config import THIRD_PARTY_BASE_URL

app = FastAPI()

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
    league = request.league
    startDate = request.startDate
    endDate = request.endDate
    scoreboard_data, rankings_data = await asyncio.gather(
    get_scoreboard(league, startDate, endDate),
    get_team_rankings(league)
)
    transformed_data = transform_events(scoreboard_data, rankings_data)
   
   
    return EventsResponse(events=transformed_data)
