using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProblemResults;

public class Result<T> : ResultBase
{
    public T? Value { get; set; }

    public TResult Match<TResult>(
        Func<T, TResult> onSuccess,
        Func<ProblemDetails, TResult> onFailure)
        => IsSuccess
            ? onSuccess(Value!)
            : onFailure(Problem!);

    public ActionResult<T> Match(
        Func<T, ActionResult<T>> onSuccess,
        Func<ProblemDetails, ActionResult<T>> onFailure)
        => IsSuccess
            ? onSuccess(Value!)
            : onFailure(Problem!);

    public IResult Match(
        Func<T, IResult> onSuccess,
        Func<ProblemDetails, IResult> onFailure)
        => IsSuccess
            ? onSuccess(Value!)
            : onFailure(Problem!);

    public ActionResult ToActionResult(string? traceId = null)
        => IsSuccess
            ? new OkObjectResult(Value)
            : ResultFactory.ProblemActionResult(Problem!, traceId);

    public ActionResult ToActionResult(
        ControllerBase controller,
        string? traceId = null)
        => IsSuccess
            ? new OkObjectResult(Value)
            : ResultFactory.ProblemActionResult(Problem!, controller.HttpContext, traceId);

    public IResult ToIResult(string? traceId = null)
        => IsSuccess
            ? TypedResults.Ok(Value)
            : ResultFactory.ProblemIResult(Problem!, traceId);

    public IResult ToIResult(
        HttpContext httpContext,
        string? traceId = null)
        => IsSuccess
            ? TypedResults.Ok(Value)
            : ResultFactory.ProblemIResult(Problem!, httpContext, traceId);

    public ActionResult HandleSuccess(Func<T, ActionResult> onSuccess)
        => IsSuccess
            ? onSuccess(Value!)
            : ResultFactory.ProblemActionResult(Problem!);

    public IResult HandleSuccess(Func<T, IResult> onSuccess)
        => IsSuccess
            ? onSuccess(Value!)
            : ResultFactory.ProblemIResult(Problem!);

    public void OnSuccess<TResult>(Func<T, TResult> onSuccess)
    {
        if (IsSuccess) onSuccess(Value!);
    }

    public static implicit operator Result<T>(Result result)
        => new() { Problem = result.Problem };
}