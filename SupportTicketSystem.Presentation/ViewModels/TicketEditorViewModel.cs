using CommunityToolkit.Mvvm.ComponentModel;
using SupportTicketSystem.Application.Features.TicketEditor;
using System;
using System.Threading.Tasks;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketPriorityEnum;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketStatusEnum;
using SupportTicketSystem.Domain.Features.TicketEditor;
using System.Collections.Generic;


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

        // Kommentarerna som finns på ärendet.
        [ObservableProperty]
        public partial IReadOnlyList<TicketComment> Comments { get; set; } = [];


        // Laddar ett ärende med hjälp av dess ID och fyller ViewModelen med ärendets information.
        public async Task LoadTicketAsync(Guid ticketId)
        {
            var ticket = await _ticketEditorService.GetTicketByIdAsync(ticketId);

            TicketID = ticket.TicketID;
            Title = ticket.TicketTitle;
            Description = ticket.TicketDescription;
            Priority = ticket.Priority;
            Status = ticket.TicketStatus;
            AssignedTechnichian = ticket.TechnichianAssigned;
            CustomerName = ticket.CustomerTicket.Name;
            Comments = ticket.TicketComments;
        }

        // Sparar ändringar i ärendets titel och beskrivning.
        public async Task UpdateTitleAndDescriptionAsync()
        {
            await _ticketEditorService.UpdateTitleAndDescriptionAsync(
                TicketID,
                Title,
                Description);
        }

        // Uppdaterar ärendets prioritet.
        public async Task UpdatePriorityAsync()
        {
            await _ticketEditorService.UpdatePriorityAsync(
                TicketID,
                Priority);
        }

        // Uppdaterar ärendets status.
        public async Task UpdateStatusAsync()
        {
            await _ticketEditorService.UpdateStatusAsync(
                TicketID,
                Status);
        }

        // Tilldelar eller byter handläggare på ärendet.
        public async Task UpdateTechnichianAssignedAsync()
        {
            await _ticketEditorService.UpdateTechnichianAssignedAsync(
                TicketID,
                AssignedTechnichian);
        }

        // Lägger till en ny kommentar på ärendet.
        public async Task AddCommentAsync()
        {
            await _ticketEditorService.AddCommentAsync(
                TicketID,
                NewComment);

            var ticket = await _ticketEditorService.GetTicketByIdAsync(TicketID);
            Comments = ticket.TicketComments;

            NewComment = string.Empty;
        }

    }
}
