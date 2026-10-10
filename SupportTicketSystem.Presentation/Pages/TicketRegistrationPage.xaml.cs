using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportTicketSystem.Presentation.Features.TicketRegistration.TicketRegistrationViewModels;

namespace SupportTicketSystem.Presentation.Features.TicketRegistration.TicketRegistrationPages;

public sealed partial class TicketRegistrationPage : Page
{
    //Property av klassen viewmodel för att kunna hämta den
    public TicketRegistrationViewModel TicketRegistrationViewModel { get; }
    
    //Parameterlös konstruktor pga Activator.CreativeInstance behöver den
    public TicketRegistrationPage()
    {
        //Hämtar viewmodel genom DI
        TicketRegistrationViewModel = App.ServiceProvider.GetRequiredService<TicketRegistrationViewModel>();

        InitializeComponent();
    }

    //Detta är en event handler med ett laddat event som körs när pagen är färdigladdad och redo för interaktion. Denna är void pga event handlers kräver det (kan ej returnera en Task vi normalt skulle använda)
    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        
    }
}
