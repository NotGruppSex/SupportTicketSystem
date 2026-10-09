using SupportTicketSystem.Domain.Features.Overviews.Models;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.Overviews.Services;

public interface ITicketOverviewService
{
    Task<IReadOnlyList<TicketModel>> SearchTicketsAsync(string searchText, TicketStatusEnum.TicketStatus? status);

    Task<TicketStatusCount> GetTicketStatusCountsAsync();
}
