using SupportTicketSystem.Application.Features.Overviews.Services;
using SupportTicketSystem.Domain.Features.Customers;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationInterfaces;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketPriorityEnum;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketStatusEnum;

namespace SupportTicketSystem.Tests;

public class TicketOverviewServiceTests
{
    // Kontrollera om sökning är skriftkänsligt eller ej
    [Theory]
    [InlineData("LOGIN")]
    [InlineData("login")]
    [InlineData("LoGiN")]
    [InlineData("DOG")]
    [InlineData("dog")]
    public async Task SearchTicketsAsync_WithDifferentCasing_ReturnsMatchingTicket(
        string searchText)
    {
        // Arrange
        var matchingTicket = new TicketModel(
            "Login problem",
            "Cannot log in.",
            new Customer("Dog Dogsson", "dog@example.com"),
            TicketPriority.Medium);

        var otherTicket = new TicketModel(
            "Printer problem",
            "Cannot print.",
            new Customer("Olof Andersson", "olof@example.com"),
            TicketPriority.Medium);

        var repository = new InMemoryTicketRepository(
            new List<TicketModel> { matchingTicket, otherTicket });

        var service = new TicketOverviewService(repository);

        // Act
        var result = await service.SearchTicketsAsync(searchText, null);

        // Assert
        var ticket = Assert.Single(result);
        Assert.Equal(matchingTicket.TicketID, ticket.TicketID);
    }

    //Kontrollerar att filtering fungerar med statusfiltret endast visar ärenden med vald status
    // tar ej med andra Tickets med annat status.
    [Theory]
    [InlineData(TicketStatus.New)]
    [InlineData(TicketStatus.InProgress)]
    [InlineData(TicketStatus.Closed)]
    public async Task SearchTicketsAsync_WithStatusFilter_ReturnsOnlySelectedStatus(
        TicketStatus selectedStatus)
    {
        // Arrange
        var customer = new Customer("Dog Dogsson", "dog@example.com");

        var newTicket = new TicketModel(
            "Login problem",
            "Cannot log in.",
            customer,
            TicketPriority.Medium);

        var inProgressTicket = new TicketModel(
            "Printer problem",
            "Cannot print.",
            customer,
            TicketPriority.Medium);

        inProgressTicket.UpdateTechnichianAssigned("Orgrim Doomhammer");
        inProgressTicket.UpdateStatus(TicketStatus.InProgress);

        var closedTicket = new TicketModel(
            "Network problem",
            "Connection restored.",
            customer,
            TicketPriority.Medium);

        closedTicket.UpdateTechnichianAssigned("Orgrim Doomhammer");
        closedTicket.UpdateStatus(TicketStatus.Closed);

        var tickets = new List<TicketModel>
        {
            newTicket,
            inProgressTicket,
            closedTicket
        };

        var repository = new InMemoryTicketRepository(tickets);
        var service = new TicketOverviewService(repository);

        var expectedTicket = tickets.Single(
            ticket => ticket.TicketStatus == selectedStatus);

        // Act
        var result = await service.SearchTicketsAsync(
            string.Empty, selectedStatus);

        // Assert
        var ticket = Assert.Single(result);
        Assert.Equal(expectedTicket.TicketID, ticket.TicketID);
        Assert.Equal(selectedStatus, ticket.TicketStatus);
    }

    // Spara i InMemory så att det ej skrivs in i filerna.
    private sealed class InMemoryTicketRepository
        : IJsonTicketRegistrationRepository
    {
        private List<TicketModel> _tickets;

        public InMemoryTicketRepository(List<TicketModel> tickets)
        {
            _tickets = new List<TicketModel>(tickets);
        }

        public Task<List<TicketModel>> GetAllTicketsAsync()
        {
            return Task.FromResult(new List<TicketModel>(_tickets));
        }

        public Task SaveAllTicketsAsync(List<TicketModel> tickets)
        {
            _tickets = new List<TicketModel>(tickets);
            return Task.CompletedTask;
        }
    }
}