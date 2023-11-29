using NT.Domain.Users;

namespace NT.Application.Users.Common;

public record UserTokenResult(int UserId, string Login, Role Role, string Token);
