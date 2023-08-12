from pydantic import BaseModel
from typing import List, Optional
from uuid import UUID
from datetime import date, time
from enum import Enum

class LeagueEnum(str, Enum):
    NFL = "NFL"

class EventsRequest(BaseModel):
    league: LeagueEnum
    startDate: date
    endDate: date

class Event(BaseModel):
    eventId: UUID
    eventDateimestamp: date
    eventTime: time
    homeTeamId: UUID
    homeTeamNickName: str
    homeTeamCity: str
    homeTeamRank: int
    homeTeamRankPoints: float
    awayTeamId: UUID
    awayTeamNickName: str
    awayTeamCity: str
    awayTeamRank: int
    awayTeamRankPoints: float

class EventsResponse(BaseModel):
    events: List[Event]
