using System;

namespace ProblemResults.Core
{
    public class Result<T> : ResultBase
    {
        public T? Value { get; set; }

        public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Problem, TResult> onFailure)
            => IsSuccess
                ? onSuccess(Value!)
                : onFailure(Problem!);

        public void OnSuccess<TResult>(Func<T, TResult> onSuccess)
        {
            if (IsSuccess) onSuccess(Value!);
        }

        public static implicit operator Result<T>(Result result)
            => new() { Problem = result.Problem };
    }
}