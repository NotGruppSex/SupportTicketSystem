using Microsoft.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;
using SupportTicketSystem.Presentation.ViewModels;

namespace SupportTicketSystem.Presentation.Pages;

public sealed partial class TicketEditorPage : Page
{
    public TicketEditorViewModel ViewModel { get; }
    public TicketEditorPage()
    {
        ViewModel = App.ServiceProvider.GetRequiredService<TicketEditorViewModel>();
        InitializeComponent();
    }
}
