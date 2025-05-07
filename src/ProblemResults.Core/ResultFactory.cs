using System.Collections.Generic;

namespace ProblemResults.Core
{
    public static class ResultFactory
    {
        public const string GenerateValue = "R2VuZXJhdGVWYWx1ZQ==";

        public static Problem NewProblem(
            int statusCode,
            string? detail = null,
            string? type = null,
            string? title = null,
            string? instance = null,
            IDictionary<string, object?>? extensions = null)
            => new()
            {
                Type = type ?? ProblemDefaults.Type(statusCode),
                Title = title ?? ProblemDefaults.Title(statusCode),
                Status = statusCode,
                Detail = detail,
                Instance = instance ?? GenerateValue,
                Extensions = extensions ?? new Dictionary<string, object?>()
            };

        public static Problem NewProblemCustom(
            string? type = null,
            string? title = null,
            int? statusCode = null,
            string? detail = null,
            string? instance = null,
            IDictionary<string, object?>? extensions = null)
            => new()
            {
                Type = type,
                Title = title,
                Status = statusCode ?? 500,
                Detail = detail,
                Instance = instance,
                Extensions = extensions ?? new Dictionary<string, object?>()
            };
    }
}