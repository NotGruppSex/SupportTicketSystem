using System;
using System.IO;
using SupportTicketSystem.Domain.Customers;
using SupportTicketSystem.Infrastructure.Customers;
using Xunit;

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
}