using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using SupportTicketSystem.Presentation.ViewModels;


namespace SupportTicketSystem.Presentation.Pages;

public sealed partial class HomePage : Page
{
    public HomeViewModel ViewModel { get; }
    public HomePage()
    {
        ViewModel = App.ServiceProvider.GetRequiredService<HomeViewModel>();

        InitializeComponent();
    }
}
