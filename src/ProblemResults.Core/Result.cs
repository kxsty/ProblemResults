using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemResults.Core
{
    public class Result : ResultBase
    {
        // This non-static methods are not included in ResultBase to prevent the user
        // from selecting methods without a value in Result<T>
        public TResult Match<TResult>(Func<TResult> onSuccess, Func<Problem, TResult> onFailure)
            => IsSuccess
                ? onSuccess()
                : onFailure(Problem!);

        public void OnSuccess(Action onSuccess)
        {
            if (IsSuccess) onSuccess();
        }

        public static Result Success()
            => new();

        public static Result<T> Success<T>(T value)
            => new() { Value = value };

        public static Result Failure(
            int statusCode,
            string? detail = null,
            params (string key, object? value)[]? extensions)
            => new()
            {
                Problem = new Problem(
                    statusCode,
                    detail,
                    extensions: extensions?.ToDictionary(tuple => tuple.key, tuple => tuple.value))
            };
        
        public static Result Failure(
            int statusCode,
            string? detail = null,
            IDictionary<string, object?>? extensions = null)
            => new()
            {
                Problem = new Problem(
                    statusCode,
                    detail,
                    extensions: extensions)
            };

        public static Result FailureCustom(Problem problem)
            => new() { Problem = problem };
    }
}