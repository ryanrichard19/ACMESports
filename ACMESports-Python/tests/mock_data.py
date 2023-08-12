mock_successfull_scoreboard = {
    "data": [
        {
            "id": "event-uuid-1",
            "timestamp": "2023-08-01T12:00:00Z",
            "home": {
                "id": "team-uuid-1",
                "nickName": "Team A",
                "city": "City A"
            },
            "away": {
                "id": "team-uuid-2",
                "nickName": "Team B",
                "city": "City B"
            }
        },
        {
            "id": "event-uuid-2",
            "timestamp": "2023-08-02T12:00:00Z",
            "home": {
                "id": "team-uuid-3",
                "nickName": "Team C",
                "city": "City C"
            },
            "away": {
                "id": "team-uuid-4",
                "nickName": "Team D",
                "city": "City D"
            }
        }
    ]
}

mock_successfull_team_rankings = [
    {
        "teamId": "team-uuid-1",
        "rank": 1,
        "rankPoints": 88
    },
    {
        "teamId": "team-uuid-2",
        "rank": 2,
        "rankPoints": 86
    },
    {
        "teamId": "team-uuid-3",
        "rank": 3,
        "rankPoints": 84
    },
    {
        "teamId": "team-uuid-4",
        "rank": 4,
        "rankPoints": 82
    }
]


mock_error_response = {
    "title": "Bad Request",
    "status": 400,
    "detail": "Invalid league provided",
    "cause": None
}
