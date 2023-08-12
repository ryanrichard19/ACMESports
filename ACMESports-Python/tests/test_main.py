import httpx
from fastapi.testclient import TestClient
from app.main import app
from tests.mock_data import mock_successfull_scoreboard,mock_successfull_team_rankings, mock_error_response



client = TestClient(app)

async def mock_api_call(request):
    if "/LeagueEnum.NFL/scoreboard" in request.url.path:
        return httpx.Response(200, json=mock_successfull_scoreboard)
    elif "/team-rankings" in request.url.path:
        return httpx.Response(200, json=mock_successfull_team_rankings)
    else:
        return httpx.Response(400, json=mock_error_response)

# Attach the mock to httpx
transport = httpx.MockTransport(mock_api_call)

# Update httpx global client to use the mocked transport
httpx._DEFAULT_CLIENT = httpx.AsyncClient(transport=transport)


def test_get_events():
    response = client.post("/events", json={"league": "NFL", "startDate": "2023-08-01", "endDate": "2023-08-31"})

    print(response.json())
    assert response.status_code == 200
    assert "data" in response.json()
