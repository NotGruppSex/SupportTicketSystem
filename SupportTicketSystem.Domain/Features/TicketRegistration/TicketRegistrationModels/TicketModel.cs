using SupportTicketSystem.Domain.Features.Customers;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using SupportTicketSystem.Domain.Features.TicketEditor;
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
    public List<TicketComment> TicketComments { get; private set; }

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
        TicketComments = new List<TicketComment>();
    }

    // Validerar och uppdaterar titel och beskrivning på ett befintligt ärende. (Möjliggör det samtidigt som private set behålls i modellen.)
    public void UpdateTitleAndDescription(string title, string description)
    {
        InputTitleValidation(title);
        InputDescriptionValidation(description);

        TicketTitle = TrimInput(title);
        TicketDescription = TrimInput(description);
    }

    // Uppdaterar prioriteten på ett befintligt ärende.
    public void UpdatePriority(TicketPriority priority)
    {
        Priority = priority;
    }

    // Uppdaterar statusen på ett befintligt ärende.
    public void UpdateStatus(TicketStatus status)
    {
        TicketStatus = status;
    }

    // Tilldelar eller byter handläggare på ett befintligt ärende.
    public void UpdateTechnichianAssigned(string technichianAssigned)
    {
        TechnichianAssigned = technichianAssigned;
    }

    // Lägger till en kommentar på ett befintligt ärende.
    public void AddComment(string comment)
    {
        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException("Comment cannot be empty.");

        TicketComments.Add(new TicketComment(comment.Trim()));
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