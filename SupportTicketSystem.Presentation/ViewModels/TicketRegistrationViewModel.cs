using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportTicketSystem.Domain.Features.Customers;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationDtos;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketPriorityEnum;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketStatusEnum;

namespace SupportTicketSystem.Presentation.Features.TicketRegistration.TicketRegistrationViewModels;



//ObsObj uppdaterar automatiskt UI vid propertyändringar. Partial pga MVVM toolkit som genererar extrakod för denna klass under kompilering och är beroende av ObservableObject(???)
public partial class TicketRegistrationViewModel : ObservableObject
{
    //För att hämta servicen just i denna klass (privat fält) och läggs till genom konstruktor (för att få tillgång till min service)
    private readonly ITicketRegistrationService _ticketService;
    public TicketRegistrationViewModel(ITicketRegistrationService ticketService)
    {
        _ticketService = ticketService;
    }

    //-----------För meddelande vid olika events senare-----------//
    [ObservableProperty] 
    public partial string StatusMessage { get; private set; } = string.Empty;



    //-----------Synbara properties för användaren att redigera-----------//

    [ObservableProperty] 
    public partial string InputTitle { get; set; } = string.Empty;

    [ObservableProperty] 
    public partial string InputDescription { get; set; } = string.Empty;

    [ObservableProperty] 
    public partial Customer? InputCustomer { get; set; } //nullable vid pageload så är ingen customer vald, alltså null. Därför måste den vara nullable.

    [ObservableProperty] 
    public partial TicketPriority InputPriority { get; set; } = TicketPriority.Medium;



    //Lista så användaren kan se alla priority:
    public List<TicketPriority> PriorityOptions { get; } = [TicketPriority.Low, TicketPriority.Medium, TicketPriority.High];

    public ObservableCollection<Customer> CustomerOptions { get; } = []; //Lista av customers som blir synbara


    //Relaycommand är handlingar som användaren triggar igång. Denna async körs när användaren trycker på "Create ticket". Den tar infon från användaren och skickar vidare till servicen för hantering. Om ej funkar skrivs felmeddelande ut. Om det lyckades rensas alla inputs. 
    [RelayCommand]
    private async Task SendInfoToServiceAsync()
    {
        try
        {
            //skapar recordobject som skickas till servicen.
            await _ticketService.RegisterTicket(new TicketRegistrationRequest(InputTitle, InputDescription, InputCustomer, InputPriority));

            StatusMessage = "Ticket created!";

            InputTitle = string.Empty;
            InputDescription = string.Empty;
            InputCustomer = null;
            InputPriority = TicketPriority.Medium;
        }
        //Om det inte funkar så skrivs ett felmeddelande ut beroende på vart felet är. 
        catch (ArgumentException exception)
        {
            StatusMessage = exception.Message;
        }
    }
}