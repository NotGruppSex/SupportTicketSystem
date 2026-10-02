using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportTicketSystem.Application.Features.Customers;
using SupportTicketSystem.Domain.Customers;
using System;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace SupportTicketSystem.Presentation.ViewModels;

public class CustomerViewModel : ObservableObject
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
    public IRelayCommand LoadCustomersCommand { get; }

    public CustomerViewModel(ICustomerService customerService)
    {
        _customerService = customerService;
        LoadCustomersCommand = new RelayCommand(LoadCustomers);
    }

    private void LoadCustomers()
    {
        ErrorMessage = string.Empty;

        try
        {
            var customers = _customerService.GetAll();

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

