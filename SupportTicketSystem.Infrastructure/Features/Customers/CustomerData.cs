using System;
using Windows.Foundation.Collections;

namespace SupportTicketSystem.Infrastructure.Features.Customers;

internal class CustomerData
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
