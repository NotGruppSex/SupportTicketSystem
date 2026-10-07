using Microsoft.Extensions.DependencyInjection;
using SupportTicketSystem.Application.Features.Customers;

namespace SupportTicketSystem.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<ICustomerService, CustomerService>();

        return services;
    }
}
