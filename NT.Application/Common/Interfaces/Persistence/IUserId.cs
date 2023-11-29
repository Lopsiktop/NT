namespace NT.Application.Common.Interfaces.Persistence;

public abstract class TToken
{
    public string Token { get; set; }
}

public interface IToken
{
    string Token { get; }
}