
from jsonschema import ValidationError
from app.schemas.event import Event, EventsResponse
from app.logger import logger


def rankings_to_dict(rankings_list):
    return {
        ranking["teamId"]: {
            "rank": ranking["rank"],
            "rankPoints": ranking["rankPoints"],
        }
        for ranking in rankings_list
    }


def transform_events(scoreboard_data, rankings_data_list):
    logger.debug("Transforming events data")
    rankings_data = rankings_to_dict(rankings_data_list)
    logger.debug(f"Rankings data: {rankings_data}")
    transformed_data = []
    logger.debug(f"Scoreboard data: {scoreboard_data}")
    for event in scoreboard_data:
        logger.debug(f"Processing event: {event}")
        home_team_id = event["home"]["id"]
        away_team_id = event["away"]["id"]
        logger.debug(f"Home team id: {home_team_id}")
        home_ranking_details = rankings_data.get(
            home_team_id, {"rank": "N/A", "rankPoints": "N/A"}
        )
        away_ranking_details = rankings_data.get(
            away_team_id, {"rank": "N/A", "rankPoints": "N/A"}
        )
        logger.debug(f"Home team ranking details: {home_ranking_details}")
        # Extract eventDate and eventTime from timestamp
        event_date, event_time = event["timestamp"].split("T")
        logger.debug(f"Event date: {event_date}")
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
