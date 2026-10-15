
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
        // Anger vilken fil appen ska använda för lagring av ärenden.
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"SupportTicketSystem","tickets.json");

        // Skapar repositoryt med den sökväg som konstruktorn kräver.
        services.AddTransient<IJsonTicketRegistrationRepository>(_ => new JsonTicketRegistrationRepository(filePath));

        return services;
    }
}
