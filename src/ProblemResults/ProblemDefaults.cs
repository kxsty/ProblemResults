// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;

namespace ProblemResults;

public static class ProblemDefaults
{
    private static readonly Dictionary<ProblemCodes, (string Type, string Title)> Defaults = new()
    {
        [ProblemCodes.Status400BadRequest] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.1", "Bad Request"),
        [ProblemCodes.Status401Unauthorized] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.2", "Unauthorized"),
        [ProblemCodes.Status402PaymentRequired] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.3", "Payment Required"),
        [ProblemCodes.Status403Forbidden] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.4", "Forbidden"),
        [ProblemCodes.Status404NotFound] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.5", "Not Found"),
        [ProblemCodes.Status405MethodNotAllowed] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.6", "Method Not Allowed"),
        [ProblemCodes.Status406NotAcceptable] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.7", "Not Acceptable"),
        [ProblemCodes.Status407ProxyAuthenticationRequired] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.8", "Proxy Authentication Required"),
        [ProblemCodes.Status408RequestTimeout] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.9", "Request Timeout"),
        [ProblemCodes.Status409Conflict] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.10", "Conflict"),
        [ProblemCodes.Status410Gone] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.11", "Gone"),
        [ProblemCodes.Status411LengthRequired] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.12", "Length Required"),
        [ProblemCodes.Status412PreconditionFailed] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.13", "Precondition Failed"),
        [ProblemCodes.Status413PayloadTooLarge] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.14", "Content Too Large"),
        [ProblemCodes.Status414UriTooLong] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.15", "URI Too Long"),
        [ProblemCodes.Status415UnsupportedMediaType] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.16", "Unsupported Media Type"),
        [ProblemCodes.Status416RangeNotSatisfiable] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.17", "Range Not Satisfiable"),
        [ProblemCodes.Status417ExpectationFailed] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.18", "Expectation Failed"),
        [ProblemCodes.Status418ImATeapot] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.19", "(Unused)"),
        [ProblemCodes.Status421MisdirectedRequest] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.20", "Misdirected Request"),
        [ProblemCodes.Status422UnprocessableEntity] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.21", "Unprocessable Entity"),
        [ProblemCodes.Status426UpgradeRequired] =
            ("https://tools.ietf.org/html/rfc9110#section-15.5.22", "Upgrade Required"),

        [ProblemCodes.Status500InternalServerError] =
            ("https://tools.ietf.org/html/rfc9110#section-15.6.1", "Internal Server Error"),
        [ProblemCodes.Status501NotImplemented] =
            ("https://tools.ietf.org/html/rfc9110#section-15.6.2", "Not Implemented"),
        [ProblemCodes.Status502BadGateway] =
            ("https://tools.ietf.org/html/rfc9110#section-15.6.3", "Bad Gateway"),
        [ProblemCodes.Status503ServiceUnavailable] =
            ("https://tools.ietf.org/html/rfc9110#section-15.6.4", "Service Unavailable"),
        [ProblemCodes.Status504GatewayTimeout] =
            ("https://tools.ietf.org/html/rfc9110#section-15.6.5", "Gateway Timeout"),
        [ProblemCodes.Status505HttpVersionNotsupported] =
            ("https://tools.ietf.org/html/rfc9110#section-15.6.6", "HTTP Version Not Supported")
    };

    public static string Type(ProblemCodes statusCode)
        => Defaults.TryGetValue(statusCode, out (string Type, string Title) value)
            ? value.Type
            : string.Empty;

    public static string Title(ProblemCodes statusCode)
        => Defaults.TryGetValue(statusCode, out (string Type, string Title) value)
            ? value.Title
            : string.Empty;
}