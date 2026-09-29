using System;

namespace SupportTicketSystem.Domain.Features.Overviews;

public class SupportTicket
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public TicketStatus? Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
