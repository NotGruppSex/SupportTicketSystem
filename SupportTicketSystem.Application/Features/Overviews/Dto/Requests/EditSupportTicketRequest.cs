using SupportTicketSystem.Domain.Features.Overviews;
using System;

namespace SupportTicketSystem.Application.Features.Overviews.Dto.Requests;

public record EditSupportTicketRequest
(
    Guid CustomerId,
    string Title,
    string Description,
    string Email,
    TicketStatus Status
);
