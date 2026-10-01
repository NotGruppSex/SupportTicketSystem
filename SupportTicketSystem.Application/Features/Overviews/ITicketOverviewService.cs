using SupportTicketSystem.Domain.Features.Overviews;
using SupportTicketSystem.Domain.Features.Overviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.Overviews;

public interface ITicketOverviewService
{
    Task<IReadOnlyList<SupportTicket>> GetAllTicketsAsync(string searchText, TicketStatus? status);
    Task <TicketStatusCount> GetTicketStatusCountsAsync();
}
public interface ITicketRepository
{
    Task<IReadOnlyList<SupportTicket>> GetAllTicketsAsync();
}
public interface ICustomerRepository
{
    Task<IReadOnlyList<Customer>> GetAllCustomersAsync();
}
