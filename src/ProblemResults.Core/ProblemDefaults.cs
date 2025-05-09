// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;

namespace ProblemResults.Core
{
    public static class ProblemDefaults
    {
        private static readonly Dictionary<int, (string Type, string Title)> Defaults = new()
        {
            [100] = ("https://tools.ietf.org/html/rfc9110#section-15.2.1", "Continue"),
            [101] = ("https://tools.ietf.org/html/rfc9110#section-15.2.2", "Switching Protocols"),

            [200] = ("https://tools.ietf.org/html/rfc9110#section-15.3.1", "OK"),
            [201] = ("https://tools.ietf.org/html/rfc9110#section-15.3.2", "Created"),
            [202] = ("https://tools.ietf.org/html/rfc9110#section-15.3.3", "Accepted"),
            [203] = ("https://tools.ietf.org/html/rfc9110#section-15.3.4", "Non-Authoritative Information"),
            [204] = ("https://tools.ietf.org/html/rfc9110#section-15.3.5", "No Content"),
            [205] = ("https://tools.ietf.org/html/rfc9110#section-15.3.6", "Reset Content"),
            [206] = ("https://tools.ietf.org/html/rfc9110#section-15.3.7", "Partial Content"),

            [300] = ("https://tools.ietf.org/html/rfc9110#section-15.4.1", "Multiple Choices"),
            [301] = ("https://tools.ietf.org/html/rfc9110#section-15.4.2", "Moved Permanently"),
            [302] = ("https://tools.ietf.org/html/rfc9110#section-15.4.3", "Found"),
            [303] = ("https://tools.ietf.org/html/rfc9110#section-15.4.4", "See Other"),
            [304] = ("https://tools.ietf.org/html/rfc9110#section-15.4.5", "Not Modified"),
            [305] = ("https://tools.ietf.org/html/rfc9110#section-15.4.6", "Use Proxy"),
            [306] = ("https://tools.ietf.org/html/rfc9110#section-15.4.7", "(Unused)"),
            [307] = ("https://tools.ietf.org/html/rfc9110#section-15.4.8", "Temporary Redirect"),
            [308] = ("https://tools.ietf.org/html/rfc9110#section-15.4.9", "Permanent Redirect"),

            [400] = ("https://tools.ietf.org/html/rfc9110#section-15.5.1", "Bad Request"),
            [401] = ("https://tools.ietf.org/html/rfc9110#section-15.5.2", "Unauthorized"),
            [402] = ("https://tools.ietf.org/html/rfc9110#section-15.5.3", "Payment Required"),
            [403] = ("https://tools.ietf.org/html/rfc9110#section-15.5.4", "Forbidden"),
            [404] = ("https://tools.ietf.org/html/rfc9110#section-15.5.5", "Not Found"),
            [405] = ("https://tools.ietf.org/html/rfc9110#section-15.5.6", "Method Not Allowed"),
            [406] = ("https://tools.ietf.org/html/rfc9110#section-15.5.7", "Not Acceptable"),
            [407] = ("https://tools.ietf.org/html/rfc9110#section-15.5.8", "Proxy Authentication Required"),
            [408] = ("https://tools.ietf.org/html/rfc9110#section-15.5.9", "Request Timeout"),
            [409] = ("https://tools.ietf.org/html/rfc9110#section-15.5.10", "Conflict"),
            [410] = ("https://tools.ietf.org/html/rfc9110#section-15.5.11", "Gone"),
            [411] = ("https://tools.ietf.org/html/rfc9110#section-15.5.12", "Length Required"),
            [412] = ("https://tools.ietf.org/html/rfc9110#section-15.5.13", "Precondition Failed"),
            [413] = ("https://tools.ietf.org/html/rfc9110#section-15.5.14", "Content Too Large"),
            [414] = ("https://tools.ietf.org/html/rfc9110#section-15.5.15", "URI Too Long"),
            [415] = ("https://tools.ietf.org/html/rfc9110#section-15.5.16", "Unsupported Media Type"),
            [416] = ("https://tools.ietf.org/html/rfc9110#section-15.5.17", "Range Not Satisfiable"),
            [417] = ("https://tools.ietf.org/html/rfc9110#section-15.5.18", "Expectation Failed"),
            [418] = ("https://tools.ietf.org/html/rfc9110#section-15.5.19", "(Unused)"),
            [421] = ("https://tools.ietf.org/html/rfc9110#section-15.5.20", "Misdirected Request"),
            [422] = ("https://tools.ietf.org/html/rfc9110#section-15.5.21", "Unprocessable Entity"),
            [426] = ("https://tools.ietf.org/html/rfc9110#section-15.5.22", "Upgrade Required"),

            [500] = ("https://tools.ietf.org/html/rfc9110#section-15.6.1", "Internal Server Error"),
            [501] = ("https://tools.ietf.org/html/rfc9110#section-15.6.2", "Not Implemented"),
            [502] = ("https://tools.ietf.org/html/rfc9110#section-15.6.3", "Bad Gateway"),
            [503] = ("https://tools.ietf.org/html/rfc9110#section-15.6.4", "Service Unavailable"),
            [504] = ("https://tools.ietf.org/html/rfc9110#section-15.6.5", "Gateway Timeout"),
            [505] = ("https://tools.ietf.org/html/rfc9110#section-15.6.6", "HTTP Version Not Supported")
        };

        public static string Type(int statusCode)
            => Defaults.TryGetValue(statusCode, out (string Type, string Title) value)
                ? value.Type
                : string.Empty;

        public static string Title(int statusCode)
            => Defaults.TryGetValue(statusCode, out (string Type, string Title) value)
                ? value.Title
                : string.Empty;
    }
}