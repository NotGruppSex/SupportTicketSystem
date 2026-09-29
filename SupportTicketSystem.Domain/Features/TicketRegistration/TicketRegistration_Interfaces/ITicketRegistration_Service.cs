using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using System.Threading.Tasks;

namespace SupportTicketSystem.DOmain.Features.TicketRegistration.TicketRegistration_Interfaces
{
    public interface ITicketRegistration_Service
    {
        Task RegisterTicket(string inputTitle, string inputDescription, MockCustomer inputCustomer, TicketPriority inputPriority);
    }
}