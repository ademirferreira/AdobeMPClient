using AdobeMPClient.Models.Common;
using System.Net;

namespace AdobeMPClient.API.Extensions;

public static class ResultExtensions
{
    public static IResult ToResult<T>(this Result<T> result)
    {
        return result.Match<IResult>(
            (data, statusCode) => statusCode switch
            {
                (int)HttpStatusCode.OK => TypedResults.Ok(data),
                (int)HttpStatusCode.Created => TypedResults.Created("", data),
                (int)HttpStatusCode.Accepted => TypedResults.Accepted<T>("", data),
                _ => TypedResults.StatusCode(statusCode)
            },
            (error, statusCode) => statusCode switch
            {
                (int)HttpStatusCode.BadRequest => TypedResults.BadRequest(error),
                (int)HttpStatusCode.NotFound => TypedResults.NotFound(error),
                _ => TypedResults.Problem(
                    detail: error?.Message ?? "An error occurred",
                    statusCode: statusCode > 0 ? statusCode : (int)HttpStatusCode.InternalServerError
                )
            }
        );
    }

    public static IResult ToResult<T>(this Result<T> result, Func<T, string> locationFactory)
    {
        return result.Match<IResult>(
            (data, statusCode) => statusCode switch
            {
                (int)HttpStatusCode.OK => TypedResults.Ok(data),
                (int)HttpStatusCode.Created => TypedResults.Created(locationFactory(data), data),
                (int)HttpStatusCode.Accepted => TypedResults.Accepted(locationFactory(data), data),
                _ => TypedResults.StatusCode(statusCode)
            },
            (error, statusCode) => statusCode switch
            {
                (int)HttpStatusCode.BadRequest => TypedResults.BadRequest(error),
                (int)HttpStatusCode.NotFound => TypedResults.NotFound(error),
                _ => TypedResults.Problem(
                    detail: error?.Message ?? "An error occurred",
                    statusCode: statusCode > 0 ? statusCode : (int)HttpStatusCode.InternalServerError
                )
            }
        );
    }
}
