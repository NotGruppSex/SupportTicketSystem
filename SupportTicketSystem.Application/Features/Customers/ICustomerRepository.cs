using SupportTicketSystem.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.Customers;

public interface ICustomerRepository
{
    Task<IReadOnlyList<Customer>> GetAllCustomersAsync();

    Task<Customer?> GetCustomerByIdAsync(Guid id);

    Task AddCustomerAsync(Customer customer);

    Task UpdateCustomerAsync(Customer customer);
}