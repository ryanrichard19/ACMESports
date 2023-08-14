import pytest
from fastapi.testclient import TestClient
from app.main import app
from app.api import api_client
from tests.mock_data import (
    mock_successfull_scoreboard,
    mock_successfull_team_rankings
)

client = TestClient(app)

def test_get_events_success(mocker):
    mocker.patch.object(
        api_client, "get_scoreboard", return_value=mock_successfull_scoreboard
    )
    mocker.patch.object(
        api_client, "get_team_rankings", return_value=mock_successfull_team_rankings
    )

    response = client.post(
        "/events",
        json={"league": "NFL", "startDate": "2023-08-01", "endDate": "2023-08-31"},
    )

    assert response.status_code == 200


def test_get_events_invalid_league():
    response = client.post(
        "/events",
        json={"league": "XXXX", "startDate": "2023-08-01", "endDate": "2023-08-31"},
    )
    assert response.status_code == 400


