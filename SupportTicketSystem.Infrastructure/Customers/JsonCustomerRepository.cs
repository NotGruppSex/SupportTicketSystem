using SupportTicketSystem.Application.Features.Customers;
using SupportTicketSystem.Domain.Customers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace SupportTicketSystem.Infrastructure.Customers;

public class JsonCustomerRepository : ICustomerRepository
{
    private readonly string _filepath;

    public JsonCustomerRepository(string filepath)
    {
        _filepath = Path.GetFullPath(filepath);
    }

    public IReadOnlyList<Customer> GetAll()
    {
        return LoadCustomers();
    }

    public Customer? GetById(Guid id)
    {
        return LoadCustomers()
            .FirstOrDefault(customer => customer.Id == id);
    }

    public void Add(Customer customer)
    {
        var customers = LoadCustomers();

        if (customers.Any(existing => existing.Id == customer.Id))
        {
            throw new InvalidOperationException(" A customer with this ID already exists. ");
        }

        customers.Add(customer);
        SaveCustomers(customers);
    }

    public void Update (Customer customer)
    {
        var customers = LoadCustomers();

        var index = customers.FindIndex(existing  => existing.Id == customer.Id);

        if (index == -1)
        {
            throw new InvalidOperationException(" Customer could not be found. ");
        }

        customers[index] = customer;

        SaveCustomers(customers);

    }

    private List<Customer> LoadCustomers()
    {
        string json;

        try
        {
            json = File.ReadAllText(_filepath);
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
            throw new InvalidDataException("The customer file does not contain a customer list. ");

        }

        var customers = new List<Customer>();
        var usedIds = new HashSet<Guid>();

        foreach (var data in storedCustomers)
        {
            if(data is null)
            {
                throw new InvalidDataException("The customer file contains an invalid entry. ");

            }

            if (!usedIds.Add(data.Id))
            {
                throw new InvalidDataException("The customer file contains duplicate IDs. ");

            }

            var customer = Customer.Restore(data.Id, data.Name, data.Email);

            customers.Add(customer);
        }

        return customers;
    }

    private void SaveCustomers(List<Customer> customers)
    {
        var storedCustomers = new List<CustomerData>();

        foreach (var customer in customers)
        {
            storedCustomers.Add(new CustomerData
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
            });
        }

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(storedCustomers, options);

        var directory = Path.GetDirectoryName(_filepath)!;
        Directory.CreateDirectory(directory);

        var temporaryPath =
            _filepath + "." + Guid.NewGuid() + "tmp";
        
        try
        {
            File.WriteAllText(temporaryPath, json);
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
