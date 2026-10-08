using SupportTicketSystem.Application.Features.Overviews.Services.MockSupportTicketService.Interfaces;
using SupportTicketSystem.Domain.Features.Overviews;
using SupportTicketSystem.Domain.Features.Overviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.Overviews.Services;

public class TicketOverviewService(ISupportTicketRepository supporticketRepository, ICustomerRepository customerRepository) : ITicketOverviewService
{
    public async Task<IReadOnlyList<SupportTicket>> SearchTicketsAsync(string searchText, TicketStatus? status)
    {
        var tickets = await supporticketRepository.GetAllSupportTicketsAsync();
        var customers = await customerRepository.GetAllCustomersAsync();
        var result = new List<SupportTicket>();

        bool matchesTitle;
        bool matchesCustomer;

        foreach (var ticket in tickets)
        {
            //Söka efter status filtert, ej implementerat helt
            //Om ticket status ej stämmer med inmatning, fortsätt
            if (status is not null && ticket.Status != status) 
                continue;


            //Lägger till i listan om ingen inmatning anges
            if (string.IsNullOrWhiteSpace(searchText))
            {
                result.Add(ticket);
                continue;
            }

            matchesTitle = ticket.Title.Contains(searchText, StringComparison.CurrentCultureIgnoreCase);

            //Detta kan ge null
            var customer = customers.FirstOrDefault(c => c.Id == ticket.CustomerId);

            //Alltså behöver 'customer' vara nullable och '==true' gör så att bool-värdet blir false om det är null
            matchesCustomer = customer?.Name.Contains(searchText, StringComparison.CurrentCultureIgnoreCase) == true;

            if (matchesTitle || matchesCustomer)
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

        //Mer tydlig lösning än med LINQ
        var tickets = await supporticketRepository.GetAllSupportTicketsAsync();

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
