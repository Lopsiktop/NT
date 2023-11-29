using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NT.Application.Common.Interfaces.Authentication;
using NT.Application.Common.Interfaces.Persistence;
using NT.Application.Users.Common;
using NT.Domain.Common.Errors;

namespace NT.Application.Users.Queries.Login;

internal class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, ErrorOr<UserTokenResult>>
{
    private readonly INTDbContext _context;
    private readonly IJwtGenerator _jwtGenerator;

    public UserLoginQueryHandler(INTDbContext context, IJwtGenerator jwtGenerator)
    {
        _context = context;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<ErrorOr<UserTokenResult>> Handle(UserLoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Login == request.Login);
        if (user is null)
            return Errors.User.WrongLoginOrPassword;

        if (!user.Verify(request.Password))
            return Errors.User.WrongLoginOrPassword;

        var token = _jwtGenerator.GenerateToken(user.Id, (int)user.Role);

        return new UserTokenResult(user.Id, user.Login, user.Role, token);
    }
}
