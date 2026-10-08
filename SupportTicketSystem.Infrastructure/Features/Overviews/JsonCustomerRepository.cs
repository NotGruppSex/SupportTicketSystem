using SupportTicketSystem.Application.Features.Overviews.Services.MockSupportTicketService.Interfaces;
using SupportTicketSystem.Domain.Features.Overviews.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SupportTicketSystem.Infrastructure.Features.Overviews;

public class JsonCustomerRepository : ICustomerRepository
{
    private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"customers.json");
    public async Task AddCustomerAsync(Customer customer)
    {
        var customers = await GetAllCustomersAsync();

        var customerList = customers.ToList();

        customerList.Add(customer);

        var jsonFile = JsonSerializer.Serialize(customerList);

        await File.WriteAllTextAsync(_filePath, jsonFile);
    }

    public async Task EditCustomerAsync(Customer customer)
    {
        var customers = await GetAllCustomersAsync();

        var customerList = customers.ToList();

        var existingCustomer = customerList.FirstOrDefault(c => c.Id == customer.Id);

        if (existingCustomer is null)
            throw new ArgumentNullException("Customer could not be found.");

        var index = customerList.IndexOf(existingCustomer);

        customerList[index] = customer;

        var jsonFile = JsonSerializer.Serialize(customerList);

        await File.WriteAllTextAsync(_filePath, jsonFile);
    }

    public async Task<IReadOnlyList<Customer>> GetAllCustomersAsync()
    {
        if (!File.Exists(_filePath))
            return [];

        var jsonFile = await File.ReadAllTextAsync(_filePath);

        if (string.IsNullOrWhiteSpace(jsonFile))
            return [];

        var customers = JsonSerializer.Deserialize<List<Customer>>(jsonFile);

        return customers ?? [];
    }
}
