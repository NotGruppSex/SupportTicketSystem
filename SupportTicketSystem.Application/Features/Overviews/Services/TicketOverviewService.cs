using SupportTicketSystem.Domain.Features.Overviews;
using SupportTicketSystem.Domain.Features.Overviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.Overviews.Services;

public class TicketOverviewService(ITicketRepository ticketRepository, ICustomerRepository customerRepository) : ITicketOverviewService
{
    private readonly ITicketRepository _ticketRepository = ticketRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;

    

    public async Task<IReadOnlyList<SupportTicket>> GetAllTicketsAsync(string searchText, TicketStatus? status)
    {
        var tickets = await _ticketRepository.GetAllTicketsAsync();
        var customers = await _customerRepository.GetAllCustomersAsync();
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
    public async Task <TicketStatusCount> GetTicketStatusCountsAsync()
    {
        int newCount = 0;
        int inProgressCount = 0;
        int resolvedCount = 0;

        var tickets = await _ticketRepository.GetAllTicketsAsync();

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
