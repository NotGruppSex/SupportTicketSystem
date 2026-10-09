using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportTicketSystem.Presentation.ViewModels;

namespace SupportTicketSystem.Presentation.Pages;

public sealed partial class CustomerPage : Page
{
    public CustomersViewModel ViewModel { get; }

    public CustomerPage()
    {
        ViewModel = App.ServiceProvider.GetRequiredService<CustomersViewModel>();

        InitializeComponent();

        Loaded += CustomerPage_Loaded;
    }

    private async void CustomerPage_Loaded(object sender, RoutedEventArgs e)
    {
        if (ViewModel.LoadCustomersCommand.CanExecute(null))
        {
            await ViewModel.LoadCustomersCommand.ExecuteAsync(null);
        }
    }
}