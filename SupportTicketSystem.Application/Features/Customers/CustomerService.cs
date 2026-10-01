using SupportTicketSystem.Domain.Customers;
using System;
using System.Collections.Generic;

namespace SupportTicketSystem.Application.Features.Customers;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public IReadOnlyList<Customer> GetAll()
    {
        return _customerRepository.GetAll();
    }

    public Customer? GetById(Guid id)
    {
        return _customerRepository.GetById(id);
    }

    public Customer Register(string name, string email)
    {
        var customer = new Customer(name, email);

        _customerRepository.Add(customer);

        return customer;
    }

    public void UpdateContact(Guid id, string name, string email)
    {
        var customer = _customerRepository.GetById(id);

        if (customer is null)
        {
            throw new ArgumentException("Customer cannot be found.");
        }

        customer.UpdateContact(name, email);

        _customerRepository.Update(customer);
    }
}