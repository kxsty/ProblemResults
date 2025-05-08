using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemResults.Core;

namespace ProblemResults.AspNetCore;

public static class ResultExt
{
    public static ActionResult Match(
        this Result result,
        Func<ActionResult> onSuccess,
        Func<Problem, ActionResult> onFailure)
        => result.IsSuccess
            ? onSuccess()
            : onFailure(result.Problem!);

    public static IResult Match(
        this Result result,
        Func<IResult> onSuccess,
        Func<Problem, IResult> onFailure)
        => result.IsSuccess
            ? onSuccess()
            : onFailure(result.Problem!);

    public static ActionResult ToActionResult(
        this Result result,
        string? traceId = null)
        => result.IsSuccess
            ? new NoContentResult()
            : ResultFactoryExt.ProblemActionResult(result.Problem!, traceId);

    public static ActionResult ToActionResult(
        this Result result,
        ControllerBase controller,
        string? traceId = null)
        => result.IsSuccess
            ? new NoContentResult()
            : ResultFactoryExt.ProblemActionResult(result.Problem!, controller.HttpContext, traceId);

    public static IResult ToIResult(
        this Result result,
        string? traceId = null)
        => result.IsSuccess
            ? TypedResults.NoContent()
            : ResultFactoryExt.ProblemIResult(result.Problem!, traceId);

    public static IResult ToIResult(
        this Result result,
        HttpContext httpContext,
        string? traceId = null)
        => result.IsSuccess
            ? TypedResults.NoContent()
            : ResultFactoryExt.ProblemIResult(result.Problem!, httpContext, traceId);

    public static ActionResult HandleSuccess(
        this Result result,
        Func<ActionResult> onSuccess,
        ControllerBase controller)
        => result.IsSuccess
            ? onSuccess()
            : ResultFactoryExt.ProblemActionResult(result.Problem!, controller.HttpContext);

    public static IResult HandleSuccess(
        this Result result,
        Func<IResult> onSuccess,
        HttpContext httpContext)
        => result.IsSuccess
            ? onSuccess()
            : ResultFactoryExt.ProblemIResult(result.Problem!, httpContext);

    public static ActionResult HandleFailure(
        this Result resultBase,
        Func<Problem, ActionResult> onFailure)
        => resultBase.IsSuccess
            ? new NoContentResult()
            : onFailure(resultBase.Problem!);

    public static IResult HandleFailure(
        this Result resultBase,
        Func<Problem, IResult> onFailure)
        => resultBase.IsSuccess
            ? TypedResults.NoContent()
            : onFailure(resultBase.Problem!);
}