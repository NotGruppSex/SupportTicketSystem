using SupportTicketSystem.Application.Features.Customers;
using SupportTicketSystem.Domain.Features.Customers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SupportTicketSystem.Infrastructure.Features.Customers;

public class JsonCustomerRepository : ICustomerRepository
{
    private readonly string _filepath;

    public JsonCustomerRepository(string filepath)
    {
        _filepath = Path.GetFullPath(filepath);
    }

    public async Task<IReadOnlyList<Customer>> GetAllCustomersAsync()
    {
        return await LoadCustomersAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(Guid id)
    {
        var customers = await LoadCustomersAsync();

        return customers.FirstOrDefault(customer => customer.Id == id);
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        var customers = await LoadCustomersAsync();

        if (customers.Any(existing => existing.Id == customer.Id))
        {
            throw new InvalidOperationException("A customer with this ID already exists.");
        }

        customers.Add(customer);

        await SaveCustomersAsync(customers);
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        var customers = await LoadCustomersAsync();

        var index = customers.FindIndex(existing => existing.Id == customer.Id);

        if (index == -1)
        {
            throw new InvalidOperationException("Customer could not be found.");
        }

        customers[index] = customer;

        await SaveCustomersAsync(customers);
    }

    private async Task<List<Customer>> LoadCustomersAsync()
    {
        string json;

        try
        {
            json = await File.ReadAllTextAsync(_filepath);
        }
        catch (FileNotFoundException)
        {
            return new List<Customer>();
        }
        catch (DirectoryNotFoundException)
        {
            return new List<Customer>();
        }

        var storedCustomers = JsonSerializer.Deserialize<List<CustomerData>>(json);

        if (storedCustomers is null)
        {
            throw new InvalidDataException("The customer file does not contain a customer list.");
        }

        var customers = new List<Customer>();
        var usedIds = new HashSet<Guid>();

        foreach (var data in storedCustomers)
        {
            if (data is null)
            {
                throw new InvalidDataException("The customer file contains an invalid entry.");
            }

            if (!usedIds.Add(data.Id))
            {
                throw new InvalidDataException("The customer file contains duplicate IDs.");
            }

            var customer = Customer.Restore(data.Id, data.Name, data.Email);

            customers.Add(customer);
        }

        return customers;
    }

    private async Task SaveCustomersAsync(List<Customer> customers)
    {
        var storedCustomers = new List<CustomerData>();

        foreach (var customer in customers)
        {
            storedCustomers.Add(new CustomerData
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            });
        }

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(storedCustomers, options);

        var directory = Path.GetDirectoryName(_filepath)!;
        Directory.CreateDirectory(directory);

        var temporaryPath = _filepath + "." + Guid.NewGuid() + ".tmp";

        try
        {
            await File.WriteAllTextAsync(temporaryPath, json);

            File.Move(temporaryPath, _filepath, overwrite: true);
        }
        finally
        {
          
            try
            {
                File.Delete(temporaryPath);
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
        }
    }
}