using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportTicketSystem.Presentation.Navigation;

namespace SupportTicketSystem.Presentation;

public sealed partial class MainWindow : Window
{
    private readonly INavigationService _navigationService;

    public MainWindow(INavigationService navigationService)
    {
        InitializeComponent();

        _navigationService = navigationService;
        _navigationService.Initialize(ContentFrame); //Skapa Initialize
        _navigationService.Navigate(AppPage.Home); //Skapa Navigate och Home

    }

    private void MainNavigation_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
    {
        if (args.InvokedItemContainer is not NavigationViewItem item) //skapa Nav.ViewItem
            return;

        AppPage? page = item.Tag?.ToString() switch
        {
            "home" => AppPage.Home,
            _ => null,
        };
        if (page is AppPage destination)
            _navigationService.Navigate(destination); //Skapa Navigate
    }

    private void MainNavigation_BackRequested(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs args)
    {
        _navigationService.GoBack();
    }

    private void ContentFrame_Navigated(object sender, Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
    {
        MainNavigation.IsBackEnabled = true;
    }
}
