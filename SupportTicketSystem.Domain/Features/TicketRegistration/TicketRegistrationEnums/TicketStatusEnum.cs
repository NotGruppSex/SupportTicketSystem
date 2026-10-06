
namespace SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums;
public class TicketStatusEnum
{
    public enum TicketStatus
    {
        New,
        InProgress,
        Closed,
    }
}

//Skapar enum för ticketmodellen så det blir enklare att enbart kunna ändra genom statusarna enbart olika status på status och prioritering. 
