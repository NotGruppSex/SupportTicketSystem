
namespace SupportTicketSystem.Application.Features.TicketRegistration.TicketRegistrationServices;
public class TicketRegistration_Service
{
    //Representerar en sak en användare vill göra. (t.ex. place order, cancel order). Den ska INTE innehålla bussiness logic utan den ska KOORDINERA steg som t.ex. 

    //Get data -> Call domain logic -> Save result

    //Känner till repo genom interfaces, jobbar med flöden (Get, do save). Besvarar frågan "Vilka steg händer när en användare gör x?"






    //Private för enbar användning här. ReadOnly för oredigerbar repo efter skapande i konstruktorn. Då blir fält enligt norm(?) som kan innehålla object av repoklassen som använder sig av interfacen. 
    private readonly IJSON_TicketRegistration_Repository _ticketRepository; //skapa interfacen!

    //Konstruktor tar in interfacen av repon, döper och tilldelar fältet det innehållet. 
    public TicketRegistration_Service(IJSON_TicketRegistration_Repository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }
    //Vi gör såhär pga att skapa utbytbarhet genom att hämta repon genom interfacen. 


    public void RegisterTicket()
    {
        //Get info from UI for Title
        

        //Get info from UI for Description
            //Store in temp variable

        //Get info from UI for Customer
            //Store in temp variable

        //Get info from UI the choice of Priority
            //Store in temp variable

        //Assign Guid ID to ticket
        //Assign Date and time to ticket
        //Assign status "New" to ticket

        //Use repo to store the finished ticket in JSON file
    }

}
