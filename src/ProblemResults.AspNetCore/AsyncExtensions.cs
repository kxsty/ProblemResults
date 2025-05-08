using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemResults.Core;

namespace ProblemResults.AspNetCore;

public static class AsyncExtensions
{
    // Match
    public static async Task<TResult> Match<TResult>(
        this Task<Result> result,
        Func<TResult> onSuccess,
        Func<Problem, TResult> onFailure)
        => (await result).Match(onSuccess, onFailure);

    public static async Task<TResult> Match<TResult, T>(
        this Task<Result<T>> result,
        Func<T, TResult> onSuccess,
        Func<Problem, TResult> onFailure)
        => (await result).Match(onSuccess, onFailure);

    public static async Task<ActionResult> Match(
        this Task<Result> result,
        Func<ActionResult> onSuccess,
        Func<Problem, ActionResult> onFailure)
        => (await result).Match(onSuccess, onFailure);

    public static async Task<ActionResult<T>> Match<T>(
        this Task<Result<T>> result,
        Func<T, ActionResult<T>> onSuccess,
        Func<Problem, ActionResult<T>> onFailure)
        => (await result).Match(onSuccess, onFailure);

    public static async Task<IResult> Match(
        this Task<Result> result,
        Func<IResult> onSuccess,
        Func<Problem, IResult> onFailure)
        => (await result).Match(onSuccess, onFailure);

    public static async Task<IResult> Match<T>(
        this Task<Result<T>> result,
        Func<T, IResult> onSuccess,
        Func<Problem, IResult> onFailure)
        => (await result).Match(onSuccess, onFailure);

    // ToActionResult
    public static async Task<ActionResult> ToActionResult(
        this Task<Result> result,
        string? traceId = null)
        => (await result).ToActionResult(traceId);

    public static async Task<ActionResult> ToActionResult<T>(
        this Task<Result<T>> result,
        string? traceId = null)
        => (await result).ToActionResult(traceId);

    public static async Task<ActionResult> ToActionResult(
        this Task<Result> result,
        ControllerBase controller,
        string? traceId = null)
        => (await result).ToActionResult(controller, traceId);

    public static async Task<ActionResult> ToActionResult<T>(
        this Task<Result<T>> result,
        ControllerBase controller,
        string? traceId = null)
        => (await result).ToActionResult(controller, traceId);

    // ToIResult
    public static async Task<IResult> ToIResult(
        this Task<Result> result,
        string? traceId = null)
        => (await result).ToIResult(traceId);

    public static async Task<IResult> ToIResult<T>(
        this Task<Result<T>> result,
        string? traceId = null)
        => (await result).ToIResult(traceId);

    public static async Task<IResult> ToIResult(
        this Task<Result> result,
        HttpContext httpContext,
        string? traceId = null)
        => (await result).ToIResult(httpContext, traceId);

    public static async Task<IResult> ToIResult<T>(
        this Task<Result<T>> result,
        HttpContext httpContext,
        string? traceId = null)
        => (await result).ToIResult(httpContext, traceId);

    // HandleSuccess
    public static async Task<ActionResult> HandleSuccess(
        this Task<Result> result,
        Func<ActionResult> onSuccess,
        ControllerBase controller)
        => (await result).HandleSuccess(onSuccess, controller);

    public static async Task<ActionResult> HandleSuccess<T>(
        this Task<Result<T>> result,
        Func<T, ActionResult> onSuccess,
        ControllerBase controller)
        => (await result).HandleSuccess(onSuccess, controller);

    public static async Task<IResult> HandleSuccess(
        this Task<Result> result,
        Func<IResult> onSuccess,
        HttpContext httpContext)
        => (await result).HandleSuccess(onSuccess, httpContext);

    public static async Task<IResult> HandleSuccess<T>(
        this Task<Result<T>> result,
        Func<T, IResult> onSuccess,
        HttpContext httpContext)
        => (await result).HandleSuccess(onSuccess, httpContext);

    // HandleFailure
    public static async Task<ActionResult> HandleFailure(
        this Task<Result> result,
        Func<Problem, ActionResult> onFailure)
        => (await result).HandleFailure(onFailure);

    public static async Task<ActionResult> HandleFailure<T>(
        this Task<Result<T>> result,
        Func<Problem, ActionResult> onFailure)
        => (await result).HandleFailure(onFailure);

    public static async Task<IResult> HandleFailure(
        this Task<Result> result,
        Func<Problem, IResult> onFailure)
        => (await result).HandleFailure(onFailure);

    public static async Task<IResult> HandleFailure<T>(
        this Task<Result<T>> result,
        Func<Problem, IResult> onFailure)
        => (await result).HandleFailure(onFailure);

    // OnSuccess
    public static async Task OnSuccess<TResult>(
        this Task<Result> result,
        Func<TResult> onSuccess)
        => (await result).OnSuccess(onSuccess);

    public static async Task OnSuccess<TResult, T>(
        this Task<Result<T>> result,
        Func<T, TResult> onSuccess)
        => (await result).OnSuccess(onSuccess);

    // OnFailure
    public static async Task OnFailure<TResult>(
        this Task<Result> result,
        Func<Problem, TResult> onFailure)
        => (await result).OnFailure(onFailure);

    public static async Task OnFailure<TResult, T>(
        this Task<Result<T>> result,
        Func<Problem, TResult> onFailure)
        => (await result).OnFailure(onFailure);
}