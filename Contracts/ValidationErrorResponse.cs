using System.Collections.Generic;

namespace CaseStudy.Contracts;

public sealed class ValidationErrorResponse
{
    public ValidationErrorResponse(IDictionary<string, string[]> errors)
    {
        Errors = errors;
    }

    public string Message { get; } = "One or more validation errors occurred.";
    public IDictionary<string, string[]> Errors { get; }
}
