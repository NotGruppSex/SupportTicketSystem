using SupportTicketSystem.Domain.Features.Overviews;
using SupportTicketSystem.Domain.Features.Overviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupportTicketSystem.Application.Features.Overviews;

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
    public TicketStatusCount GetTicketStatusCounts()
    {
        int newCount = 0;
        int inProgressCount = 0;
        int resolvedCount = 0;

        var tickets = _ticketRepository.GetAllTickets();

        foreach (var ticket in tickets)
        {
            switch (ticket.Status)
            {
                case TicketStatus.New:
                    newCount++;
                    break;

                case TicketStatus.InProgress:
                    inProgressCount++;
                    break;

                case TicketStatus.Resolved:
                    resolvedCount++;
                    break;
            }
        }

        var ticketStatusCountResult = new TicketStatusCount(newCount, inProgressCount, resolvedCount);

        return ticketStatusCountResult;
    }
}
