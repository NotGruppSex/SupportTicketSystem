using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using System.Threading.Tasks;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketPriorityEnum;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketStatusEnum;

namespace SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistration_Interfaces
{
    public interface ITicketRegistration_Service
    {
        Task RegisterTicket(string inputTitle, string inputDescription, MockCustomer inputCustomer, TicketPriority inputPriority);
    }
}