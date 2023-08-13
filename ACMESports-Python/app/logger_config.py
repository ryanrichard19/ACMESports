import logging
import os

# Set up logging
logging.basicConfig(
    format="%(name)s: %(asctime)s | %(levelname)s | %(filename)s:%(lineno)s | %(process)d >>> %(message)s",
    datefmt="%Y-%m-%dT%H:%M:%SZ",
)
logger = logging.getLogger(__name__)
log_level = os.getenv("LOG_LEVEL", "INFO")
logger.setLevel(log_level)