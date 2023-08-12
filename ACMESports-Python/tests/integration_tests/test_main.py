import pytest
import httpx
from fastapi.testclient import TestClient
from app.main import app
from tests.mock_data import mock_successfull_scoreboard,mock_successfull_team_rankings, mock_error_response
import respx

client = TestClient(app)

@respx.mock
def test_get_events():
    respx.get("/LeagueEnum.NFL/scoreboard").mock(return_value=httpx.Response(200, json=mock_successfull_scoreboard))
    respx.get("/LeagueEnum.NFL/team-rankings").mock(return_value=httpx.Response(200, json=mock_successfull_team_rankings))

    response = client.post("/events", json={"league": "NFL", "startDate": "2023-08-01", "endDate": "2023-08-31"})

    assert response.status_code == 200
    assert "events" in response.json()
