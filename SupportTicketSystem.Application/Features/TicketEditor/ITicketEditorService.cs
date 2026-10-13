using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using System;
using System.Threading.Tasks;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketPriorityEnum;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketStatusEnum;
using SupportTicketSystem.Domain.Features.TicketEditor.TicketEditorDtos;

namespace SupportTicketSystem.Application.Features.TicketEditor;

public interface ITicketEditorService
{
    Task<TicketModel> GetTicketByIdAsync(Guid ticketId);

    Task UpdateTitleAndDescriptionAsync(
        Guid ticketId,
        string title,
        string description);

    Task UpdatePriorityAsync(
        Guid ticketId,
        TicketPriority priority);

    Task UpdateStatusAsync(
        Guid ticketId,
        TicketStatus status);

    Task UpdateTechnichianAssignedAsync(
        Guid ticketId,
        string technichianAssigned);

    Task AddCommentAsync(
        Guid ticketId,
        string comment);

    Task SaveTicketChangesAsync(SaveTicketChangesRequest request);
}