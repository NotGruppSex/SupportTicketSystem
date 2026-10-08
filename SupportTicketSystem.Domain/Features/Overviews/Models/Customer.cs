using System;
using System.Net.Mail;

namespace SupportTicketSystem.Domain.Features.Overviews.Models;

public class Customer(string name, string emailAddress)
{
    public Guid Id { get; init; } = GenerateId();
    public string Name { get; private set; } = NormalizeName(name);
    public string EmailAddress { get; private set; } = NormalizeEmail(emailAddress);

    private static Guid GenerateId() => Guid.NewGuid();

    private static string NormalizeName(string nameInput)
    {
        if(string.IsNullOrWhiteSpace(nameInput))
            throw new ArgumentNullException("You must enter a valid name. ", nameof(nameInput));
        
        nameInput = nameInput.Trim();

        return nameInput;
    }
    private static string NormalizeEmail(string emailInput)
    {
        if (string.IsNullOrWhiteSpace(emailInput))
            throw new ArgumentNullException("You must enter a valid email!");

        emailInput = emailInput.Trim();

        if (!MailAddress.TryCreate(emailInput, out var validEmail))
            throw new ArgumentException(nameof(emailInput), " is not a valid email!");

        return emailInput;
    }
    public void RenameCustomer(string newName) => Name = NormalizeName(newName);

    public void UpdateEmail(string newEmail) => EmailAddress = NormalizeEmail(newEmail);
}
