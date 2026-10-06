using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistration_dtos;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistration_Interfaces;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.TicketRegistration.TicketRegistrationServices;

//Hämtar in repot genom dess interface så den kan användas. 
public class TicketRegistration_Service(IJson_TicketRegistration_Repository ticketRepository) : ITicketRegistration_Service
{
    //Asyncmetod tar in info från användaren och skapar ticketen genom record. Sen repo för att hämta alla tickets, lägger till nya ticketen och sen sparar alla tickets genom repo. 
    public async Task RegisterTicket(TicketRegistrationRequest ticketRequest)
    {
        TicketModel newTicket = new TicketModel(
            ticketRequest.TicketTitle, 
            ticketRequest.TicketDescription, 
            ticketRequest.TicketCustomer, 
            ticketRequest.TicketPriority);

        var allTickets = await ticketRepository.GetAllTicketsAsync();
        allTickets.Add(newTicket);

        await ticketRepository.SaveAllTicketsAsync(allTickets);
    }
}
