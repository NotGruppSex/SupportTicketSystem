using SupportTicketSystem.Domain.Features.Overviews;
using SupportTicketSystem.Domain.Features.Overviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.Overviews.Services;

public interface ITicketOverviewService
{
    Task<IReadOnlyList<SupportTicket>> SearchTicketsAsync(string searchText, TicketStatus? status);

    Task<TicketStatusCount> GetTicketStatusCountsAsync();
}
