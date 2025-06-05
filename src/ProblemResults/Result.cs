using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProblemResults;

public class Result : ResultBase
{
    // These non-static methods are not included in ResultBase to prevent the user
    // from selecting methods without a value when using Result<T>
    public TResult Match<TResult>(
        Func<TResult> onSuccess,
        Func<Problem, TResult> onFailure)
        => IsSuccess
            ? onSuccess()
            : onFailure(Problem!);

    public ActionResult Match(
        Func<ActionResult> onSuccess,
        Func<Problem, ActionResult> onFailure)
        => IsSuccess
            ? onSuccess()
            : onFailure(Problem!);

    public IResult Match(
        Func<IResult> onSuccess,
        Func<Problem, IResult> onFailure)
        => IsSuccess
            ? onSuccess()
            : onFailure(Problem!);

    public ActionResult ToActionResult(
        ControllerBase controller,
        string? traceId = null)
        => IsSuccess
            ? new NoContentResult()
            : ResultFactory.ProblemActionResult(Problem!, controller.HttpContext, traceId);

    public IResult ToIResult(
        HttpContext httpContext,
        string? traceId = null)
        => IsSuccess
            ? TypedResults.NoContent()
            : ResultFactory.ProblemIResult(Problem!, httpContext, traceId);

    public ActionResult HandleSuccess(
        Func<ActionResult> onSuccess,
        ControllerBase controller)
        => IsSuccess
            ? onSuccess()
            : ResultFactory.ProblemActionResult(Problem!, controller.HttpContext);

    public IResult HandleSuccess(
        Func<IResult> onSuccess,
        HttpContext httpContext)
        => IsSuccess
            ? onSuccess()
            : ResultFactory.ProblemIResult(Problem!, httpContext);

    public ActionResult HandleFailure(Func<Problem, ActionResult> onFailure)
        => IsSuccess
            ? new NoContentResult()
            : onFailure(Problem!);

    public IResult HandleFailure(Func<Problem, IResult> onFailure)
        => IsSuccess
            ? TypedResults.NoContent()
            : onFailure(Problem!);

    public void OnSuccess(Action onSuccess)
    {
        if (IsSuccess) onSuccess();
    }

    public static Result Success()
        => new();

    public static Result<T> Success<T>(T value)
        => new() { Value = value };

    public static Result Failure(
        int statusCode,
        string? detail = null,
        params (string key, object? value)[]? extensions)
        => new()
        {
            Problem = new(
                statusCode,
                detail,
                extensions?.ToDictionary(tuple => tuple.key, tuple => tuple.value))
        };

    public static Result Failure(
        int statusCode,
        string? detail = null,
        IDictionary<string, object?>? extensions = null)
        => new()
        {
            Problem = new(
                statusCode,
                detail,
                extensions)
        };
}