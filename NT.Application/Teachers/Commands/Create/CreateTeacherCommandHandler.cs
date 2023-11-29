using ErrorOr;
using MediatR;
using NT.Application.Common.Interfaces.Persistence;
using NT.Application.Teachers.Common;
using NT.Domain.Common.Errors;
using NT.Domain.Teachers;

namespace NT.Application.Teachers.Commands.Create;

internal class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, ErrorOr<TeacherResult>>
{
    private readonly INTDbContext _context;

    public CreateTeacherCommandHandler(INTDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<TeacherResult>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(request.UserId);
        if (user is null)
            return Errors.User.ThisUserIsNotExist;

        var teacherResult = Teacher.Create(request.Name, request.Surname, request.Patronymic, user);
        if (teacherResult.IsError)
            return teacherResult.Errors;

        var teacher = teacherResult.Value;

        await _context.Teachers.AddAsync(teacher);
        await _context.SaveChangesAsync();

        return new TeacherResult(teacher.Name, teacher.Surname, teacher.Patronymic, teacher.UserId);
    }
}
