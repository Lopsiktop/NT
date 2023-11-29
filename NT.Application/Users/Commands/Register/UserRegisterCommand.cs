using ErrorOr;
using MediatR;
using NT.Application.Users.Common;

namespace NT.Application.Users.Commands.Register;

public record UserRegisterCommand(string Login, string Password) : IRequest<ErrorOr<UserResult>>;