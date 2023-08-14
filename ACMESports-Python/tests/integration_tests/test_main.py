import httpx
from fastapi.testclient import TestClient
from app.main import app
from tests.mock_data import mock_error_response


client = TestClient(app)

def test_get_events():

    response = client.post("/events", json={"league": "NFL", "startDate": "2023-08-01", "endDate": "2023-08-31"})

    assert response.status_code == 200

    response_data = response.json()
    assert "events" in response_data

    event = response_data["events"][0]   
    assert event["eventId"] == "4dd7dca1-6606-4afe-b137-935d0d2338d9"
    assert event["eventDateimestamp"] == "2023-08-07"
    assert event["eventTime"] == "12:00:00"
    assert event["homeTeamId"] == "8ff6a159-4252-41b4-bd50-8c44571efa79"
    assert event["homeTeamNickName"] == "Eagles"
    assert event["homeTeamCity"] == "Philadelphia"
    assert event["homeTeamRank"] == 1
    assert event["homeTeamRankPoints"] == 1500.0
    assert event["awayTeamId"] == "46dd4c25-1ed4-49cc-8b21-bc614373ee03"
    assert event["awayTeamNickName"] == "Patriots"
    assert event["awayTeamCity"] == "New England"
    assert event["awayTeamRank"] == 2
    assert event["awayTeamRankPoints"] == 2000.0


def test_bad_request():
    response = client.post("/events", json={"league": "XXXX", "startDate": "2023-08-01", "endDate": "2023-08-31"})
    assert response.status_code == 400
    assert "errors" in response.json()