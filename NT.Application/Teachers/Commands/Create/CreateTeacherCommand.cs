using ErrorOr;
using MediatR;
using NT.Application.Common.Interfaces.Persistence;
using NT.Application.Teachers.Common;

namespace NT.Application.Teachers.Commands.Create;

public record CreateTeacherCommand(string Name, string Surname, string Patronymic, int UserId, string Token) :
    IToken, IRequest<ErrorOr<TeacherResult>>;