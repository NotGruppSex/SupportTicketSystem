using SupportTicketSystem.Domain.Features.Customers;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using SupportTicketSystem.Infrastructure.Features.Customers;
using SupportTicketSystem.Infrastructure.Features.TicketRegistration.TicketRegistrationRepositories;
using System.Text.Json;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketPriorityEnum;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketStatusEnum;

namespace SupportTicketSystem.Tests.Customers;

public class JsonIntegrationTests
{
    [Fact]
    public async Task AddCustomer_ShouldPreserveDetailsAfterReload() 
        //Kontrollerar Kundens uppgifter och ID finns kvar efter återinläsning.
    {
        var folder = Path.Combine(
            Path.GetTempPath(),
            "SupportTicketSystem.Tests",
            Guid.NewGuid().ToString());

        Directory.CreateDirectory(folder);

        try
        {
            var filePath = Path.Combine(folder, "customers.json");
            var repository = new JsonCustomerRepository(filePath);
            var customer = new Customer("Anna", "anna@example.com");

            await repository.AddCustomerAsync(customer);

            // Läs filen med en ny repository-instans.
            var newRepository = new JsonCustomerRepository(filePath);
            var customers = await newRepository.GetAllCustomersAsync();

            var savedCustomer = Assert.Single(customers);

            Assert.Equal(customer.Id, savedCustomer.Id);
            Assert.Equal("Anna", savedCustomer.Name);
            Assert.Equal("anna@example.com", savedCustomer.Email);
        }
        finally
        {
            Directory.Delete(folder, recursive: true);
        }
    }

    [Fact]
    public async Task UpdateCustomer_ShouldPreserveIdAndSaveNewDetails() 
        //Kontrollerar att ändrade kunduppgifter sparas med samma ID, utan en extra kund.
    {
        var folder = Path.Combine(
            Path.GetTempPath(),
            "SupportTicketSystem.Tests",
            Guid.NewGuid().ToString());

        Directory.CreateDirectory(folder);

        try
        {
            var filePath = Path.Combine(folder, "customers.json");
            var repository = new JsonCustomerRepository(filePath);
            var customer = new Customer("Anna", "anna@example.com");

            await repository.AddCustomerAsync(customer);

            var originalId = customer.Id;

            customer.UpdateDetails(
                "Anna Andersson",
                "anna.new@example.com");

            await repository.UpdateCustomerAsync(customer);

            // Läs filen med en ny repository-instans.
            var newRepository = new JsonCustomerRepository(filePath);
            var customers = await newRepository.GetAllCustomersAsync();

            var savedCustomer = Assert.Single(customers);

            Assert.Equal(originalId, savedCustomer.Id);
            Assert.Equal("Anna Andersson", savedCustomer.Name);
            Assert.Equal("anna.new@example.com", savedCustomer.Email);
        }
        finally
        {
            Directory.Delete(folder, recursive: true);
        }
    }
    [Fact]
    public async Task GetAllCustomers_InvalidJson_ShouldPreserveFile() 
        //kontrollerar att felaktig JSON ger JsonException och filens innehåll förblir oförändrat.
    {
        var folder = Path.Combine(
            Path.GetTempPath(),
            "SupportTicketSystem.Tests",
            Guid.NewGuid().ToString());

        Directory.CreateDirectory(folder);

        try
        {
            var filePath = Path.Combine(folder, "customers.json");
            var invalidJson = "{ this is invalid JSON";

            await File.WriteAllTextAsync(filePath, invalidJson);

            var repository = new JsonCustomerRepository(filePath);

            // Felaktig JSON ska rapporteras som ett fel.
            await Assert.ThrowsAsync<JsonException>(
                () => repository.GetAllCustomersAsync());

            // Filens innehåll ska vara oförändrat.
            var fileContent = await File.ReadAllTextAsync(filePath);

            Assert.Equal(invalidJson, fileContent);
        }
        finally
        {
            Directory.Delete(folder, recursive: true);
        }
    }
    [Fact]
    public async Task CustomerAndTicket_ShouldPreserveDetailsAfterReload()
    //Kontrollerar att kund och ärende läses med nya repository-instanser.
    //Uppgifter, ID, Kundkoppling, handläggare, ändrad status och kommentarer kontrolleras.

    {
        var folder = Path.Combine(
            Path.GetTempPath(),
            "SupportTicketSystem.Tests",
            Guid.NewGuid().ToString());

        Directory.CreateDirectory(folder);

        try
        {
            var customerPath = Path.Combine(folder, "customers.json");
            var ticketPath = Path.Combine(folder, "tickets.json");

            var customerRepository =
                new JsonCustomerRepository(customerPath);

            var ticketRepository =
                new JsonTicketRegistrationRepository(ticketPath);

            // Skapa och spara kunden.
            var customer = new Customer("Anna", "anna@example.com");

            await customerRepository.AddCustomerAsync(customer);

            // Skapa ett ärende som är kopplat till kunden.
            var ticket = new TicketModel(
                "Login problem",
                "Cannot log in.",
                customer,
                TicketPriority.High);

            ticket.UpdateTechnichianAssigned("William");
            ticket.UpdateStatus(TicketStatus.InProgress);
            ticket.AddComment("Investigating the problem.");

            await ticketRepository.SaveAllTicketsAsync(
                new List<TicketModel> { ticket });

            // Läs filerna med nya repository-instanser.
            var newCustomerRepository =
                new JsonCustomerRepository(customerPath);

            var newTicketRepository =
                new JsonTicketRegistrationRepository(ticketPath);

            var customers =
                await newCustomerRepository.GetAllCustomersAsync();

            var tickets =
                await newTicketRepository.GetAllTicketsAsync();

            var savedCustomer = Assert.Single(customers);
            var savedTicket = Assert.Single(tickets);

            // Kundens uppgifter och ID ska finnas kvar.
            Assert.Equal(customer.Id, savedCustomer.Id);
            Assert.Equal(customer.Name, savedCustomer.Name);
            Assert.Equal(customer.Email, savedCustomer.Email);

            // Ärendets uppgifter och ID ska finnas kvar.
            Assert.Equal(ticket.TicketID, savedTicket.TicketID);
            Assert.Equal(ticket.TicketTitle, savedTicket.TicketTitle);
            Assert.Equal(
                ticket.TicketDescription,
                savedTicket.TicketDescription);
            Assert.Equal(ticket.Priority, savedTicket.Priority);
            Assert.Equal(
                ticket.TicketCreationDate,
                savedTicket.TicketCreationDate);

            Assert.Equal("William", savedTicket.TechnichianAssigned);
            Assert.Equal(TicketStatus.InProgress, savedTicket.TicketStatus);

            // Ärendet ska vara kopplat till samma kund.
            Assert.NotNull(savedTicket.CustomerTicket);
            Assert.Equal(savedCustomer.Id, savedTicket.CustomerTicket.Id);
            Assert.Equal(savedCustomer.Name, savedTicket.CustomerTicket.Name);
            Assert.Equal(savedCustomer.Email, savedTicket.CustomerTicket.Email);

            // Kommentaren och dess datum ska finnas kvar.
            var savedComment = Assert.Single(savedTicket.TicketComments);

            Assert.Equal("Investigating the problem.", savedComment.CommentText);
            Assert.Equal(
                ticket.TicketComments[0].CommentDate,
                savedComment.CommentDate);
        }
        finally
        {
            Directory.Delete(folder, recursive: true);
        }
    }
}