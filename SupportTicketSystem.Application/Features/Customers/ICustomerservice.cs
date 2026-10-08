using SupportTicketSystem.Domain.Features.Customers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICustomerService
{
    Task<IReadOnlyList<Customer>> GetAllCustomersAsync();

    Task<Customer?> GetCustomerByIdAsync(Guid customerId);

    Task<Customer> RegisterCustomerAsync(string name, string email);

    Task UpdateCustomerDetailsAsync(Guid customerId, string name, string email);
        
}