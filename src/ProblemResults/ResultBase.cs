using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProblemResults;

public class ResultBase
{
    public ProblemDetails? Problem { get; set; }

    protected bool IsSuccess => Problem == null;

    public ActionResult HandleFailure(Func<ProblemDetails, ActionResult> onFailure)
        => IsSuccess
            ? new OkResult()
            : onFailure(Problem!);

    public IResult HandleFailure(Func<ProblemDetails, IResult> onFailure)
        => IsSuccess
            ? TypedResults.NoContent()
            : onFailure(Problem!);

    public void OnFailure<TResult>(Func<ProblemDetails, TResult> onFailure)
    {
        if (!IsSuccess) onFailure(Problem!);
    }
}