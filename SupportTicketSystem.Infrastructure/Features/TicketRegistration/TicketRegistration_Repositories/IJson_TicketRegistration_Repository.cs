using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportTicketSystem.Infrastructure.Features.TicketRegistration.TicketRegistrationRepositories
{
    public interface IJson_TicketRegistration_Repository
    {
        Task SaveAllTicketsAsync(IEnumerable<TicketModel> tickets);
    }
}