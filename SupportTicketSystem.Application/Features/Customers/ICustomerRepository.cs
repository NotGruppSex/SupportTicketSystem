using SupportTicketSystem.Domain.Customers;
using System;
using System.Collections.Generic;

namespace SupportTicketSystem.Application.Features.Customers;

public interface ICustomerRepository
{
    IReadOnlyList<Customer> GetAll();

    Customer? GetById(Guid id);

    void Add(Customer customer);

    void Update(Customer customer);

}
