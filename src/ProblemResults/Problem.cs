using System;
using System.Collections.Generic;

namespace ProblemResults;

public class Problem
{
    internal Problem(int status, string? detail = null, IDictionary<string, object?>? extensions = null)
    {
        Status = status;
        Detail = detail;

        if (extensions != null)
            foreach (KeyValuePair<string, object?> extension in extensions)
                Extensions.Add(extension);
    }

    public int Status { get; init; }

    public string? Detail { get; init; }

    public IDictionary<string, object?> Extensions { get; } =
        new Dictionary<string, object?>(StringComparer.Ordinal);
}