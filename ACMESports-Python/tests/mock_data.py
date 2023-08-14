mock_successfull_scoreboard = [
    {
        "id": "4dd7dca1-6606-4afe-b137-935d0d2338d9",
        "timestamp": "2023-08-01T12:00:00Z",
        "home": {
            "id": "8ff6a159-4252-41b4-bd50-8c44571efa79",
            "nickName": "Team A",
            "city": "City A",
        },
        "away": {
            "id": "46dd4c25-1ed4-49cc-8b21-bc614373ee03",
            "nickName": "Team B",
            "city": "City B",
        },
    },
    {
        "id": "eca19690-38de-4284-81e6-3d413ac6a476",
        "timestamp": "2023-08-02T12:00:00Z",
        "home": {
            "id": "f38773a0-e842-427d-98a7-0ebdca9417e1",
            "nickName": "Team C",
            "city": "City C",
        },
        "away": {
            "id": "b34e1351-b475-4f68-92b3-c48ad740e388",
            "nickName": "Team D",
            "city": "City D",
        },
    },
]

mock_successfull_team_rankings = [
    {"teamId": "8ff6a159-4252-41b4-bd50-8c44571efa79", "rank": 1, "rankPoints": 88},
    {"teamId": "46dd4c25-1ed4-49cc-8b21-bc614373ee03", "rank": 2, "rankPoints": 86},
    {"teamId": "f38773a0-e842-427d-98a7-0ebdca9417e1", "rank": 3, "rankPoints": 84},
    {"teamId": "b34e1351-b475-4f68-92b3-c48ad740e388", "rank": 4, "rankPoints": 82},
]


mock_error_response = {
  "title": "Invalid Request",
  "status": 400,
  "detail": "The request parameters provided are incorrect or in an invalid format.",
  "cause": {
    "value": "The 'league' parameter provided does not match any of our supported leagues, or the 'since'/'until' query parameters are missing or in an invalid format. Please ensure you're following the correct format and try again."
  }
}
