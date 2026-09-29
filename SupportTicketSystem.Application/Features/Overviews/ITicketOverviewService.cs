using SupportTicketSystem.Domain.Features.Overviews;
using SupportTicketSystem.Domain.Features.Overviews.Models;
using System.Collections.Generic;

namespace SupportTicketSystem.Application.Features.Overviews;

public interface ITicketOverviewService
{
    IReadOnlyList<SupportTicket> GetAllTickets(string searchText, TicketStatus? status);
    TicketStatusCount GetTicketStatusCounts();
}
public interface ITicketRepository
{
    IReadOnlyList<SupportTicket> GetAllTickets();
}
public interface ICustomerRepository
{
    IReadOnlyList<Customer> GetAllCustomers();
}
