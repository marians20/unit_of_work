using Microsoft.Extensions.DependencyInjection;
using Uow.ApplicationServices.Mappers;
using Uow.ApplicationServices.Services;
using Uow.PrimaryPorts;

namespace Uow.Application.Bootstrap;

public static class DiConfiguration
{
    public static IServiceCollection RegisterDomain(this IServiceCollection services) =>
        services
            .AddAutoMapper(typeof(UowMapperProfile))
            .AddScoped<IUserService, UserService>()
            .AddScoped<IRoleService, RoleService>();
}