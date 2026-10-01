using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportTicketSystem.Application.Features.Overviews.Services;
using SupportTicketSystem.Domain.Features.Overviews;
using SupportTicketSystem.Domain.Features.Overviews.Models;
using SupportTicketSystem.Presentation.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportTicketSystem.Presentation.ViewModels;

public partial class OverviewViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly ITicketOverviewService _ticketOverviewService;

    public string Title { get;} = "Overview Page";

    [ObservableProperty]
    public partial string SearchText { get; set; } = string.Empty;

    [ObservableProperty]
    public partial TicketStatus? SelectedStatus { get; set; }

    [ObservableProperty]
    public partial IReadOnlyList<SupportTicket> Tickets { get; set; }

    [ObservableProperty]
    public partial TicketStatusCount StatusCounts { get; set; } 

    public OverviewViewModel(INavigationService navigationService, ITicketOverviewService ticketOverviewService)
    {
        _navigationService = navigationService;
        _ticketOverviewService = ticketOverviewService;

        Tickets = [];
        StatusCounts = new (0, 0, 0);

    }
    [RelayCommand]
    private async Task Search()
    {
        Tickets = await _ticketOverviewService.GetAllTicketsAsync(SearchText, SelectedStatus);
        StatusCounts = await _ticketOverviewService.GetTicketStatusCountsAsync();
    }
    public async Task LoadAsync()
    {
        await Search();
    }

}