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
                Problem = ResultFactory.NewProblem(
                    statusCode,
                    detail,
                    extensions: extensions?.ToDictionary(tuple => tuple.key, tuple => tuple.value))
            };

        public static Result Failure(
            int statusCode,
            string? detail = null,
            string? type = null,
            string? title = null,
            string? instance = null,
            params (string key, object? value)[]? extensions)
            => new()
            {
                Problem = ResultFactory.NewProblem(
                    statusCode,
                    detail,
                    type,
                    title,
                    instance,
                    extensions?.ToDictionary(tuple => tuple.key, tuple => tuple.value))
            };

        public static Result Failure(
            int statusCode,
            string? detail = null,
            string? type = null,
            string? title = null,
            string? instance = null,
            IDictionary<string, object?>? extensions = null)
            => new()
            {
                Problem = ResultFactory.NewProblem(
                    statusCode,
                    detail,
                    type,
                    title,
                    instance,
                    extensions)
            };

        public static Result FailureCustom(
            string? type = null,
            string? title = null,
            int? statusCode = null,
            string? detail = null,
            string? instance = null,
            params (string key, object? value)[]? extensions)
            => new()
            {
                Problem = ResultFactory.NewProblemCustom(
                    type,
                    title,
                    statusCode,
                    detail,
                    instance,
                    extensions?.ToDictionary(tuple => tuple.key, tuple => tuple.value))
            };

        public static Result FailureCustom(
            string? type = null,
            string? title = null,
            int? statusCode = null,
            string? detail = null,
            string? instance = null,
            IDictionary<string, object?>? extensions = null)
            => new()
            {
                Problem = ResultFactory.NewProblemCustom(
                    type,
                    title,
                    statusCode,
                    detail,
                    instance,
                    extensions)
            };

        public static Result FailureCustom(Problem problem)
            => new() { Problem = problem };
    }
}