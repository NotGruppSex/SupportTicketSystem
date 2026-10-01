using Microsoft.UI.Xaml;
namespace SupportTicketSystem.Presentation.Features.TicketRegistration.FakeMainPage;
public sealed partial class FakeMainWindow : Window
{
    public FakeMainWindow()
    {
        InitializeComponent();
    }

    private void GoTicketRegistrationButton_Click(object sender, RoutedEventArgs e)
    {
        DevContentFrame.Navigate(
            typeof(Features.TicketRegistration.TicketRegistrationPages.TicketRegistrationPage));
    }
}
