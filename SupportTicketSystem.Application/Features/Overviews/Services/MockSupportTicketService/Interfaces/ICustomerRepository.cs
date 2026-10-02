using SupportTicketSystem.Domain.Features.Overviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.Overviews.Services.MockSupportTicketService.Interfaces;

public interface ICustomerRepository
{
    Task<IReadOnlyList<Customer>> GetAllCustomersAsync();
    Task AddCustomerAsync (Customer customer);
    Task EditCustomerAsync(Customer customer);
}
