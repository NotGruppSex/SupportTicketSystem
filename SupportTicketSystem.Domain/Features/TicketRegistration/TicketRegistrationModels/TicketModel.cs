
using System;

namespace SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;

public class MockCustomer //TODO - ta bort denna när vi har en customer
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

//Modellen, kräver vissa properties vid skapande genom konstruktorn nedan. Alla nya tickets ska ha status "New" varav det tilldelas inom klassen. Samma för DateTime.  
public class TicketModel 
{
    public Guid TicketID { get; private set; }
    public string TicketTitle { get; private set; }
    public string TicketDescription { get; private set; }
    public MockCustomer CustomerTicket { get; private set; } //TODO - Ändra denna till rätt customer sen
    public TicketPriority Priority { get; private set; }
    public TicketStatus TicketStatus { get; private set; }
    public DateTime TicketCreationDate { get; private set; }


    //Konstruktor som kräver vissa properties från användaren. Kör också validering, initiering och id generering
    public TicketModel(string title, string description, MockCustomer customer, TicketPriority priority)
    {
        //Inputs och validering på det som måste finnas i en ticket. 
        if (string.IsNullOrWhiteSpace(title.Trim()))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        if (string.IsNullOrWhiteSpace(description.Trim()))
            throw new ArgumentException("Description cannot be empty.", nameof(description  ));

        if (customer == null)
            throw new ArgumentNullException(nameof(customer), "You must choose a customer.");

        if (!Enum.IsDefined(priority))
            throw new ArgumentException("You must choose a priority.", nameof(priority));

        //Tilldelning av alla properties.
        TicketID = Guid.NewGuid();
        TicketTitle = title;
        TicketDescription = description;
        CustomerTicket = customer;
        Priority = priority;
        TicketStatus = TicketStatus.New;
        TicketCreationDate = DateTime.Now;
    }

}

