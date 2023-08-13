from fastapi import HTTPException
import httpx
from config import THIRD_PARTY_BASE_URL, API_KEY

HEADERS = {"X-API-Key": API_KEY}


async def get_scoreboard(league, since, until):
    endpoint = f"{THIRD_PARTY_BASE_URL}/{league.value}/scoreboard"
    params = {"since": since, "until": until}
    async with httpx.AsyncClient() as client:
        response = await client.get(endpoint, headers=HEADERS, params=params)
        if response.status_code != 200:
            raise HTTPException(status_code=response.status_code, detail=response.text)
        return response.json()


async def get_team_rankings(league):
    endpoint = f"{THIRD_PARTY_BASE_URL}/{league.value}/team-rankings"
    async with httpx.AsyncClient() as client:
        response = await client.get(endpoint, headers=HEADERS)
        if response.status_code != 200:
            raise HTTPException(status_code=response.status_code, detail=response.text)
        return response.json()
