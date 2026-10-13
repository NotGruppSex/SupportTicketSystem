using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportTicketSystem.Application.Features.TicketEditor;
using System;
using System.Linq;
using System.Threading.Tasks;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketPriorityEnum;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketStatusEnum;
using SupportTicketSystem.Domain.Features.TicketEditor;
using System.Collections.Generic;
using SupportTicketSystem.Domain.Features.TicketEditor.TicketEditorDtos;


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

        // Felmeddelande som visas om en ändring av ärendet inte kan genomföras.
        [ObservableProperty]
        public partial string ErrorMessage { get; set; } = string.Empty;


        // Fördefinierade handläggare som användaren kan välja mellan.
        public IReadOnlyList<string> Technichians { get; } =
        [
            "Emilia",
            "Love",
            "Tomas",
            "William"
        ];

        // Prioriteter som användaren kan välja mellan.
        public IReadOnlyList<TicketPriority> Priorities { get; } =
            Enum.GetValues<TicketPriority>();

        // Statusar som användaren kan välja mellan.
        public IReadOnlyList<TicketStatus> Statuses { get; } =
            Enum.GetValues<TicketStatus>();

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
            Comments = ticket.TicketComments
                .OrderBy(comment => comment.CommentDate)
                .ToList();
        }


        [RelayCommand]
        // Sparar ändringar i ärendets titel och beskrivning.
        public async Task UpdateTitleAndDescriptionAsync()
        {
            try
            {
                ErrorMessage = string.Empty;

                await _ticketEditorService.UpdateTitleAndDescriptionAsync(
                    TicketID,
                    Title,
                    Description);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        [RelayCommand]
        // Uppdaterar ärendets prioritet.
        public async Task UpdatePriorityAsync()
        {
            try
            {
                ErrorMessage = string.Empty;

                await _ticketEditorService.UpdatePriorityAsync(
                    TicketID,
                    Priority);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        [RelayCommand]
        // Uppdaterar ärendets status.
        public async Task UpdateStatusAsync()
        {
            try
            {
                ErrorMessage = string.Empty;

                await _ticketEditorService.UpdateStatusAsync(
                    TicketID,
                    Status);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        [RelayCommand]
        // Tilldelar eller byter handläggare på ärendet.
        public async Task UpdateTechnichianAssignedAsync()
        {
            try
            {
                ErrorMessage = string.Empty;

                await _ticketEditorService.UpdateTechnichianAssignedAsync(
                    TicketID,
                    AssignedTechnichian);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        [RelayCommand]
        // Lägger till en ny kommentar på ärendet.
        public async Task AddCommentAsync()
        {
            try
            {
                ErrorMessage = string.Empty;

                await _ticketEditorService.AddCommentAsync(
                    TicketID,
                    NewComment);

                var ticket = await _ticketEditorService.GetTicketByIdAsync(TicketID);

                Comments = ticket.TicketComments
                    .OrderBy(comment => comment.CommentDate)
                    .ToList();

                NewComment = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        [RelayCommand]
        public async Task SaveTicketChangesAsync()
        {
            var request = new SaveTicketChangesRequest(
                 TicketID,
                 Title,
                 Description,
                 Priority,
                 Status,
                 AssignedTechnichian,
                 NewComment);

            await _ticketEditorService.SaveTicketChangesAsync(request);
        }
    }

}   
