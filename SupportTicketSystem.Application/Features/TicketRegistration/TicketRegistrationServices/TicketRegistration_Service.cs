
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistration_Interfaces;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using SupportTicketSystem.DOmain.Features.TicketRegistration.TicketRegistration_Interfaces; //TODO CHECK THIS SHIT OUT
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.TicketRegistration.TicketRegistrationServices;

//Hämtar in repot genom dess interface så den kan användas. 
public class TicketRegistration_Service(IJson_TicketRegistration_Repository ticketRepository) : ITicketRegistration_Service
{
    //Asyncmetod som tar in info från användaren och skapar ticketen. Sen repo för att hämta alla tickets, lägger till nya ticketen och sen sparar alla tickets genom repo. 
    public async Task RegisterTicket(string inputTitle, string inputDescription, MockCustomer inputCustomer, TicketPriority inputPriority)
    {
        TicketModel newTicket = new TicketModel(inputTitle, inputDescription, inputCustomer, inputPriority);

        var allTickets = await ticketRepository.GetAllTicketsAsync();
        allTickets.Add(newTicket);

        await ticketRepository.SaveAllTicketsAsync(allTickets);
    }
}
