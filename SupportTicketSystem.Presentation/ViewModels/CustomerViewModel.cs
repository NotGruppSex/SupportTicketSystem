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
    private string _customerName = string.Empty;
    private string _customerEmail = string.Empty;
    private string _statusMessage = string.Empty;

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
        public string CustomerName
        {
             get
            {
            return _customerName;
            }

        set
        {
            SetProperty(ref _customerName, value);
        }
    }

    public string CustomerEmail
    {
        get
        {
            return _customerEmail;
        }

        set
        {
            SetProperty(ref _customerEmail, value);
        }
    }

    public string StatusMessage
    {
        get
        {
            return _statusMessage;
        }

        set
        {
            SetProperty(ref _statusMessage, value);
        }
    }
    

    [RelayCommand]
    private async Task LoadCustomersAsync()
    {
        ErrorMessage = string.Empty;
        StatusMessage = string.Empty;

        try
        {
            var customers = await _customerService.GetAllCustomersAsync();


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

    [RelayCommand]

    private async Task RegisterCustomerAsync()
    {
        ErrorMessage = string.Empty;
        StatusMessage = string.Empty;

        try
        {
            var customer = await _customerService.RegisterCustomerAsync(CustomerName, CustomerEmail);

            Customers.Add(customer);

            CustomerName = string.Empty;
            CustomerEmail = string.Empty;

            StatusMessage = "Customer saved.";
        }
        catch (ArgumentException exception)
        {
            ErrorMessage = exception.Message;
        }
        catch (JsonException)
        {
            ErrorMessage = "The customer file contains invalid JSON. The customer was not saved.";

        }
        catch (IOException)
        {
            ErrorMessage = "The customer could not be saved. Check the customer file and try again.";

        }
        catch (UnauthorizedAccessException)
        {
            ErrorMessage = "The app does not have permission to access the customer file.";

        }
        catch (InvalidOperationException exception)
        {
            ErrorMessage = exception.Message;
        }
  
    }

}