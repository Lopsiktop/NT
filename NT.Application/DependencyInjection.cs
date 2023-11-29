using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NT.Application.Common.Behaviors;

namespace NT.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UserIsNotInBlackListBehavior<,>));
        return services;
    }
}
