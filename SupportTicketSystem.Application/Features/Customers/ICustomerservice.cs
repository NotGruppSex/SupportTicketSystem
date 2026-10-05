using SupportTicketSystem.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.Customers;

public interface ICustomerService
{
    Task<IReadOnlyList<Customer>> GetAllAsync();

    Task<Customer?> GetByIdAsync(Guid id);

    Task<Customer> RegisterAsync(string name, string email);

    Task UpdateContactAsync(Guid id, string name, string email);
}