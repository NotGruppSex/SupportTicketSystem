using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using SupportTicketSystem.Domain.Customers;
using SupportTicketSystem.Infrastructure.Customers;
using Xunit;

namespace SupportTicketSystem.Tests.Customers;

public class JsonCustomerRepositoryTests
{
    [Fact]
    public async Task Add_ShouldPreserveCustomerWhenLoadedAgain()
    {
        var folderPath = Path.Combine(
            Path.GetTempPath(),
            "SupportTicketSystem.Tests",
            Guid.NewGuid().ToString());

        var filePath = Path.Combine(folderPath, "customers.json");

        try
        {
            // Arrange: skapa repository och kund.
            var repository = new JsonCustomerRepository(filePath);

            var customer = new Customer(
                "Anna Andersson",
                "anna@example.com");

            // Act: spara och läs med ett nytt repository.
            await repository.AddCustomerAsync(customer);

            var newRepository = new JsonCustomerRepository(filePath);

            var loadedCustomer =
                await newRepository.GetCustomerByIdAsync(customer.Id);

            // Assert: kontrollera att uppgifterna finns kvar.
            Assert.True(File.Exists(filePath));
            Assert.NotNull(loadedCustomer);
            Assert.Equal(customer.Id, loadedCustomer.Id);
            Assert.Equal(customer.Name, loadedCustomer.Name);
            Assert.Equal(customer.Email, loadedCustomer.Email);
        }
        finally
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, recursive: true);
            }
        }
    }

    [Fact]
    public async Task Update_ShouldSaveContactDetailsAndPreserveId()
    {
        var folderPath = Path.Combine(
            Path.GetTempPath(),
            "SupportTicketSystem.Tests",
            Guid.NewGuid().ToString());

        var filePath = Path.Combine(folderPath, "customers.json");

        try
        {
            // Arrange: spara en kund och kom ihåg dess id.
            var repository = new JsonCustomerRepository(filePath);

            var customer = new Customer(
                "Anna Andersson",
                "anna@example.com");

            await repository.AddCustomerAsync(customer);

            var originalId = customer.Id;

            // Act: ändra kontaktuppgifterna och spara.
            customer.UpdateDetails(
                "Anna Svensson",
                "anna.svensson@example.com");

            await repository.UpdateCustomerAsync(customer);

            var newRepository = new JsonCustomerRepository(filePath);

            var customers =
                await newRepository.GetAllCustomersAsync();

            // Assert: exakt en kund, samma id och nya uppgifter.
            var loadedCustomer = Assert.Single(customers);

            Assert.Equal(originalId, loadedCustomer.Id);
            Assert.Equal("Anna Svensson", loadedCustomer.Name);
            Assert.Equal(
                "anna.svensson@example.com",
                loadedCustomer.Email);
        }
        finally
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, recursive: true);
            }
        }
    }

    [Fact]
    public async Task Add_ShouldNotOverwriteInvalidJson()
    {
        var folderPath = Path.Combine(
            Path.GetTempPath(),
            "SupportTicketSystem.Tests",
            Guid.NewGuid().ToString());

        var filePath = Path.Combine(folderPath, "customers.json");

        try
        {
            // Arrange: skapa en fil med trasig JSON.
            Directory.CreateDirectory(folderPath);

            var originalContent = "This is not valid JSON.";

            await File.WriteAllTextAsync(filePath, originalContent);

            var repository = new JsonCustomerRepository(filePath);

            var customer = new Customer(
                "Anna",
                "anna@example.com");

            // Act + Assert: felet ska stoppa sparningen.
            await Assert.ThrowsAsync<JsonException>(
                () => repository.AddCustomerAsync(customer));

            var actualContent =
                await File.ReadAllTextAsync(filePath);

            Assert.Equal(originalContent, actualContent);
        }
        finally
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, recursive: true);
            }
        }
    }
}