using System;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketPriorityEnum;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketStatusEnum;
namespace SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;

public class MockCustomer //TODO - ta bort denna när vi har en customer
{
    public Guid MockCustomerID { get; set; }
    public string MockCustomerName { get; set; }
    public string MockCustomerEmail { get; set; }
    public string MockCustomerPhone { get; set; }
}

//Modellen med krav av vissa properties konstruktorn. Nya tickets får defaultvärde status new och dagens datum.  
public class TicketModel 
{
    public Guid TicketID { get; private set; }
    public string TicketTitle { get; private set; }
    public string TicketDescription { get; private set; }
    public MockCustomer CustomerTicket { get; private set; } //TODO - Ändra denna till rätt customer sen
    public TicketPriority Priority { get; private set; }
    public TicketStatus TicketStatus { get; private set; }
    public DateTime TicketCreationDate { get; private set; }

    //Parameterlös konstruktor för serialisering av Json - annars funkar den inte och vi får krash av någon anledning.
    public TicketModel() { } 

    //Konstruktor med propertieskrav, validering och trim.
    public TicketModel(string title, string description, MockCustomer customer, TicketPriority priority)
    {
        //Validering och trimning.
        InputTitleValidation(title);
        InputDescriptionValidation(description);
        InputCustomerValidation(customer);
        InputEnumValidation(priority);

        title = TrimInput(title);
        description = TrimInput(description);

        //Tilldelning av alla properties. Defaultvärden status + datum
        TicketID = Guid.NewGuid();
        TicketTitle = title;
        TicketDescription = description;
        CustomerTicket = customer;
        Priority = priority;
        TicketStatus = TicketStatus.New;
        TicketCreationDate = DateTime.Now;
    }


    //___________________Validerings- och trimmetoder___________________//

    public void InputTitleValidation(string titleToValidate)
    {
        if (string.IsNullOrWhiteSpace(titleToValidate))
            throw new ArgumentException("Title cannot be empty.", nameof(titleToValidate));
    }
    public void InputDescriptionValidation(string descriptionToValidate)
    {
        if (string.IsNullOrWhiteSpace(descriptionToValidate))
            throw new ArgumentException("Description cannot be empty.", nameof(descriptionToValidate));
    }

    public void InputCustomerValidation(MockCustomer customerToValidate)
    {
        if (customerToValidate == null)
        {
            throw new ArgumentNullException(nameof(customerToValidate   ), "You must choose a customer.");
        }
    }

    public void InputEnumValidation(TicketPriority enumToValidate)
    {
        if (!Enum.IsDefined(enumToValidate))
            throw new ArgumentException("You must choose a priority.", nameof(enumToValidate));
    }

    public string TrimInput(string input)
    {
        input = input.Trim();
        return input;
    }
}