namespace SupportTicketSystem.Domain.Features.Overviews.Models;

public record TicketStatusCount
(
    int New,
    int InProgress,
    int Resolved
);
