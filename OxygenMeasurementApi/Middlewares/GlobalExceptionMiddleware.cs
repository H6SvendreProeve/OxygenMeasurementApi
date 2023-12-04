using System.Net;
using Microsoft.AspNetCore.Mvc;
using OxygenMeasurementApi.Exceptions;
using ArgumentException = System.ArgumentException;
using UnauthorizedAccessException = System.UnauthorizedAccessException;

namespace OxygenMeasurementApi.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate next;


    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message) = GetExceptionDetails(exception);

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/problem+json";

        var problemDetails = new ProblemDetails
        {
            Detail = message,
            Status = (int)statusCode,
        };


        await context.Response.WriteAsJsonAsync(problemDetails);
    }


    private static (HttpStatusCode, string) GetExceptionDetails(Exception exception)
    {
        switch (exception)
        {
            case BadRequestException:
            case CustomArgumentOutOfRangeException:
                return (HttpStatusCode.BadRequest, exception.Message);

            case UnauthorizedAccessException unauthorizedAccessException:
                return (HttpStatusCode.Unauthorized, unauthorizedAccessException.Message);

            case KeyNotFoundException:
            case HeaderNotFoundException:
            case NotFoundException:
            case ArgumentException:
                return (HttpStatusCode.NotFound, exception.Message);


            case CustomDbException:
                return (HttpStatusCode.InternalServerError,
                    "An error happened on the database level. Contact the administrator for further information");
            case InternalServerException:
                return (HttpStatusCode.InternalServerError, "An unexpected error occured");

            default:
                return (HttpStatusCode.InternalServerError, "An unexpected error occurred");
        }
    }
}