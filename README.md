# ProblemResults
ProblemResults is a minimalistic result pattern library for .NET designed to simplify error returns in accordarnce with [RFC 9457](https://tools.ietf.org/html/rfc9457) (Problem Details) standard.

## Installation
- [NuGet](https://www.nuget.org/packages/ProblemResults/)

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

existingResult.ToActionResult(this); // Use “this” in the controller and “httpContext” in the minimal api to generate the instance field
existingResult.ToIResult(httpContext); // Also you can add your own traceId

// Allows the user to define part of the result (e.g. if you define onSuccess
// and there is a failure, then onFailure will execute), returns ActionResult or IResult
existingResult.HandleSuccess(
    value => , this);
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
