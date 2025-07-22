using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProblemResults;

public class Result<T> : ResultBase
{
    public T? Value { get; init; }

    public TResult Match<TResult>(
        Func<T, TResult> onSuccess,
        Func<Problem, TResult> onFailure)
        => IsSuccess
            ? onSuccess(Value!)
            : onFailure(Problem!);

    public ActionResult<T> Match(
        Func<T, ActionResult<T>> onSuccess,
        Func<Problem, ActionResult<T>> onFailure)
        => IsSuccess
            ? onSuccess(Value!)
            : onFailure(Problem!);

    public IResult Match(
        Func<T, IResult> onSuccess,
        Func<Problem, IResult> onFailure)
        => IsSuccess
            ? onSuccess(Value!)
            : onFailure(Problem!);

    public ActionResult ToActionResult(
        ControllerBase controller,
        string? traceId = null)
        => IsSuccess
            ? new OkObjectResult(Value)
            : ResultFactory.ProblemActionResult(Problem!, controller.HttpContext, traceId);

    public IResult ToIResult(
        HttpContext httpContext,
        string? traceId = null)
        => IsSuccess
            ? TypedResults.Ok(Value)
            : ResultFactory.ProblemIResult(Problem!, httpContext, traceId);

    public ActionResult HandleSuccess(
        Func<T, ActionResult> onSuccess,
        ControllerBase controller)
        => IsSuccess
            ? onSuccess(Value!)
            : ResultFactory.ProblemActionResult(Problem!, controller.HttpContext);

    public IResult HandleSuccess(
        Func<T, IResult> onSuccess,
        HttpContext httpContext)
        => IsSuccess
            ? onSuccess(Value!)
            : ResultFactory.ProblemIResult(Problem!, httpContext);

    public ActionResult HandleFailure(Func<Problem, ActionResult> onFailure)
        => IsSuccess
            ? new OkObjectResult(Value)
            : onFailure(Problem!);

    public IResult HandleFailure(Func<Problem, IResult> onFailure)
        => IsSuccess
            ? TypedResults.Ok(Value)
            : onFailure(Problem!);

    public void OnSuccess(Action<T> onSuccess)
    {
        if (IsSuccess) onSuccess(Value!);
    }

    public static Result<T> Success()
        => new();

    public static Result<T> Success(T value)
        => new() { Value = value };

    public static Result<T> Failure(
        ProblemCodes statusCode,
        string? detail = null,
        params (string key, object? value)[]? extensions)
        => new()
        {
            Problem = new(
                statusCode,
                detail,
                extensions?.ToDictionary(tuple => tuple.key, tuple => tuple.value))
        };

    public static Result<T> Failure(
        ProblemCodes statusCode,
        string? detail = null,
        IDictionary<string, object?>? extensions = null)
        => new()
        {
            Problem = new(
                statusCode,
                detail,
                extensions)
        };

    public static implicit operator Result<T>(Result result)
        => new() { Problem = result.Problem };

    public static implicit operator Result<T>(T value)
        => new() { Value = value };
}