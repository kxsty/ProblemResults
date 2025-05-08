using System;

namespace ProblemResults.Core
{
    public class ResultBase
    {
        public Problem? Problem { get; set; }

        public bool IsSuccess => Problem == null;

        public void OnFailure(Action<Problem> onFailure)
        {
            if (!IsSuccess) onFailure(Problem!);
        }
    }
}