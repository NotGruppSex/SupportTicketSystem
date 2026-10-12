using SupportTicketSystem.Domain.Features.Customers;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketStatusEnum;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketPriorityEnum;

namespace SupportTicketSystem.Tests.Domain;

public class TicketModelTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithEmptyTitle_ThrowsArgumentException(string? title)
    {
        var customer = new Customer("Dog Dogsson", "dog@example.com");

        Assert.Throws<ArgumentException>(() => new TicketModel(
            title,
            "Giltig beskrivning",
            customer,
            TicketPriority.Medium));
    }

    [Fact]
    public void Constructor_WithValidInput_CreatesTicketWithStatusNew()
    {
        // Arrange — en giltig kund som konstruktorn kräver
        var customer = new Customer("Dog Dogsson", "dog@example.com");

        // Act — skapa ett giltigt ärende
        var ticket = new TicketModel(
            "Kan inte logga in",
            "Får felmeddelande sedan uppdateringen igår",
            customer,
            TicketPriority.Medium);

        // Assert — ärendet skapades och fick statusen Nytt
        Assert.NotNull(ticket);
        Assert.Equal(TicketStatus.New, ticket.TicketStatus);
    }


}