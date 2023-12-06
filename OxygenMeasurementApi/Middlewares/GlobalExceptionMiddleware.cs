using System.Net;
using Microsoft.AspNetCore.Mvc;
using OxygenMeasurementApi.Exceptions;

namespace OxygenMeasurementApi.Middlewares;

/// <summary>
/// Middleware for handling exceptions globally in the ASP.NET Core Web Api.
/// </summary>
public class GlobalExceptionMiddleware
{
    // function that processes a http request
    private readonly RequestDelegate next;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalExceptionMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    /// <summary>
    /// Invokes the middleware to handle exceptions globally.
    /// calls HandleExceptionAsync when an exception was thrown.
    /// </summary>
    /// <param name="context">The HttpContext for the request.</param>
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

    /// <summary>
    /// Handles exceptions by setting the appropriate HTTP status code and returning a JSON response
    /// with problem details, including the exception details.
    /// </summary>
    /// <param name="context">The HttpContext for the request.</param>
    /// <param name="exception">The exception that occurred.</param>
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

    /// <summary>
    /// Gets the HTTP status code and an error message based on the type of exception.
    /// </summary>
    /// <param name="exception">The exception for which to retrieve details.</param>
    /// <returns>A tuple containing the HTTP status code and error message.</returns>
    private static (HttpStatusCode, string) GetExceptionDetails(Exception exception)
    {
        switch (exception)
        {
            case BadRequestException:
            case CustomArgumentOutOfRangeException:
                return (HttpStatusCode.BadRequest, exception.Message);

            case NotFoundException:
            case ArgumentException:
                return (HttpStatusCode.NotFound, exception.Message);

            case CustomDbException:
                return (HttpStatusCode.InternalServerError,
                    "An error happened on the database level. Contact the administrator for further information");

            default:
                return (HttpStatusCode.InternalServerError, "An unexpected error occurred");
        }
    }
}