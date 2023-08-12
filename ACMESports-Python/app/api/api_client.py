import requests
from config import THIRD_PARTY_BASE_URL, API_KEY

HEADERS = {
    "X-API-Key": API_KEY
}

def get_scoreboard(league, since, until):
    endpoint = f"{THIRD_PARTY_BASE_URL}/{league}/scoreboard"
    params = {
        "since": since,
        "until": until
    }
    response = requests.get(endpoint, headers=HEADERS, params=params)
    # Error handling (e.g., checking response.status_code) can be done here
    return response.json()

def get_team_rankings(league):
    endpoint = f"{THIRD_PARTY_BASE_URL}/{league}/team-rankings"
    response = requests.get(endpoint, headers=HEADERS)
    return response.json()
