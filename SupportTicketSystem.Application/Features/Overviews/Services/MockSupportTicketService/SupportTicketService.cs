using SupportTicketSystem.Domain.Features.Overviews.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.Overviews.Services.MockSupportTicketService;

public class SupportTicketService : ISupportTicketRepository
{
    public Task AddSupportTicketAsync(SupportTicket supportTicket)
    {
        throw new NotImplementedException();
    }

    public Task EditSupportTicketAsync(SupportTicket supportTicket)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<SupportTicket>> GetAllSupportTicketsAsync()
    {
        throw new NotImplementedException();
    }
}
