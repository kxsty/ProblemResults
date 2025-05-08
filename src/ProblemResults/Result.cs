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
        Func<ProblemDetails, TResult> onFailure)
        => IsSuccess
            ? onSuccess()
            : onFailure(Problem!);

    public ActionResult Match(
        Func<ActionResult> onSuccess,
        Func<ProblemDetails, ActionResult> onFailure)
        => IsSuccess
            ? onSuccess()
            : onFailure(Problem!);

    public IResult Match(
        Func<IResult> onSuccess,
        Func<ProblemDetails, IResult> onFailure)
        => IsSuccess
            ? onSuccess()
            : onFailure(Problem!);

    public ActionResult ToActionResult(string? traceId = null)
        => IsSuccess
            ? new NoContentResult()
            : ResultFactory.ProblemActionResult(Problem!, traceId);

    public ActionResult ToActionResult(
        ControllerBase controller,
        string? traceId = null)
        => IsSuccess
            ? new NoContentResult()
            : ResultFactory.ProblemActionResult(Problem!, controller.HttpContext, traceId);

    public IResult ToIResult(string? traceId = null)
        => IsSuccess
            ? TypedResults.NoContent()
            : ResultFactory.ProblemIResult(Problem!, traceId);

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

    public ActionResult HandleFailure(Func<ProblemDetails, ActionResult> onFailure)
        => IsSuccess
            ? new NoContentResult()
            : onFailure(Problem!);

    public IResult HandleFailure(Func<ProblemDetails, IResult> onFailure)
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
            Problem = ResultFactory.NewProblem(
                statusCode,
                detail,
                extensions: extensions?.ToDictionary())
        };

    public static Result Failure(
        int statusCode,
        string? detail = null,
        string? type = null,
        string? title = null,
        string? instance = null,
        params (string key, object? value)[]? extensions)
        => new()
        {
            Problem = ResultFactory.NewProblem(
                statusCode,
                detail,
                type,
                title,
                instance,
                extensions?.ToDictionary())
        };

    public static Result Failure(
        int statusCode,
        string? detail = null,
        string? type = null,
        string? title = null,
        string? instance = null,
        IDictionary<string, object?>? extensions = null)
        => new()
        {
            Problem = ResultFactory.NewProblem(
                statusCode,
                detail,
                type,
                title,
                instance,
                extensions)
        };

    public static Result FailureCustom(
        string? type = null,
        string? title = null,
        int? statusCode = null,
        string? detail = null,
        string? instance = null,
        params (string key, object? value)[]? extensions)
        => new()
        {
            Problem = ResultFactory.NewProblemCustom(
                type,
                title,
                statusCode,
                detail,
                instance,
                extensions?.ToDictionary())
        };

    public static Result FailureCustom(
        string? type = null,
        string? title = null,
        int? statusCode = null,
        string? detail = null,
        string? instance = null,
        IDictionary<string, object?>? extensions = null)
        => new()
        {
            Problem = ResultFactory.NewProblemCustom(
                type,
                title,
                statusCode,
                detail,
                instance,
                extensions)
        };

    public static Result FailureCustom(ProblemDetails problem)
        => new() { Problem = problem };
}