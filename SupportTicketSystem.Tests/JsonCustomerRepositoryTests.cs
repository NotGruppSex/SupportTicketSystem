using System;
using System.IO;
using SupportTicketSystem.Domain.Customers;
using SupportTicketSystem.Infrastructure.Customers;
using Xunit;
using System.Text.Json;

namespace SupportTicketSystem.Tests.Customers;

public class JsonCustomerRepositoryTests
{
    [Fact]
    public void Add_ShouldPreserveCustomerWhenLoadedAgain()
    {
        // Arrange: skapa en unik testmapp och en kund.
        var folderPath = Path.Combine(
            Path.GetTempPath(),
            "SupportTicketSystem.Tests",
            Guid.NewGuid().ToString());

        var filePath = Path.Combine(folderPath, "customers.json");

        try
        {
            var repository = new JsonCustomerRepository(filePath);

            var customer = new Customer(
                "Anna Andersson",
                "anna@example.com");

            // Act: spara och läs tillbaka med ett nytt repository.
            repository.Add(customer);

            var newRepository = new JsonCustomerRepository(filePath);
            var loadedCustomer = newRepository.GetById(customer.Id);

            // Assert: kontrollera att uppgifterna finns kvar.
            Assert.True(File.Exists(filePath));
            Assert.NotNull(loadedCustomer);
            Assert.Equal(customer.Id, loadedCustomer.Id);
            Assert.Equal(customer.Name, loadedCustomer.Name);
            Assert.Equal(customer.Email, loadedCustomer.Email);
        }
        finally
        {
            // Ta endast bort den unika mapp som testet skapade.
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, recursive: true);
            }
        }
    }
    [Fact]
    public void Update_ShouldSaveContactDetailsAndPreserveId()
    {
        // Arrange: skapa en kund och spara den.
        var folderPath = Path.Combine(
            Path.GetTempPath(),
            "SupportTicketSystem.Tests",
            Guid.NewGuid().ToString());

        var filePath = Path.Combine(folderPath, "customers.json");

        try
        {
            var repository = new JsonCustomerRepository(filePath);

            var customer = new Customer(
                "Anna Andersson",
                "anna@example.com");

            repository.Add(customer);

            var originalId = customer.Id;

            // Act: ändra uppgifterna och spara uppdateringen.
            customer.UpdateContact(
                "Anna Svensson",
                "anna.svensson@example.com");

            repository.Update(customer);

            // Läs tillbaka från filen med ett nytt repository.
            var newRepository = new JsonCustomerRepository(filePath);
            var customers = newRepository.GetAll();

            // Assert: uppdateringen ska inte skapa en extra kund.
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
    public void Add_ShouldNotOverwriteInvalidJson()
    {
        // Arrange: skapa en fil med ogiltig JSON.
        var folderPath = Path.Combine(
            Path.GetTempPath(),
            "SupportTicketSystem.Tests",
            Guid.NewGuid().ToString());

        var filePath = Path.Combine(folderPath, "customers.json");

        try
        {
            Directory.CreateDirectory(folderPath);

            var originalContent = "This is not valid JSON.";
            File.WriteAllText(filePath, originalContent);

            var repository = new JsonCustomerRepository(filePath);
            var customer = new Customer("Anna", "anna@example.com");

            // Act + Assert: inläsningsfelet ska stoppa sparningen.
            Assert.Throws<JsonException>(() => repository.Add(customer));

            // Filens befintliga innehåll ska vara oförändrat.
            var actualContent = File.ReadAllText(filePath);

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