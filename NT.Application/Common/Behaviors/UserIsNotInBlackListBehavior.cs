using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NT.Application.Common.Interfaces.Persistence;
using NT.Domain.Common.Errors;

namespace NT.Application.Common.Behaviors;

public class UserIsNotInBlackListBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : TToken, IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly INTDbContext _context;

    public UserIsNotInBlackListBehavior(INTDbContext context)
    {
        _context = context;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var token = await _context.BlackLists.SingleOrDefaultAsync(x => x.Token == request.Token);
        if (token is null)
            return await next();

        return (dynamic)Errors.User.ThisTokenIsDisabled;
    }
}
