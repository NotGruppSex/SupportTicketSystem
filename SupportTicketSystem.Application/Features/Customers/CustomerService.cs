using SupportTicketSystem.Domain.Features.Customers;
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

    public Task<IReadOnlyList<Customer>> GetAllCustomersAsync()
    {
        return _customerRepository.GetAllCustomersAsync();
    }

    public Task<Customer?> GetCustomerByIdAsync(Guid customerId)
    {
        return _customerRepository.GetCustomerByIdAsync(customerId);
    }

    public async Task<Customer> RegisterCustomerAsync(string name,string email)
    {
        var customer = new Customer(name, email);

        await _customerRepository.AddCustomerAsync(customer);

        return customer;
    }

    public async Task UpdateCustomerDetailsAsync(Guid customerId, string name, string email)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(customerId);

        if (customer is null)
        {
            throw new ArgumentException("Customer could not be found.");
        }

        customer.UpdateDetails(name, email);

        await _customerRepository.UpdateCustomerAsync(customer);
    }
}