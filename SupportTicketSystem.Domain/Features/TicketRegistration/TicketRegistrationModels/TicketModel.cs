
using System;

namespace SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;

public class MockCustomer
{
    public Guid MockCustomerID { get; set; }
    public string MockCustomerName { get; set; }
    public string MockCustomerEmail { get; set; }
    public string MockCustomerPhone { get; set; }
}

//Skapar enum för ticketmodellen så det blir enklare att enbart kunna ändra genom statusarna enbart olika status på status och prioritering. 
public enum TicketStatus
{
    New,
    InProgress,
    Closed,
}

public enum TicketPriority
{
    Low,
    Medium,
    High,
}

//Modellen, kräver vissa properties vid skapande. Alla nya tickets ska ha status "New" varav det tilldelas inom klassen. Samma för DateTime.  
public class TicketModel (Guid id, string title, string description, MockCustomer customer, TicketPriority priority) 
{
    public Guid TicketID { get; set; } = id;
    public string TicketTitle { get; set; } = title;
    public string TicketDescription { get; set; } = description;
    public MockCustomer customerTicket { get; set; } = customer; //TODO - Ändra denna till rätt customer sen
    public TicketPriority Priority { get; set; } = priority;
    public TicketStatus TicketStatus { get; set; } = TicketStatus.New;
    public DateTime TicketCreationDate { get; set; } = DateTime.Now; //private för att denna ska vara statisk. 
}

