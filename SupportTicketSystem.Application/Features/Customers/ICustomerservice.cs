using SupportTicketSystem.Domain.Customers;
using System;
using System.Collections.Generic;

namespace SupportTicketSystem.Application.Features.Customers;

public interface ICustomerService
{
    IReadOnlyList<Customer> GetAll();

    Customer? GetById(Guid id);

    Customer Register(string name, string email);

    void UpdateContact(Guid id, string name, string email);
}