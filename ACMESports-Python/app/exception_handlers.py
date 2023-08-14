import sys
import pybreaker

from typing import Union

from fastapi import Request
from fastapi.exceptions import RequestValidationError, HTTPException
from fastapi.exception_handlers import http_exception_handler as _http_exception_handler
from fastapi.exception_handlers import (
    request_validation_exception_handler as _request_validation_exception_handler,
)
from fastapi.responses import JSONResponse
from fastapi.responses import PlainTextResponse
from fastapi.responses import Response
import httpx

from app.logger import logger


async def request_validation_exception_handler(
    request: Request, exc: RequestValidationError
) -> JSONResponse:
    for error in exc.errors():
        logger.info(error)
        if error["loc"][0] == "body" and error["loc"][1] == "league":
            # Log and customize the error message
            detail = {"errors": [{"loc": error["loc"], "msg": "Invalid league"}]}
            logger.info(detail)
            return JSONResponse(status_code=400, content=detail)

    body = await request.body()
    query_params = request.query_params._dict
    detail = {
        "errors": exc.errors(),
        "body": body.decode(),
        "query_params": query_params,
    }
    logger.info(detail)
    return await _request_validation_exception_handler(request, exc)


async def http_exception_handler(
    request: Request, exc: HTTPException
) -> Union[JSONResponse, Response]:
    """
    This is a wrapper to the default HTTPException handler of FastAPI.
    This function will be called when a HTTPException is explicitly raised.
    """
    logger.debug("Our custom http_exception_handler was called")
    return await _http_exception_handler(request, exc)


async def unhandled_exception_handler(
    request: Request, exc: Exception
) -> PlainTextResponse:
    """
    This middleware will log all unhandled exceptions.
    Unhandled exceptions are all exceptions that are not HTTPExceptions or RequestValidationErrors.
    """
    logger.debug("Our custom unhandled_exception_handler was called")
    host = getattr(getattr(request, "client", None), "host", None)
    port = getattr(getattr(request, "client", None), "port", None)
    url = (
        f"{request.url.path}?{request.query_params}"
        if request.query_params
        else request.url.path
    )
    exception_type, exception_value, _ = sys.exc_info()
    exception_name = getattr(exception_type, "__name__", None)
    logger.debug("exception_name: %s", exception_name)
    logger.error(
        "%s:%s - \"%s %s\" 500 Internal Server Error <%s: %s>",
        host,
        port,
        request.method,
        url,
        exception_name,
        exception_value,
    )

    if isinstance(exc, httpx.HTTPError):
        user_message = "An external service error occurred. Please contact admin for details."
        logger.error("External API Error: %s", str(exc))
        return PlainTextResponse(user_message, status_code=500)

    if isinstance(exc, pybreaker.CircuitBreakerError):
        user_message = "External service currently unavailable. Please try again later."
        logger.error("Circuit open due to multiple failures with external service.")
        return PlainTextResponse(user_message, status_code=503)  # 503 Service Unavailable


    if isinstance(exc, pybreaker.CircuitBreakerError):
        user_message = "External service currently unavailable. Please try again later."
        logger.error("Circuit open due to multiple failures with external service.")
        return PlainTextResponse(user_message, status_code=503)  # 503 Service Unavailable

    return PlainTextResponse("An unexpected error occurred. Please contact admin for details.", status_code=500)
