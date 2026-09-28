
using System;

namespace SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
public class TicketModel
{
    public Guid TicketID { get; set; }
    public string TicketTitle { get; set; }
    public string TicketDescription { get; set; }

    //Lägg till denna 
    public Customer customerTicket { get; set; }
    public string Priority { get; set; }
}
