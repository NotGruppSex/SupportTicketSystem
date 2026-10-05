using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportTicketSystem.Application.Features.Customers;
using SupportTicketSystem.Domain.Customers;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SupportTicketSystem.Presentation.ViewModels;

public class CustomersViewModel : ObservableObject
{
    private readonly ICustomerService _customerService;

    public string Title { get; } = "Customers";

    public ObservableCollection<Customer> Customers { get; } = new();

    private string _errorMessage = string.Empty;

    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public IAsyncRelayCommand LoadCustomersCommand { get; }

    public CustomersViewModel(ICustomerService customerService)
    {
        _customerService = customerService;

        LoadCustomersCommand =
            new AsyncRelayCommand(LoadCustomersAsync);
    }

    private async Task LoadCustomersAsync()
    {
        ErrorMessage = string.Empty;

        try
        {
            var customers = await _customerService.GetAllAsync();

            Customers.Clear();

            foreach (var customer in customers)
            {
                Customers.Add(customer);
            }
        }
        catch (JsonException)
        {
            ErrorMessage = "The customer file contains invalid JSON. No data was changed.";
        }
        catch (IOException)
        {
            ErrorMessage = "The customer file could not be read. Check the file and try again.";

        }
        catch (UnauthorizedAccessException)
        {
            ErrorMessage = "You do not have permission to read the customer file.";

        }
        catch (ArgumentException)
        {
            ErrorMessage = "The customer file contains invalid customer details.";

        }
    }
}