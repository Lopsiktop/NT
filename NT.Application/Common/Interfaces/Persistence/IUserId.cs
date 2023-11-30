namespace NT.Application.Common.Interfaces.Persistence;

public record TToken(string Token)
{
    public string Token { get; set; }
}

public interface IToken
{
    string Token { get; }
}