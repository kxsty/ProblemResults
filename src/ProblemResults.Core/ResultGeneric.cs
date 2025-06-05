using System;

namespace ProblemResults.Core
{
    public class Result<T> : ResultBase
    {
        public T? Value { get; init; }

        public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Problem, TResult> onFailure)
            => IsSuccess
                ? onSuccess(Value!)
                : onFailure(Problem!);

        public void OnSuccess(Action<T> onSuccess)
        {
            if (IsSuccess) onSuccess(Value!);
        }

        public static implicit operator Result<T>(Result result)
            => new() { Problem = result.Problem };
        
        public static implicit operator Result<T>(T value)
            => new() { Value = value };
    }
}