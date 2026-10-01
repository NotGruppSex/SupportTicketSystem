using System;

namespace SupportTicketSystem.Application.Features.Overviews.Dto.Requests;

public record CreateSupporTicketRequest
(
    Guid CustomerId,
    string Title,
    string Description
);
