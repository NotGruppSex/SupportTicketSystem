using System;

namespace SupportTicketSystem.Domain.Features.Overviews.Models;

public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
