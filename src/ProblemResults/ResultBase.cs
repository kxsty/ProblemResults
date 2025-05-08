using System;
using Microsoft.AspNetCore.Mvc;

namespace ProblemResults;

public class ResultBase
{
    public ProblemDetails? Problem { get; set; }

    protected bool IsSuccess => Problem == null;

    public void OnFailure(Action<ProblemDetails> onFailure)
    {
        if (!IsSuccess) onFailure(Problem!);
    }
}