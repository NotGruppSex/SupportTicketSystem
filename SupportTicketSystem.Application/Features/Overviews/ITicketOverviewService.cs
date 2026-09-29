using SupportTicketSystem.Domain.Features.Overviews;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupportTicketSystem.Application.Features.Overviews;

public interface ITicketOverviewService
{
    IReadOnlyList<SupportTicket> GetAllTickets(string searchText, TicketStatus? status);
    TicketStatusCounts GetStatusCounts();
}
public class TicketOverviewService(ITicketRepository ticketRepository, ICustomerRepository customerRepository) : ITicketOverviewService
{
    private readonly ITicketRepository _ticketRepository = ticketRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;

    

    public IReadOnlyList<SupportTicket> GetAllTickets(string searchText, TicketStatus? status)
    {
        var tickets = _ticketRepository.GetAllTickets();
        var customers = _customerRepository.GetAllCustomers();
        var result = new List<SupportTicket>();

        bool matchesTitle;
        bool matchesCustomer;

        foreach (var ticket in tickets)
        {
            if (status is not null && ticket.Status != status)
                continue;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                result.Add(ticket);
                continue;
            }

            matchesTitle = ticket.Title.Contains(searchText, StringComparison.CurrentCultureIgnoreCase);

            var customer = customers.FirstOrDefault(c => c.Id == ticket.CustomerId);

            matchesCustomer = customer?.Name.Contains(searchText, StringComparison.CurrentCultureIgnoreCase) == true;

            if(matchesTitle || matchesCustomer)
                result.Add(ticket);
        }

        result.Sort((first, second) => second.CreatedAt.CompareTo(first.CreatedAt));

        return result;

    }
    public TicketStatusCounts GetStatusCounts()
    {
        
    }
}
public interface ITicketRepository
{
    IReadOnlyList<SupportTicket> GetAllTickets();
}
public interface ICustomerRepository
{
    IReadOnlyList<Customer> GetAllCustomers();
}
