namespace NT.Application.Common.Interfaces.Authentication;

public interface IJwtGenerator
{
    string GenerateToken(int userId, int roleId);
}
