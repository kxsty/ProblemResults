using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ProblemResults;

public static class ResultFactory
{
    public const string GenerateValue = "R2VuZXJhdGVWYWx1ZQ==";

    public static ProblemDetails NewProblem(
        int statusCode,
        string? detail = null,
        string? type = null,
        string? title = null,
        string? instance = null,
        IDictionary<string, object?>? extensions = null)
        => new()
        {
            Type = type ?? ProblemDetailsDefaults.Type(statusCode),
            Title = title ?? ProblemDetailsDefaults.Title(statusCode),
            Status = statusCode,
            Detail = detail,
            Instance = instance ?? GenerateValue,
            Extensions = extensions ?? new Dictionary<string, object?>()
        };

    public static ProblemDetails NewProblemCustom(
        string? type = null,
        string? title = null,
        int? statusCode = null,
        string? detail = null,
        string? instance = null,
        IDictionary<string, object?>? extensions = null)
        => new()
        {
            Type = type,
            Title = title,
            Status = statusCode ?? 500,
            Detail = detail,
            Instance = instance,
            Extensions = extensions ?? new Dictionary<string, object?>()
        };

    public static ObjectResult ProblemActionResult(ProblemDetails problemDetails, HttpContext httpContext,
        string? traceId = null)
    {
        problemDetails.Extensions.TryAdd("traceId", traceId
                                                    ?? Activity.Current?.Id
                                                    ?? httpContext.TraceIdentifier);

        if (problemDetails.Instance == GenerateValue)
            problemDetails.Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}";

        return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
    }

    public static ObjectResult ProblemActionResult(ProblemDetails problemDetails, string? traceId = null)
    {
        problemDetails.Extensions.TryAdd("traceId", traceId
                                                    ?? Activity.Current?.Id
                                                    ?? Activity.Current?.TraceId.ToString());

        if (problemDetails.Instance == GenerateValue)
            problemDetails.Instance = null;


        return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
    }

    public static ProblemHttpResult ProblemIResult(ProblemDetails problemDetails, HttpContext httpContext,
        string? traceId = null)
    {
        problemDetails.Extensions.TryAdd("traceId", traceId
                                                    ?? Activity.Current?.Id
                                                    ?? httpContext.TraceIdentifier);

        if (problemDetails.Instance == GenerateValue)
            problemDetails.Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}";

        return TypedResults.Problem(
            problemDetails.Detail,
            problemDetails.Instance,
            problemDetails.Status,
            problemDetails.Title,
            problemDetails.Type,
            problemDetails.Extensions);
    }

    public static ProblemHttpResult ProblemIResult(ProblemDetails problemDetails, string? traceId = null)
    {
        problemDetails.Extensions.TryAdd("traceId", traceId
                                                    ?? Activity.Current?.Id
                                                    ?? Activity.Current?.TraceId.ToString());

        if (problemDetails.Instance == GenerateValue)
            problemDetails.Instance = null;

        return TypedResults.Problem(
            problemDetails.Detail,
            problemDetails.Instance,
            problemDetails.Status,
            problemDetails.Title,
            problemDetails.Type,
            problemDetails.Extensions);
    }
}