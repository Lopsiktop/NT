using NT.Domain.Users;

namespace NT.Application.Users.Common;

public record UserResult(int UserId, string Login, Role Role);
