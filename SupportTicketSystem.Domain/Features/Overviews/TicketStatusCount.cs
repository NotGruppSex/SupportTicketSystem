namespace SupportTicketSystem.Domain.Features.Overviews;

public record TicketStatusCount
(
    int New,
    int InProgress,
    int Resolved
);
