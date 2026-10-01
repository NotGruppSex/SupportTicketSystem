
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportTicketSystem.Presentation.ViewModels;


namespace SupportTicketSystem.Presentation.Pages;


public sealed partial class OverviewPage : Page
{
    public OverviewViewModel Overview { get; }

    public OverviewPage()
    {
        Overview = App.ServiceProvider.GetRequiredService<OverviewViewModel>();

        InitializeComponent();

        Loaded += OverviewPage_Loaded;
    }
    private async void OverviewPage_Loaded(object sender, RoutedEventArgs e)
    {
        await Overview.LoadAsync();
    }
}
