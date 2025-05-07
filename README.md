# ProblemResults
ProblemResults is a minimalistic result pattern library for .NET designed to simplify error returns in accordance with [RFC 9457](https://tools.ietf.org/html/rfc9457) (Problem Details) standard

## Installation
- [ProblemResults](https://www.nuget.org/packages/ProblemResults/) (Main package with ASP NET Core included, .NET 8)

Experimental packages:
- [ProblemResults.Core](https://www.nuget.org/packages/ProblemResults.Core/) (Package without dependencies on ASP NET Core, .NET 5)
- [ProblemResults.AspNetCore](https://www.nuget.org/packages/ProblemResults.AspNetCore/) (Based on Core package, adds support for ASP NET Core, .NET 7)

## Usage
### Create a Result
```csharp
Result.Success();

Result.Success(Value);

// The type, title, instance and traceId will be generated,
// but you can still add them and extensions
Result.Failure(
    StatusCodes.Status403Forbidden,
    "detail",
    ("key", "value")); // Convenient writing of optional extensions using tuples

// The type, title, instance and traceId will not be generated
// and you must add them yourself
Result.FailureCustom(  
    "type",
    "title",
    StatusCodes.Status404NotFound,
    "detail",
    "instance",
    ("key", "value"));

// You can add an existing problemDetails
Result.FailureCustom(problemDetails);
```
### Process the result
```csharp
// Returns your custom response
existingResult.Match(
    value => ,
    problem => );

// Executes code if there is a match, returns void
    existingResult.OnSuccess(
        value => );
    existingResult.OnFailure(
        problem => );

// Applies to all methods below:
// Returns Ok or NoContent (depending on the value present) on success
// and Problem with the given status code on failure 

existingResult.ToActionResult(this); // You can add your own traceId
existingResult.ToIResult(httpContext);

// Allows the user to define part of the result (e.g. if you define onSuccess
// and there is a failure, then onFailure will execute), returns ActionResult or IResult
existingResult.HandleSuccess(
    value => );
existingResult.HandleFailure(
    problem => );
```
### Example of http response
```json
{
    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
    "title": "Not Found",
    "status": 404,
    "detail": "Item not found", // Your text
    "instance": "DELETE /api/v1/items/{id}", // Actual id
    "traceId": "00-00000000000000000000000000000000-0000000000000000-00" // Actual traceId
}
```
