using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProblemResults.Core;

namespace ProblemResults.AspNetCore;

public static class ResultFactory
{
    private static readonly HashSet<string> ForbiddenFields = new() { "type", "title", "instance", "traceId" };

    private static ProblemDetails NewProblemDetails(
        Problem problem,
        HttpContext httpContext,
        string? traceId = null)
    {
        ProblemDetails problemDetails = new()
        {
            Type = ProblemDefaults.Type(problem.Status),
            Title = ProblemDefaults.Title(problem.Status),
            Status = problem.Status,
            Detail = problem.Detail,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
        };

        if (problem.Extensions.Count > 0)
            foreach (KeyValuePair<string, object?> extension in problem.Extensions)
                if (!ForbiddenFields.Contains(extension.Key))
                    problemDetails.Extensions.Add(extension);

        problemDetails.Extensions["traceId"] = traceId
                                               ?? Activity.Current?.Id
                                               ?? httpContext.TraceIdentifier;

        return problemDetails;
    }

    public static ObjectResult ProblemActionResult(
        Problem problem,
        HttpContext httpContext,
        string? traceId = null)
    {
        ProblemDetails problemDetails = NewProblemDetails(problem, httpContext, traceId);

        return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
    }

    public static ProblemHttpResult ProblemIResult(
        Problem problem,
        HttpContext httpContext,
        string? traceId = null)
    {
        ProblemDetails problemDetails = NewProblemDetails(problem, httpContext, traceId);

        return TypedResults.Problem(problemDetails);
    }
}