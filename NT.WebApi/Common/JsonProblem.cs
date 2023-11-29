namespace NT.WebApi.Common;

public record JsonProblem(string Title, string Description, int StatusCode, JsonError[] Errors);
