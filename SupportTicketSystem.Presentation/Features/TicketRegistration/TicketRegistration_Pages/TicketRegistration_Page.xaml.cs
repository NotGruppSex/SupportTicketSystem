using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportTicketSystem.Presentation.Features.TicketRegistration.TicketRegistrationViewModels;

namespace SupportTicketSystem.Presentation.Features.TicketRegistration.TicketRegistrationPages;

public sealed partial class TicketRegistrationPage : Page
{
    public TicketRegistrationPage(TicketRegistration_ViewModel ticketViewModel)
    {
        InitializeComponent();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {

    }
}
