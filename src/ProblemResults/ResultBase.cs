using System;

namespace ProblemResults;

public class ResultBase
{
    public Problem? Problem { get; init; }

    protected bool IsSuccess => Problem == null;

    public void OnFailure(Action<Problem> onFailure)
    {
        if (!IsSuccess) onFailure(Problem!);
    }
}