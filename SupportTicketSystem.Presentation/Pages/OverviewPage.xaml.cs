
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using SupportTicketSystem.Presentation.ViewModels;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums;
using Microsoft.Extensions.DependencyInjection;

namespace SupportTicketSystem.Presentation.Pages;

public sealed partial class OverviewPage : Page
{
    public OverviewViewModel ViewModel { get; }

    public OverviewPage()
    {
        ViewModel = App.ServiceProvider.GetRequiredService<OverviewViewModel>();
        InitializeComponent();

        StatusFilter.SelectedIndex = 0;
        Loaded += OverviewPage_Loaded;
    }

    private async void OverviewPage_Loaded(object sender, RoutedEventArgs e)
    {
        await ViewModel.Search();
    }

    private void StatusFilter_SelectionChanged(object sender,SelectionChangedEventArgs e)
    {
        ViewModel.SelectedStatus = StatusFilter.SelectedIndex switch
        {
            1 => TicketStatusEnum.TicketStatus.New,
            2 => TicketStatusEnum.TicketStatus.InProgress,
            3 => TicketStatusEnum.TicketStatus.Closed,
            _ => null
        };
    }
}
