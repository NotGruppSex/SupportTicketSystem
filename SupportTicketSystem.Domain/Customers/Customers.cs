using System;
using System.Net.Mail;

namespace SupportTicketSystem.Domain.Customers;

public class Customer
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }

    public Customer(string name, string email)
        : this(Guid.NewGuid(), name, email)
    { 
    
    }

    private Customer(Guid id, string name, string email)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Customer id is required. ");
        }

        Validate(name, email);

        Id = id;
        Name = name.Trim();
        Email = email.Trim();
    }

    public static Customer Restore(Guid id, string name,string email)
    {
        return new Customer(id, name, email);
    }
    public void UpdateContact(string name, string email)
    {
        Validate(name, email);

        Name = name.Trim();
        Email = email.Trim();

    }

    private static void Validate(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Customer name is required.");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Customer E-mail is required.");
        }

        var trimmedEmail = email.Trim();

        if (!MailAddress.TryCreate(trimmedEmail, out var address)
            || address.Address != trimmedEmail)
        {
            throw new ArgumentException("E-mailaddress must be a valid format.");
        }

    }
}
