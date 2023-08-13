from jsonschema import ValidationError
from app.schemas.event import Event, EventsResponse


def rankings_to_dict(rankings_list):
    return {
        ranking["teamId"]: {
            "rank": ranking["rank"],
            "rankPoints": ranking["rankPoints"],
        }
        for ranking in rankings_list
    }


def transform_events(scoreboard_data, rankings_data_list):
    rankings_data = rankings_to_dict(rankings_data_list)
    transformed_data = []

    for event in scoreboard_data["data"]:
        home_team_id = event["home"]["id"]
        away_team_id = event["away"]["id"]

        home_ranking_details = rankings_data.get(
            home_team_id, {"rank": "N/A", "rankPoints": "N/A"}
        )
        away_ranking_details = rankings_data.get(
            away_team_id, {"rank": "N/A", "rankPoints": "N/A"}
        )

        # Extract eventDate and eventTime from timestamp
        event_date, event_time = event["timestamp"].split("T")

        transformed_event = {
            "eventId": event["id"],
            "eventDateimestamp": event_date,
            "eventTime": event_time.split("Z")[0],
            "homeTeamId": home_team_id,
            "homeTeamNickName": event["home"]["nickName"],
            "homeTeamCity": event["home"]["city"],
            "homeTeamRank": home_ranking_details["rank"],
            "homeTeamRankPoints": home_ranking_details["rankPoints"],
            "awayTeamId": away_team_id,
            "awayTeamNickName": event["away"]["nickName"],
            "awayTeamCity": event["away"]["city"],
            "awayTeamRank": away_ranking_details["rank"],
            "awayTeamRankPoints": away_ranking_details["rankPoints"],
        }

        # Continue processing valid_event as required
        transformed_data.append(transformed_event)

    return transformed_data
