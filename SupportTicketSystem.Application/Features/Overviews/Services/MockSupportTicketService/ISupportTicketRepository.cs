using SupportTicketSystem.Application.Features.Overviews.Dto.Requests;
using SupportTicketSystem.Application.Features.Overviews.Dto.Results;
using SupportTicketSystem.Domain.Features.Overviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.Overviews.Services.MockSupportTicketService;

public interface ISupportTicketRepository
{
    Task<IReadOnlyList<SupportTicket>> GetAllSupportTicketsAsync();
    public Task<EditSupportTicketResult> EditSupportTicketAsync(EditSupportTicketRequest request);
    Task<CreateSupportTicketResult> AddSupportTicketAsync(CreateSupportTicketRequest request);

}
