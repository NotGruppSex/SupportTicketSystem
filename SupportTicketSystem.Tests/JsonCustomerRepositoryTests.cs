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
            // Arrange
            var repository = new JsonCustomerRepository(filePath);

            var customer = new Customer(
                "Anna Andersson",
                "anna@example.com");

            // Act
            await repository.AddAsync(customer);

            var newRepository = new JsonCustomerRepository(filePath);

            var loadedCustomer =
                await newRepository.GetByIdAsync(customer.Id);

            // Assert
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
            // Arrange
            var repository = new JsonCustomerRepository(filePath);

            var customer = new Customer(
                "Anna Andersson",
                "anna@example.com");

            await repository.AddAsync(customer);

            var originalId = customer.Id;

            // Act
            customer.UpdateContact(
                "Anna Svensson",
                "anna.svensson@example.com");

            await repository.UpdateAsync(customer);

            var newRepository = new JsonCustomerRepository(filePath);
            var customers = await newRepository.GetAllAsync();

            // Assert
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
            // Arrange
            Directory.CreateDirectory(folderPath);

            var originalContent = "This is not valid JSON.";

            await File.WriteAllTextAsync(filePath, originalContent);

            var repository = new JsonCustomerRepository(filePath);
            var customer = new Customer("Anna", "anna@example.com");

            // Act + Assert
            await Assert.ThrowsAsync<JsonException>(
                () => repository.AddAsync(customer));

            var actualContent = await File.ReadAllTextAsync(filePath);

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