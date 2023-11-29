using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NT.Application.Common.Interfaces.Persistence;
using NT.Application.Users.Common;
using NT.Domain.Common.Errors;
using NT.Domain.Users;

namespace NT.Application.Users.Commands.Register;

internal class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, ErrorOr<UserResult>>
{
    private readonly INTDbContext _context;

    public UserRegisterCommandHandler(INTDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<UserResult>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var user_is_exist = await _context.Users.SingleOrDefaultAsync(x => x.Login == request.Login);
        if (user_is_exist is not null)
            return Errors.User.ThisUserIsAlreadyExist;

        var user = User.Create(request.Login, request.Password);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return new UserResult(user.Id, user.Login, user.Role);
    }
}
