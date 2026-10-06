using System.Threading.Tasks;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistration_dtos;

namespace SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistration_Interfaces
{
    public interface ITicketRegistration_Service
    {
        Task RegisterTicket(TicketRegistrationRequest ticketRequest);
    }
}