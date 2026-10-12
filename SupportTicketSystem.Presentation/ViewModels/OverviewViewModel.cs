
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportTicketSystem.Application.Features.Overviews.Services;
using SupportTicketSystem.Domain.Features.Overviews.Models;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using SupportTicketSystem.Presentation.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportTicketSystem.Presentation.ViewModels;

public partial class OverviewViewModel : ObservableObject
{
    private readonly ITicketOverviewService _ticketOverviewService;
    private readonly INavigationService _navigationService;

    public string Title { get; } = "Overview Page";

    [ObservableProperty]
    public partial string SearchText { get; set; } = string.Empty;

    [ObservableProperty]
    public partial TicketStatusEnum.TicketStatus? SelectedStatus { get; set; }

    [ObservableProperty]
    public partial IReadOnlyList<TicketModel> Tickets { get; set; }

    [ObservableProperty]
    public partial TicketStatusCount StatusCounts { get; set; }

    [ObservableProperty]
    public partial TicketModel? SelectedTicket { get; set; }

    [ObservableProperty]
    public partial bool IsDetailsOpen { get; set; }

    public OverviewViewModel(ITicketOverviewService ticketOverviewService, INavigationService navigationService)
    {
        _ticketOverviewService = ticketOverviewService;
        _navigationService = navigationService;

        Tickets = [];
        StatusCounts = new(0, 0, 0);
    }

    partial void OnSelectedTicketChanged(TicketModel? value)
    {
        IsDetailsOpen = value is not null;
        EditTicketCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    private void CloseDetails()
    {
        SelectedTicket = null;
    }

    private bool CanEditTicket()
    {
        return SelectedTicket is not null;
    }

    [RelayCommand(CanExecute = nameof(CanEditTicket))]
    private void EditTicket()
    {
        if (SelectedTicket is null)
            return;

        _navigationService.Navigate(
            AppPage.EditTicket,
            SelectedTicket.TicketID.ToString());
    }

    [RelayCommand]
    public async Task Search()
    {
        Tickets = await _ticketOverviewService.SearchTicketsAsync(
            SearchText,
            SelectedStatus);

        StatusCounts = await _ticketOverviewService.GetTicketStatusCountsAsync();
    }
}
