using CommunityToolkit.Mvvm.ComponentModel;
using SupportTicketSystem.Application.Features.TicketEditor;
using System;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketPriorityEnum;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketStatusEnum;


namespace SupportTicketSystem.Presentation.ViewModels
{
    // ViewModel för att visa och redigera ett befintligt ärende.
    public partial class TicketEditorViewModel : ObservableObject
    {
        // Service som hanterar funktionerna för att arbeta med ett befintligt ärende.
        private readonly ITicketEditorService _ticketEditorService;

        // TicketEditorService skickas in genom dependency injection.
        public TicketEditorViewModel(ITicketEditorService ticketEditorService)
        {
            _ticketEditorService = ticketEditorService;
        }

        // Ärendets rubrik som ska visas och kunna ändras i gränssnittet.
        [ObservableProperty]
        public partial string Title { get; set; } = string.Empty;

        // Ärendets beskrivning som ska visas och kunna ändras i gränssnittet.
        [ObservableProperty]
        public partial string Description { get; set; } = string.Empty;

        // Handläggaren som är tilldelad ärendet.
        [ObservableProperty]
        public partial string AssignedTechnichian { get; set; } = string.Empty;

        // Ärendets ID.
        [ObservableProperty]
        public partial Guid TicketID { get; set; }

        // Ärendets prioritet.
        [ObservableProperty]
        public partial TicketPriority Priority { get; set; }

        // Ärendets status.

        [ObservableProperty]
        public partial TicketStatus Status { get; set; }

        // Kunden som är kopplad till ärendet.
        [ObservableProperty]
        public partial string CustomerName { get; set; } = string.Empty;

        // Texten för en ny kommentar.
        [ObservableProperty]
        public partial string NewComment { get; set; } = string.Empty;

    }
}
