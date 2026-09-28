using CommunityToolkit.Mvvm.ComponentModel;
using SupportTicketSystem.Presentation.Navigation;

namespace SupportTicketSystem.Presentation.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    public string Title { get; set; } = "Home Page";

    public HomeViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
}
