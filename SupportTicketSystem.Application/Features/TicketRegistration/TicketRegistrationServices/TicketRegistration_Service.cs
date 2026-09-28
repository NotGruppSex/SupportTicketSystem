using System;
using System.Collections.Generic;
using System.Text;

namespace SupportTicketSystem.Application.Features.TicketRegistration.TicketRegistrationServices
{
    internal class TicketRegistration_Service
    {
        //Representerar en sak en användare vill göra. (t.ex. place order, cancel order). Den ska INTE innehålla bussiness logic utan den ska COORDINERA steg som t.ex. 

        //Get data -> Call domain logic -> Save result

        //Känner till repo genom interfaces, jobbar med flöden (Get, do save). Besvarar frågan "Vilka steg händer när en användare gör x?"
    }
}
