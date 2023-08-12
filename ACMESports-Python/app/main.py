from fastapi import FastAPI
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


#@app.post("/events", response_model=EventsResponse)
@app.post("/events")
async def get_events(request: EventsRequest):
    league = request.league
    startDate = request.startDate
    endDate = request.endDate
    print(league, startDate, endDate)
    scoreboard_data = get_scoreboard(league, startDate, endDate)
    print(scoreboard_data)
   # rankings_data = get_team_rankings(league)
   # transformed_data = transform_events(scoreboard_data, rankings_data)
    return {"msg": "Hello World"} #transformed_data
