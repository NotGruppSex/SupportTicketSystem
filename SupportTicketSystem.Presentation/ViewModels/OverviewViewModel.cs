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
    private readonly INavigationService _navigationService;
    private readonly ITicketOverviewService _ticketOverviewService;

    public string Title { get;} = "Overview Page";
    
    [ObservableProperty]
    public partial string SearchText { get; set; } = string.Empty;

    [ObservableProperty]
    public partial TicketStatusEnum.TicketStatus? SelectedStatus { get; set; }

    [ObservableProperty]
    public partial IReadOnlyList<TicketModel> Tickets { get; set; }

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
    public async Task Search()
    {
        Tickets = await _ticketOverviewService.SearchTicketsAsync(SearchText, SelectedStatus);
        StatusCounts = await _ticketOverviewService.GetTicketStatusCountsAsync();
    }

}