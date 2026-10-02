using SupportTicketSystem.Domain.Features.Overviews;
using SupportTicketSystem.Domain.Features.Overviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.Overviews.Services.MockSupportTicketService;

public interface ISupportTicketRepository
{
    Task<IReadOnlyList<SupportTicket>> GetAllSupportTicketsAsync();
    Task EditSupportTicketAsync(SupportTicket supportTicket);
    Task AddSupportTicketAsync(SupportTicket supportTicket);

}
