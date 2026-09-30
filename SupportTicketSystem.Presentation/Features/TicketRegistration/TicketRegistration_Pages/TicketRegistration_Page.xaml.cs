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

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await ViewModel.LoadTicketsAsync();
    }
}
