using System;

namespace SupportTicketSystem.Application.Features.Overviews.Dto.Requests;

public record CreateSupportTicketRequest
(
    Guid CustomerId,
    string Title,
    string Description
);
