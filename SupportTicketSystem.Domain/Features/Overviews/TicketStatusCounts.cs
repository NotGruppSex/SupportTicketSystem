namespace SupportTicketSystem.Domain.Features.Overviews;

public record TicketStatusCounts
(
    int New,
    int InProgress,
    int Resolved
);
