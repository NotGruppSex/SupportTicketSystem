using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportTicketSystem.Application.Features.Overviews;
using SupportTicketSystem.Domain.Features.Overviews;
using SupportTicketSystem.Domain.Features.Overviews.Models;
using SupportTicketSystem.Presentation.Navigation;
using System.Collections.Generic;

namespace SupportTicketSystem.Presentation.Features.Overviews.ViewModel;

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

        Search();
    }
    [RelayCommand]
    private void Search()
    {
        Tickets = _ticketOverviewService.GetAllTickets(SearchText, SelectedStatus);
        StatusCounts = _ticketOverviewService.GetTicketStatusCounts();
    }

}