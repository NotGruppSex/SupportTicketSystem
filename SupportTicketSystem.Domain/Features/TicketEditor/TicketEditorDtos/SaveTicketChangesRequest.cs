#nullable enable
using System;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketPriorityEnum;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketStatusEnum;

namespace SupportTicketSystem.Domain.Features.TicketEditor.TicketEditorDtos
{
    public record SaveTicketChangesRequest
    (
        Guid TicketID,
        string TicketTitle,
        string TicketDescription,
        TicketPriority Priority,
        TicketStatus Status,
        string TechnichianAssigned,
        string? CommentText
    );
}
