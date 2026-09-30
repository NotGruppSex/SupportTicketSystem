using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportTicketSystem.Presentation.Features.TicketRegistration.TicketRegistrationViewModels;

namespace SupportTicketSystem.Presentation.Features.TicketRegistration.TicketRegistrationPages;

public sealed partial class TicketRegistrationPage : Page
{
    //Property av klassen viewmodel för att kunna hämta den
    public TicketRegistration_ViewModel TicketRegistrationViewModel { get; }
    
    //Page tar emot vår viewmodel
    public TicketRegistrationPage(TicketRegistration_ViewModel ticketViewModel)
    {
        //Sätter värdet på propertyn med vår viewmodel. FÖR ATT???
        TicketRegistrationViewModel = ticketViewModel;

        InitializeComponent();
    }

    //Detta är en event handler med ett laddat event som körs när pagen är färdigladdad och redo för interaktion. Denna är void pga event handlers kräver det (kan ej returnera en Task vi normalt skulle använda)
    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        //För att få alla customers in till pagen. Behöver inte alla priorities då dessa redan finns i ViewModel
        TicketRegistrationViewModel.LoadMockCustomers();
    }
}
