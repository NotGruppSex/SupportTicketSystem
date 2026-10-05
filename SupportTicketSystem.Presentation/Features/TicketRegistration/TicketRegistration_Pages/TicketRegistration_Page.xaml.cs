using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportTicketSystem.Application.Features.TicketRegistration.TicketRegistrationServices;
using SupportTicketSystem.Infrastructure.Features.TicketRegistration.TicketRegistrationRepositories;
using SupportTicketSystem.Presentation.Features.TicketRegistration.TicketRegistrationViewModels;

namespace SupportTicketSystem.Presentation.Features.TicketRegistration.TicketRegistrationPages;

public sealed partial class TicketRegistrationPage : Page
{
    //Property av klassen viewmodel för att kunna hämta den
    public TicketRegistration_ViewModel TicketRegistrationViewModel { get; }
    
    //Page tar emot vår viewmodel
    public TicketRegistrationPage(TicketRegistration_ViewModel ticketViewModel)
    {
        //Sätter värdet på propertyn med vår viewmodel.
        TicketRegistrationViewModel = ticketViewModel;

        InitializeComponent();
    }

    //_____________________________________________________________________________________________//

    //TODO - TA BORT DENNA INFÖR MERGE - TILLFÄLLIG FÖR ATT TESTA MIN SIDA
    public TicketRegistrationPage()
        : this(new TicketRegistration_ViewModel(
            new TicketRegistration_Service(new Json_TicketRegistration_Repository())))
    {
    }

    //_____________________________________________________________________________________________//

    //Detta är en event handler med ett laddat event som körs när pagen är färdigladdad och redo för interaktion. Denna är void pga event handlers kräver det (kan ej returnera en Task vi normalt skulle använda)
    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        //För att få alla customers in till pagen. Behöver inte alla priorities då dessa redan finns i ViewModel
        TicketRegistrationViewModel.LoadMockCustomers();
    }
}


//För att testa och inte skapa mergekonflikter: 
    //Gå till NavigationService.cs rad 22:
    //Gå till följande kod på rad 22: AppPage.Home => typeof(HomePage)
    //Ändra till: AppPage.Home => typeof(SupportTicketSystem.Presentation.Features.TicketRegistration.TicketRegistrationPages.TicketRegistrationPage)

    //När väl klar = ändra tillbaka till AppPage.Home => typeof(HomePage) och ta bort using SupportTicketSystem.Presentation.Features.TicketRegistration.TicketRegistrationPages; i MainWindow.xaml.cs