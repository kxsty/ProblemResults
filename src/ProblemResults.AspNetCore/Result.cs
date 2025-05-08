using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemResults.Core;

namespace ProblemResults.AspNetCore;

public static class Result
{
    public static ActionResult Match(
        this Core.Result result,
        Func<ActionResult> onSuccess,
        Func<Problem, ActionResult> onFailure)
        => result.IsSuccess
            ? onSuccess()
            : onFailure(result.Problem!);

    public static IResult Match(
        this Core.Result result,
        Func<IResult> onSuccess,
        Func<Problem, IResult> onFailure)
        => result.IsSuccess
            ? onSuccess()
            : onFailure(result.Problem!);

    public static ActionResult ToActionResult(
        this Core.Result result,
        string? traceId = null)
        => result.IsSuccess
            ? new NoContentResult()
            : ResultFactory.ProblemActionResult(result.Problem!, traceId);

    public static ActionResult ToActionResult(
        this Core.Result result,
        ControllerBase controller,
        string? traceId = null)
        => result.IsSuccess
            ? new NoContentResult()
            : ResultFactory.ProblemActionResult(result.Problem!, controller.HttpContext, traceId);

    public static IResult ToIResult(
        this Core.Result result,
        string? traceId = null)
        => result.IsSuccess
            ? TypedResults.NoContent()
            : ResultFactory.ProblemIResult(result.Problem!, traceId);

    public static IResult ToIResult(
        this Core.Result result,
        HttpContext httpContext,
        string? traceId = null)
        => result.IsSuccess
            ? TypedResults.NoContent()
            : ResultFactory.ProblemIResult(result.Problem!, httpContext, traceId);

    public static ActionResult HandleSuccess(
        this Core.Result result,
        Func<ActionResult> onSuccess,
        ControllerBase controller)
        => result.IsSuccess
            ? onSuccess()
            : ResultFactory.ProblemActionResult(result.Problem!, controller.HttpContext);

    public static IResult HandleSuccess(
        this Core.Result result,
        Func<IResult> onSuccess,
        HttpContext httpContext)
        => result.IsSuccess
            ? onSuccess()
            : ResultFactory.ProblemIResult(result.Problem!, httpContext);

    public static ActionResult HandleFailure(
        this Core.Result resultBase,
        Func<Problem, ActionResult> onFailure)
        => resultBase.IsSuccess
            ? new NoContentResult()
            : onFailure(resultBase.Problem!);

    public static IResult HandleFailure(
        this Core.Result resultBase,
        Func<Problem, IResult> onFailure)
        => resultBase.IsSuccess
            ? TypedResults.NoContent()
            : onFailure(resultBase.Problem!);
}