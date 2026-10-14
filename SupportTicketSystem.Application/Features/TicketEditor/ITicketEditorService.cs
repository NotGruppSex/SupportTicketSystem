using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using System;
using System.Threading.Tasks;
using SupportTicketSystem.Domain.Features.TicketEditor.TicketEditorDtos;

namespace SupportTicketSystem.Application.Features.TicketEditor;

public interface ITicketEditorService
{
    Task<TicketModel> GetTicketByIdAsync(Guid ticketId);

    Task SaveTicketChangesAsync(SaveTicketChangesRequest request);
}