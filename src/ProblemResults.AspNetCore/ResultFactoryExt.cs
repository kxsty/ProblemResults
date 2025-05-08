using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProblemResults.Core;

namespace ProblemResults.AspNetCore;

public static class ResultFactoryExt
{
    public static ObjectResult ProblemActionResult(Problem problem, HttpContext httpContext,
        string? traceId = null)
    {
        problem.Extensions.TryAdd("traceId", traceId
                                             ?? Activity.Current?.Id
                                             ?? httpContext.TraceIdentifier);

        if (problem.Instance == ResultFactory.GenerateValue)
            problem.Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}";

        return new ObjectResult(problem) { StatusCode = problem.Status };
    }

    public static ObjectResult ProblemActionResult(Problem problem, string? traceId = null)
    {
        problem.Extensions.TryAdd("traceId", traceId
                                             ?? Activity.Current?.Id
                                             ?? Activity.Current?.TraceId.ToString());

        if (problem.Instance == ResultFactory.GenerateValue)
            problem.Instance = null;


        return new ObjectResult(problem) { StatusCode = problem.Status };
    }

    public static ProblemHttpResult ProblemIResult(Problem problem, HttpContext httpContext,
        string? traceId = null)
    {
        problem.Extensions.TryAdd("traceId", traceId
                                             ?? Activity.Current?.Id
                                             ?? httpContext.TraceIdentifier);

        if (problem.Instance == ResultFactory.GenerateValue)
            problem.Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}";

        return TypedResults.Problem(
            problem.Detail,
            problem.Instance,
            problem.Status,
            problem.Title,
            problem.Type,
            problem.Extensions);
    }

    public static ProblemHttpResult ProblemIResult(Problem problem, string? traceId = null)
    {
        problem.Extensions.TryAdd("traceId", traceId
                                             ?? Activity.Current?.Id
                                             ?? Activity.Current?.TraceId.ToString());

        if (problem.Instance == ResultFactory.GenerateValue)
            problem.Instance = null;

        return TypedResults.Problem(
            problem.Detail,
            problem.Instance,
            problem.Status,
            problem.Title,
            problem.Type,
            problem.Extensions);
    }
}