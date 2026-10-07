using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportTicketSystem.Domain.Features.Customers;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SupportTicketSystem.Presentation.ViewModels;

public partial class CustomersViewModel : ObservableObject
{
    private readonly ICustomerService _customerService;
    private string _errorMessage = string.Empty;

    public CustomersViewModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public ObservableCollection<Customer> Customers { get; } = [];

    public string ErrorMessage
    {
        get
        {
            return _errorMessage;
        }

        set
        {
            SetProperty(ref _errorMessage, value);
        }
    }

    [RelayCommand]
    private async Task LoadCustomersAsync()
    {
        ErrorMessage = string.Empty;

        try
        {
            var customers =
                await _customerService.GetAllCustomersAsync();

            Customers.Clear();

            foreach (var customer in customers)
            {
                Customers.Add(customer);
            }
        }
        catch (JsonException)
        {
            ErrorMessage = "The customer file contains invalid JSON or an unexpected data format. No data was changed.";
        }
        catch (IOException)
        {
            ErrorMessage = "The customer file could not be loaded. Check that it is accessible and contains valid customer data.";
        }
        catch (UnauthorizedAccessException)
        {
            ErrorMessage = "The app does not have permission to read the customer file.";
        }
        catch (ArgumentException)
        {
            ErrorMessage = "The customer file contains invalid customer details.";
        }
    }
}