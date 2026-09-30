using CommunityToolkit.Mvvm.ComponentModel;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using System.Collections.ObjectModel;

namespace SupportTicketSystem.Presentation.Features.TicketRegistration.TicketRegistrationViewModels;

//Varför ärver den från observable object?
public class TicketRegistration_ViewModel : ObservableObject
{
    //Klass för att skapa lista av kunder. (kan bara ändras vid skapning av ny ticket)
    public ObservableCollection<MockCustomer> mockCustomers { get; } = [];

    //Lägger till en observable property:
    [ObservableProperty]
    public partial string StatusMessage { get; private set; } = string.Empty;
}
