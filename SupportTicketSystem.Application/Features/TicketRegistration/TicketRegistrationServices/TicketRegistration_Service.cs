
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using SupportTicketSystem.Infrastructure.Features.TicketRegistration.TicketRegistrationRepositories;
using System;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.TicketRegistration.TicketRegistrationServices;
public class TicketRegistration_Service
{
    //Representerar en sak en användare vill göra. (t.ex. place order, cancel order). Den ska INTE innehålla bussiness logic utan den ska KOORDINERA steg som t.ex. 

    //Get data -> Call domain logic -> Save result

    //Känner till repo genom interfaces, jobbar med flöden (Get, do save). Besvarar frågan "Vilka steg händer när en användare gör x?"






    //Private för enbar användning här. ReadOnly för oredigerbar repo efter skapande i konstruktorn. Då blir fält enligt norm(?) som kan innehålla object av repoklassen som använder sig av interfacen. 
    private readonly IJson_TicketRegistration_Repository _ticketRepository;

    //Konstruktor tar in interfacen av repon, döper och tilldelar fältet det innehållet. 
    public TicketRegistration_Service(IJson_TicketRegistration_Repository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }
    //Vi gör såhär pga att skapa utbytbarhet genom att hämta repon genom interfacen. 


    public async Task RegisterTicket(string inputTitle, string inputDescription, MockCustomer inputCustomer, TicketPriority inputPriority)
    {
        //Inputs och validering
        if (string.IsNullOrWhiteSpace(inputTitle))
            throw new ArgumentException("Title cannot be empty.", nameof(inputTitle));

        if (string.IsNullOrWhiteSpace(inputDescription))
            throw new ArgumentException("Description cannot be empty.", nameof(inputDescription));

        if (inputCustomer == null)
            throw new ArgumentNullException(nameof(inputCustomer), "You must choose a customer.");

        if (!Enum.IsDefined(inputPriority))
            throw new ArgumentException("You must choose a priority.", nameof(inputPriority));

        //Skapande av ticketobjectet. (inklusive ny guid)
        TicketModel newTicket = new TicketModel(Guid.NewGuid(), inputTitle, inputDescription, inputCustomer, inputPriority);

        //Status och datum sätts inuti modellen.

        //Use repo to store the finished ticket in JSON file
        IJson_TicketRegistration_Repository ticketRepository = _ticketRepository;

    }

}
