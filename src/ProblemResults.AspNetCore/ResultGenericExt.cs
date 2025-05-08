using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemResults.Core;

namespace ProblemResults.AspNetCore;

public static class ResultGenericExt
{
    public static ActionResult<T> Match<T>(
        this Result<T> result,
        Func<T, ActionResult<T>> onSuccess,
        Func<Problem, ActionResult<T>> onFailure)
        => result.IsSuccess
            ? onSuccess(result.Value!)
            : onFailure(result.Problem!);

    public static IResult Match<T>(
        this Result<T> result,
        Func<T, IResult> onSuccess,
        Func<Problem, IResult> onFailure)
        => result.IsSuccess
            ? onSuccess(result.Value!)
            : onFailure(result.Problem!);

    public static ActionResult ToActionResult<T>(
        this Result<T> result,
        string? traceId = null)
        => result.IsSuccess
            ? new OkObjectResult(result.Value)
            : ResultFactoryExt.ProblemActionResult(result.Problem!, traceId);

    public static ActionResult ToActionResult<T>(
        this Result<T> result,
        ControllerBase controller,
        string? traceId = null)
        => result.IsSuccess
            ? new OkObjectResult(result.Value)
            : ResultFactoryExt.ProblemActionResult(result.Problem!, controller.HttpContext, traceId);

    public static IResult ToIResult<T>(
        this Result<T> result,
        string? traceId = null)
        => result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : ResultFactoryExt.ProblemIResult(result.Problem!, traceId);

    public static IResult ToIResult<T>(
        this Result<T> result,
        HttpContext httpContext,
        string? traceId = null)
        => result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : ResultFactoryExt.ProblemIResult(result.Problem!, httpContext, traceId);

    public static ActionResult HandleSuccess<T>(
        this Result<T> result,
        Func<T, ActionResult> onSuccess,
        ControllerBase controller)
        => result.IsSuccess
            ? onSuccess(result.Value!)
            : ResultFactoryExt.ProblemActionResult(result.Problem!, controller.HttpContext);

    public static IResult HandleSuccess<T>(
        this Result<T> result,
        Func<T, IResult> onSuccess,
        HttpContext httpContext)
        => result.IsSuccess
            ? onSuccess(result.Value!)
            : ResultFactoryExt.ProblemIResult(result.Problem!, httpContext);

    public static ActionResult HandleFailure<T>(
        this Result<T> result,
        Func<Problem, ActionResult> onFailure)
        => result.IsSuccess
            ? new OkObjectResult(result.Value)
            : onFailure(result.Problem!);

    public static IResult HandleFailure<T>(
        this Result<T> result,
        Func<Problem, IResult> onFailure)
        => result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : onFailure(result.Problem!);
}