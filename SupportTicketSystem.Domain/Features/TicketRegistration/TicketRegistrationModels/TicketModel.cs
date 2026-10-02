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


//TODO - gör om till record?

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

    //Parameterlös konstruktor för serialisering av Json - annars funkar den inte och vi får krash.
    public TicketModel() { } 

    //Konstruktor som kräver vissa properties från användaren. Kör också validering, initiering och id generering
    public TicketModel(string title, string description, MockCustomer customer, TicketPriority priority)
    {
        //Validering och trimning.
        InputStringValidation(title);
        InputStringValidation(description);
        InputObjectValidation(customer);
        InputEnumValidation(priority);

        TrimInput(title);
        TrimInput(description);
        
        //Tilldelning av alla properties.
        TicketID = Guid.NewGuid();
        TicketTitle = title;
        TicketDescription = description;
        CustomerTicket = customer;
        Priority = priority;
        TicketStatus = TicketStatus.New;
        TicketCreationDate = DateTime.Now;
    }

    public void InputStringValidation(string valueToValidate)
    {
        if (string.IsNullOrWhiteSpace(valueToValidate))
        {
            if (valueToValidate == TicketTitle)
                throw new ArgumentException("Title cannot be empty.", nameof(valueToValidate));
            if (valueToValidate == TicketDescription)
                throw new ArgumentException("Description cannot be empty.", nameof(valueToValidate));
        }
    }

    public void InputObjectValidation(object objectToValidate)
    {
        if (objectToValidate == null)
        {
            if (objectToValidate == CustomerTicket)
                throw new ArgumentNullException(nameof(objectToValidate), "You must choose a customer.");
        }
    }

    public void InputEnumValidation(TicketPriority enumToValidate)
    {
        if (!Enum.IsDefined(enumToValidate))
            throw new ArgumentException("You must choose a priority.", nameof(enumToValidate));
    }

    public void TrimInput(string input)
    {
        if (input == TicketTitle)
        {
            TicketTitle = TicketTitle.Trim();
        }
        if (input == TicketDescription)
        {
            TicketDescription = TicketDescription.Trim();
        }
    }
}