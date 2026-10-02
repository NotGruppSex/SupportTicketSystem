using SupportTicketSystem.Application.Features.Overviews.Dto.Requests;
using SupportTicketSystem.Application.Features.Overviews.Dto.Results;
using SupportTicketSystem.Domain.Features.Overviews.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.Overviews.Services.MockSupportTicketService;

public class SupportTicketService : ISupportTicketRepository 
{
    public async Task<CreateSupportTicketResult> AddSupportTicketAsync(CreateSupportTicketRequest request)
    {
        try
        {
            var supportTicket = new SupportTicket(request.CustomerId, request.Title, request.Description);

            
        }
    }

    public Task<EditSupportTicketResult> EditSupportTicketAsync(EditSupportTicketRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<SupportTicket>> GetAllSupportTicketsAsync()
    {
        throw new NotImplementedException();
    }
}
