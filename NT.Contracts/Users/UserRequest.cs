namespace NT.Contracts.Users;

public record UserRequest(string Login, string Password) : Request;