
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistration_Interfaces;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using SupportTicketSystem.DOmain.Features.TicketRegistration.TicketRegistration_Interfaces;
using SupportTicketSystem.Infrastructure.Features.TicketRegistration.TicketRegistrationRepositories;
using System;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.TicketRegistration.TicketRegistrationServices;

//Hämtar in repot genom dess interface så den kan användas. 
public class TicketRegistration_Service(IJson_TicketRegistration_Repository ticketRepository) : ITicketRegistration_Service
{
    //Representerar en sak en användare vill göra. (t.ex. place order, cancel order). Den ska INTE innehålla bussiness logic utan den ska KOORDINERA steg som t.ex. 

    //Get data -> Call domain logic -> Save result

    //Känner till repo genom interfaces, jobbar med flöden (Get, do save). Besvarar frågan "Vilka steg händer när en användare gör x?"



    public async Task RegisterTicket(string inputTitle, string inputDescription, MockCustomer inputCustomer, TicketPriority inputPriority)
    {
        //Inputs och validering på det som måste finnas i en ticket. 
        if (string.IsNullOrWhiteSpace(inputTitle))
            throw new ArgumentException("Title cannot be empty.", nameof(inputTitle));

        if (string.IsNullOrWhiteSpace(inputDescription))
            throw new ArgumentException("Description cannot be empty.", nameof(inputDescription));

        if (inputCustomer == null) //TODO - Ev. lägga till en repo för att hämta alla kunder så användaren kan välja från enlista - KOLLA UPP HUR
            throw new ArgumentNullException(nameof(inputCustomer), "You must choose a customer.");

        if (!Enum.IsDefined(inputPriority))
            throw new ArgumentException("You must choose a priority.", nameof(inputPriority));

        //Skapande av ticketobjectet. (inklusive ny guid) Status och datum sätts inuti modellen.
        TicketModel newTicket = new TicketModel(Guid.NewGuid(), inputTitle.Trim(), inputDescription.Trim(), inputCustomer, inputPriority);

        //Repo för att hämta alla tickets i Json-filen och sen lägger till den nya ticketen i den listan.
        var allTickets = await ticketRepository.GetAllTicketsAsync();
        allTickets.Add(newTicket);

        //Repo för att spara alla tickets i Json-filen. 
        await ticketRepository.SaveAllTicketsAsync(allTickets);
    }
}
