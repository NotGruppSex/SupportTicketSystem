using Microsoft.Extensions.DependencyInjection;
using SupportTicketSystem.Application.Features.Customers;
using System;
using System.IO;

namespace SupportTicketSystem.Infrastructure.Customers;

public static class CustomerServiceRegistration
{
    public static IServiceCollection AddCustomerInfrastructure(
        this IServiceCollection services)
    {
        var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"SupportTicketSystem");

        var customerFilePath = Path.Combine(folderPath, "customers.json");
        
        services.AddTransient<ICustomerRepository>(_ => new JsonCustomerRepository(customerFilePath));

        return services;
    }
}
