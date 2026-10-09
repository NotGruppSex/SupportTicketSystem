
using Microsoft.Extensions.DependencyInjection;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationInterfaces;
using SupportTicketSystem.Infrastructure.Features.TicketRegistration.TicketRegistrationRepositories;
using System;
using System.IO;

namespace SupportTicketSystem.Infrastructure.Features.TicketRegistration;
public static class TicketRegistrationServiceRegistration
{
    public static IServiceCollection AddTicketRegistrationInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IJsonTicketRegistrationRepository, JsonTicketRegistrationRepository>();
        return services;
    }
}
