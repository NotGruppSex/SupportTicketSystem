using Microsoft.Extensions.DependencyInjection;
using SupportTicketSystem.Infrastructure.Features.Customers;

namespace SupportTicketSystem.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddCustomerInfrastructure();

        return services;
    }
}