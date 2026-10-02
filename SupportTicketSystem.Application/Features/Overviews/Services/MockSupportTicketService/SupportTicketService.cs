using SupportTicketSystem.Application.Features.Overviews.Dto.Requests;
using SupportTicketSystem.Application.Features.Overviews.Dto.Results;
using SupportTicketSystem.Domain.Features.Overviews.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.Overviews.Services.MockSupportTicketService;

public class SupportTicketService (ISupportTicketRepository supportTicketRepository)
{
    public async Task<CreateSupportTicketResult> CreateSupportTicketAsync(CreateSupportTicketRequest request)
    {
        try
        {
            var supportTicket = new SupportTicket(request.CustomerId, request.Title, request.Description);

            await supportTicketRepository.AddSupportTicketAsync(supportTicket);

            return new CreateSupportTicketResult(true, null, supportTicket);
        }
        catch(Exception ex)
        {
            return new CreateSupportTicketResult(false, ex.Message, null);
        }
    }
    public async Task<EditSupportTicketResult> EditSupportTicketAsync(EditSupportTicketRequest request)
    {
        try
        {
            var allSupportTickets = await supportTicketRepository.GetAllSupportTicketsAsync();

            var currentSupportTicket = allSupportTickets.FirstOrDefault(currentSupportTicket => currentSupportTicket.Id == request.TicketId);

            if(currentSupportTicket is null)
            {
                return new EditSupportTicketResult(false, "Support ticket could not be found", null);
            }

            currentSupportTicket.ChangeTitle(request.Title);
            currentSupportTicket.ChangeDescription(request.Description);
            currentSupportTicket.ChangeTicketStatus(request.TicketStatus);

            await supportTicketRepository.EditSupportTicketAsync(currentSupportTicket);

            return new EditSupportTicketResult(true, null, currentSupportTicket);
        }
        catch (Exception ex)
        {
            return new EditSupportTicketResult(false , ex.Message, null);
        }
    }

}
