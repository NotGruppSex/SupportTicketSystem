using SupportTicketSystem.Domain.Features.Customers;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketPriorityEnum;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketStatusEnum;

namespace SupportTicketSystem.Tests;

public class TicketEditorTests
{
    // Testar att ett ärende utan tilldelad handläggare inte kan ändra status
    // till InProgress. Testet ska bekräfta att ett undantag kastas.
    [Fact]
    public void UpdateStatus_WithoutTechnichian_ThrowsInvalidOperationException()
    {
        // Arrange - Skapa ett ärende utan handläggare
        var customer = new Customer("Anna Andersson", "anna@example.com");

        var ticket = new TicketModel(
            "Computer problem",
            "Computer does not start",
            customer,
            TicketPriority.Medium);

        // Act & Assert - Försök ändra status och kontrollera att det stoppas
        Assert.Throws<InvalidOperationException>(() =>
            ticket.UpdateStatus(TicketStatus.InProgress));
    }

    // Testar att ett ärende med tilldelad handläggare kan ändra status
    // till InProgress. Testet ska bekräfta att statusen uppdateras korrekt.
    [Fact]
    public void UpdateStatus_WithTechnichian_ChangesStatusToInProgress()
    {
        // Arrange - Skapa ett ärende och tilldela en handläggare
        var customer = new Customer("Anna Andersson", "anna@example.com");

        var ticket = new TicketModel(
            "Computer problem",
            "Computer does not start",
            customer,
            TicketPriority.Medium);

        ticket.UpdateTechnichianAssigned("Anna");

        // Act - Ändra status till Pågående
        ticket.UpdateStatus(TicketStatus.InProgress);

        // Assert - Kontrollera att statusen ändrades
        Assert.Equal(TicketStatus.InProgress, ticket.TicketStatus);
    }
}

