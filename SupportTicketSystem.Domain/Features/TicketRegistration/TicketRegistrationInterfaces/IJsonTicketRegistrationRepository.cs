using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationInterfaces
{
    public interface IJsonTicketRegistrationRepository
    {
        Task SaveAllTicketsAsync(List<TicketModel> tickets);

        Task<List<TicketModel>> GetAllTicketsAsync();
    }
}