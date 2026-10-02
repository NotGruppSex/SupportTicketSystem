using System;

namespace SupportTicketSystem.Domain.Features.Overviews.Models;

public class SupportTicket(Guid customerId, string title, string description)
{
    public Guid Id { get; init; } = GenerateId();
    public string Title { get; private set; } = NormalizeText(title);
    public string Description { get; private set; } = NormalizeText(description);
    public TicketStatus Status { get; private set; } = TicketStatus.New;
    public DateTime CreatedAt { get; init; } = DateTime.Now;

    private static Guid GenerateId() => Guid.NewGuid();

    private static string NormalizeText(string inputText)
    {
        if(string.IsNullOrWhiteSpace(inputText))
            throw new ArgumentNullException("You must enter a valid text.", nameof(inputText));

        inputText = inputText.Trim();

        return inputText;
    }
    public void ChangeTicketStatus(TicketStatus newStatus)
    {
        if (newStatus == Status)
            throw new ArgumentException("Ticket already has this status.");

        Status = newStatus;
    }
    public void ChangeDescription(string description) => Description = NormalizeText(description);

    public void ChangeTitle(string title) => Title = NormalizeText(title);

}
