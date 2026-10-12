using Microsoft.Extensions.DependencyInjection;
using SupportTicketSystem.Application.Features.Customers;
using SupportTicketSystem.Application.Features.Overviews.Services;
using SupportTicketSystem.Application.Features.TicketRegistration.TicketRegistrationServices;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationInterfaces;
using SupportTicketSystem.Application.Features.TicketEditor;

namespace SupportTicketSystem.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<ITicketRegistrationService, TicketRegistrationService>();

        services.AddTransient<ITicketOverviewService, TicketOverviewService>();

        services.AddTransient<ITicketEditorService, TicketEditorService>();

        return services;
    }
}
