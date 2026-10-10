using SupportTicketSystem.Domain.Features.Customers;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketPriorityEnum;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketStatusEnum;
namespace SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;

//Modellen med krav av vissa properties konstruktorn. Nya tickets får defaultvärde status new och dagens datum. Behövde lägga till JsonInclude för att kunna deserialize, annars funkade det inte.   
public class TicketModel 
{
    [JsonInclude]
    public Guid TicketID { get; private set; }
    [JsonInclude]
    public string TicketTitle { get; private set; }
    [JsonInclude]
    public string TicketDescription { get; private set; }
    [JsonInclude]
    public Customer CustomerTicket { get; private set; }
    [JsonInclude]
    public TicketPriority Priority { get; private set; }
    [JsonInclude]
    public TicketStatus TicketStatus { get; private set; }
    [JsonInclude]
    public DateTime TicketCreationDate { get; private set; }
    [JsonInclude]
    public string TechnichianAssigned { get; private set; }
    [JsonInclude]
    public List<string> TicketComments { get; private set; }

    //Parameterlös konstruktor för deserialisering av Json - annars krash
    public TicketModel() { } 

    //Konstruktor med propertieskrav, validering och trim.
    public TicketModel(string title, string description, Customer customer, TicketPriority priority)
    {
        //Validering och trimning (ej för priority då den alltid har ett värde)
        InputTitleValidation(title);
        InputDescriptionValidation(description);
        InputCustomerValidation(customer);


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
        TechnichianAssigned = string.Empty;
        TicketComments = new List<string>();
    }


    //___________________Validerings- och trimmetoder___________________//

    public void InputTitleValidation(string titleToValidate)
    {
        if (string.IsNullOrWhiteSpace(titleToValidate))
            throw new ArgumentException("Title cannot be empty.");
    }
    public void InputDescriptionValidation(string descriptionToValidate)
    {
        if (string.IsNullOrWhiteSpace(descriptionToValidate))
            throw new ArgumentException("Description cannot be empty.");
    }

    public void InputCustomerValidation(Customer customerToValidate)
    {
        if (customerToValidate == null)
            throw new ArgumentException("You must choose a customer.");
    }

    //No validation for priority as it always will be at least medium and never null or empty.

    public string TrimInput(string input)
    {
        input = input.Trim();
        return input;
    }
}