using System.Threading.Tasks;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationDtos;

namespace SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationInterfaces
{
    public interface ITicketRegistrationService
    {
        Task RegisterTicket(TicketRegistrationRequest ticketRequest);
    }
}