using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemResults.Core;

namespace ProblemResults.AspNetCore;

public static class ResultBase
{
    public static ActionResult HandleFailure(
        this Core.ResultBase resultBase,
        Func<Problem, ActionResult> onFailure)
        => resultBase.IsSuccess
            ? new OkResult()
            : onFailure(resultBase.Problem!);

    public static IResult HandleFailure(
        this Core.ResultBase resultBase,
        Func<Problem, IResult> onFailure)
        => resultBase.IsSuccess
            ? TypedResults.NoContent()
            : onFailure(resultBase.Problem!);
}