using Microsoft.Extensions.DependencyInjection;
using SupportTicketSystem.Infrastructure.Features.Customers;
using SupportTicketSystem.Infrastructure.Features.TicketRegistration;

namespace SupportTicketSystem.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddCustomerInfrastructure();
        services.AddTicketRegistrationInfrastructure();

        return services;
    }
}