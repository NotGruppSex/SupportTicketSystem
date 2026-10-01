using SupportTicketSystem.Domain.Features.Overviews.Models;

namespace SupportTicketSystem.Application.Features.Overviews.Dto.Results;

public record CreateCustomerResult
(
    bool Success,
    string? Message,
    Customer? Customer
);
