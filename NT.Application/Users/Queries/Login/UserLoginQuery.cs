using ErrorOr;
using MediatR;
using NT.Application.Users.Common;

namespace NT.Application.Users.Queries.Login;

public record UserLoginQuery(string Login, string Password) : IRequest<ErrorOr<UserTokenResult>>;
