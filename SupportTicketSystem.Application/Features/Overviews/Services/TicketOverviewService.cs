using SupportTicketSystem.Application.Features.Customers;
using SupportTicketSystem.Domain.Features.Overviews.Models;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationInterfaces;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.Overviews.Services;

public class TicketOverviewService(IJsonTicketRegistrationRepository supporticketRepository, ICustomerRepository customerRepository) : ITicketOverviewService


{
    public async Task<IReadOnlyList<TicketModel>> SearchTicketsAsync(string searchText)
    {
        var tickets = await supporticketRepository.GetAllTicketsAsync();
        var customers = await customerRepository.GetAllCustomersAsync();
        var result = new List<TicketModel>();

        bool matchesTitle;
        bool matchesCustomer;

        foreach (var ticket in tickets)
        {
            //Söka efter status filtert, ej implementerat helt
            //Om ticket status ej stämmer med inmatning, fortsätt

            //Lägger till i listan om ingen inmatning anges
            if (string.IsNullOrWhiteSpace(searchText))
            {
                result.Add(ticket);
                continue;
            }

            matchesTitle = ticket.TicketTitle.Contains(searchText, StringComparison.CurrentCultureIgnoreCase);

            //Detta kan ge null
            var customer = customers.FirstOrDefault(c => c.Id == ticket.CustomerTicket.Id);

            //Alltså behöver 'customer' vara nullable och '==true' gör så att bool-värdet blir false om det är null
            matchesCustomer = customer?.Name.Contains(searchText, StringComparison.CurrentCultureIgnoreCase) == true;

            if (matchesTitle || matchesCustomer)
                result.Add(ticket);
        }

        result.Sort((first, second) => second.TicketCreationDate.CompareTo(first.TicketCreationDate));

        return result;

    }
    public async Task <TicketStatusCount> GetTicketStatusCountsAsync()
    {
        int newCount = 0;
        int inProgressCount = 0;
        int resolvedCount = 0;

        //Mer tydlig lösning än med LINQ
        var tickets = await supporticketRepository.GetAllTicketsAsync();

        foreach (var ticket in tickets)
        {
            switch (ticket.TicketStatus)
            {
                case TicketStatusEnum.TicketStatus.New:
                    newCount++;
                    break;

                case TicketStatusEnum.TicketStatus.InProgress:
                    inProgressCount++;
                    break;

                case TicketStatusEnum.TicketStatus.Closed:
                    resolvedCount++;
                    break;
            }
        }

        var ticketStatusCountResult = new TicketStatusCount(newCount, inProgressCount, resolvedCount);

        return ticketStatusCountResult;
    }
}
