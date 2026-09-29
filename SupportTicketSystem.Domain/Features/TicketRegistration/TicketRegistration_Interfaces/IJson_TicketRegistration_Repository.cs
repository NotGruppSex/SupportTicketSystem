using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistration_Interfaces
{
    public interface IJson_TicketRegistration_Repository
    {
        Task SaveAllTicketsAsync(IEnumerable<TicketModel> tickets);

        Task<List<TicketModel>> GetAllTicketsAsync();
    }
}