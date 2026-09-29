using System;
using System.Net.Mail;

namespace SupportTicketSystem.Domain.Customers;

public class Customer
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }


    public Customer(string name, string email)
    {
        Validate(name, email);

        Id = Guid.NewGuid();
        Name = name.Trim();
        Email = email.Trim();

    }

    public void UpdateContact(string name, string email)
    {
        Validate(name, email);

        Name = name.Trim();
        Email =email.Trim();

    }

    private static void Validate(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Kundens namn måste vara ifyllt.");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Kundens email måste vara ifylld.");
        }

        var trimmedEmail = email.Trim();

        if (!MailAddress.TryCreate(trimmedEmail, out var address)
            || address.Address != trimmedEmail)
        {
            throw new ArgumentException("E-postadressen har ett ogiligt format.");
        }

    }
}
