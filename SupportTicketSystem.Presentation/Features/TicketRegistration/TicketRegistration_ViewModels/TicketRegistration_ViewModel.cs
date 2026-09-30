using CommunityToolkit.Mvvm.ComponentModel;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using SupportTicketSystem.DOmain.Features.TicketRegistration.TicketRegistration_Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SupportTicketSystem.Presentation.Features.TicketRegistration.TicketRegistrationViewModels;

//Observable object uppdaterar automatiskt UI när en property ändras. Partial pga MVVM toolkit som genererar extrakod för denna klass under kompilering och är beroende av ObservableObject(???)
public partial class TicketRegistration_ViewModel : ObservableObject
{
    //För att hämta servicen just i denna klass (privat fält) och lägga till din genom konstruktor
    private readonly ITicketRegistration_Service _ticketService;
    public TicketRegistration_ViewModel(ITicketRegistration_Service ticketService)
    {
        _ticketService = ticketService;
    }

    //För meddelande vid olika events senare
    [ObservableProperty] public partial string StatusMessage { get; private set; } = string.Empty;


    //synbara properties för användaren att redigera. OP rensar UI automatiskt. 

    [ObservableProperty] public partial string InputTitle { get; set; } = string.Empty;
    [ObservableProperty] public partial string InputDescription { get; set; } = string.Empty;
    [ObservableProperty] public partial MockCustomer InputCustomer { get; set; }
    [ObservableProperty] public partial TicketPriority InputPriority { get; set; } = TicketPriority.Medium; //Standardvärde

    //Synbara properties som senare kommer visas vid skapad ticket - därför de ej sätts nu (sätts i modellen) och är nullable här
    [ObservableProperty] public partial Guid? GenereatedId { get; private set; }
    [ObservableProperty] public partial TicketStatus? DefaultStatus { get; private set; }
    [ObservableProperty] public partial DateTime? GeneratedDate { get; private set; }


    //TODO - Tillfällig lista med customers så användaren kan välja mellan customers 
    public ObservableCollection<MockCustomer> CustomerOptions { get; } = [];

    //TODO - Tillfälliga customers i listan
    public void LoadMockCustomers()
    {
        CustomerOptions.Add(new MockCustomer { MockCustomerID = Guid.NewGuid(), MockCustomerName = "Mock Mocksson", MockCustomerEmail = "mock@mock.se", MockCustomerPhone = "073123456789" });
        CustomerOptions.Add(new MockCustomer { MockCustomerID = Guid.NewGuid(), MockCustomerName = "Möck Möcksson", MockCustomerEmail = "möck@mock.se", MockCustomerPhone = "073123456780" });
    }
    
    
    //TODO kod när vi har faktiska listan:

    /*
    public async Task LoadCustomersAsync()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        foreach (var c in customers)
            CustomerOptions.Add(c);
    }
    */

    

















    //Relaycommand är handlingar som användaren triggar igång.
}
