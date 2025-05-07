using System;

namespace ProblemResults.Core
{
    public class ResultBase
    {
        public Problem? Problem { get; set; }

        public bool IsSuccess => Problem == null;

        public void OnFailure<TResult>(Func<Problem, TResult> onFailure)
        {
            if (!IsSuccess) onFailure(Problem!);
        }
    }
}