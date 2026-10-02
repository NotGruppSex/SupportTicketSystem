using SupportTicketSystem.Domain.Features.Overviews;
using System;

namespace SupportTicketSystem.Application.Features.Overviews.Dto.Requests;

public record EditSupportTicketRequest
(
    Guid TicketId,
    string Title,
    string Description,
    TicketStatus TicketStatus
);
