
using System;

namespace SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;

public class MockCustomer
{
    public Guid MockCustomerID { get; set; }
    public string MockCustomerName { get; set; }
    public string MockCustomerEmail { get; set; }
    public string MockCustomerPhone { get; set; }
}

public class TicketModel
{
    public Guid TicketID { get; set; }
    public string TicketTitle { get; set; }
    public string TicketDescription { get; set; }
    public MockCustomer customerTicket { get; set; } //Ändra denna till rätt customer sen
    public string Priority { get; set; }
}
