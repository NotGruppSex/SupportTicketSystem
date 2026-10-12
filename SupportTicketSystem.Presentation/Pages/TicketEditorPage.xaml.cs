using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SupportTicketSystem.Presentation.ViewModels;
using System;

namespace SupportTicketSystem.Presentation.Pages;

public sealed partial class TicketEditorPage : Page
{
    public TicketEditorViewModel ViewModel { get; }
    public TicketEditorPage()
    {
        ViewModel = App.ServiceProvider.GetRequiredService<TicketEditorViewModel>();
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is string ticketId &&
            Guid.TryParse(ticketId, out Guid id))
        {
            await ViewModel.LoadTicketAsync(id);
        }
    }
}
