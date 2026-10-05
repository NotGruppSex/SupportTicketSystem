using SupportTicketSystem.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.Customers;

public interface ICustomerRepository
{
    Task<IReadOnlyList<Customer>> GetAllAsync();

    Task<Customer?> GetByIdAsync(Guid id);

    Task AddAsync(Customer customer);

    Task UpdateAsync(Customer customer);
}