using SupportTicketSystem.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportTicketSystem.Application.Features.Customers;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Task<IReadOnlyList<Customer>> GetAllAsync()
    {
        return _customerRepository.GetAllAsync();
    }

    public Task<Customer?> GetByIdAsync(Guid id)
    {
        return _customerRepository.GetByIdAsync(id);
    }

    public async Task<Customer> RegisterAsync(
        string name,
        string email)
    {
        var customer = new Customer(name, email);

        await _customerRepository.AddAsync(customer);

        return customer;
    }

    public async Task UpdateContactAsync(
        Guid id,
        string name,
        string email)
    {
        var customer = await _customerRepository.GetByIdAsync(id);

        if (customer is null)
        {
            throw new ArgumentException("Customer could not be found.");
        }

        customer.UpdateContact(name, email);

        await _customerRepository.UpdateAsync(customer);
    }
}