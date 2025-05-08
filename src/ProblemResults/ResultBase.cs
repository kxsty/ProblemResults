using System;
using Microsoft.AspNetCore.Mvc;

namespace ProblemResults;

public class ResultBase
{
    public ProblemDetails? Problem { get; set; }

    protected bool IsSuccess => Problem == null;

    public void OnFailure<TResult>(Func<ProblemDetails, TResult> onFailure)
    {
        if (!IsSuccess) onFailure(Problem!);
    }
}