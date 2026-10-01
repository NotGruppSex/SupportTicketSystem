using SupportTicketSystem.Domain.Features.Overviews.Models;

namespace SupportTicketSystem.Application.Features.Overviews.Dto.Results;

public record CreateTicketResult
(
    bool Success,
    string? Message,
    SupportTicket? Ticket
);
