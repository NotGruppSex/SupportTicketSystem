using SupportTicketSystem.Application.Features.TicketRegistration;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationInterfaces;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketPriorityEnum;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketStatusEnum;
using SupportTicketSystem.Domain.Features.TicketEditor.TicketEditorDtos;

namespace SupportTicketSystem.Application.Features.TicketEditor;

public class TicketEditorService : ITicketEditorService
{
    // Repository som används för att hämta och spara ärenden i JSON-filen.
    private readonly IJsonTicketRegistrationRepository _ticketRepository;

    public TicketEditorService(IJsonTicketRegistrationRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    // Hämtar ett specifikt ärende med hjälp av dess ID.
    public async Task<TicketModel> GetTicketByIdAsync(Guid ticketId)
    {
        var tickets = await _ticketRepository.GetAllTicketsAsync();

        return tickets.FirstOrDefault(ticket => ticket.TicketID == ticketId)
            ?? throw new ArgumentException("Ticket not found.");
    }

    // Hittar ärendet, uppdaterar titel och beskrivning och sparar ändringarna.
    public async Task UpdateTitleAndDescriptionAsync(
        Guid ticketId,
        string title,
        string description)
    {
        var tickets = await _ticketRepository.GetAllTicketsAsync();
        var ticket = tickets.FirstOrDefault(ticket => ticket.TicketID == ticketId)
            ?? throw new ArgumentException("Ticket not found.");

        ticket.UpdateTitleAndDescription(title, description);

        await _ticketRepository.SaveAllTicketsAsync(tickets);
    }

    // Hittar ärendet, uppdaterar prioritet och sparar ändringarna.
    public async Task UpdatePriorityAsync(Guid ticketId, TicketPriority priority)
    {
        var tickets = await _ticketRepository.GetAllTicketsAsync();
        var ticket = tickets.FirstOrDefault(ticket => ticket.TicketID == ticketId)
            ?? throw new ArgumentException("Ticket not found.");

        ticket.UpdatePriority(priority);

        await _ticketRepository.SaveAllTicketsAsync(tickets);
    }

    // Hittar ärendet, uppdaterar status och sparar ändringarna.
    public async Task UpdateStatusAsync(Guid ticketId, TicketStatus status)
    {
        var tickets = await _ticketRepository.GetAllTicketsAsync();
        var ticket = tickets.FirstOrDefault(ticket => ticket.TicketID == ticketId)
            ?? throw new ArgumentException("Ticket not found.");

        ticket.UpdateStatus(status);

        await _ticketRepository.SaveAllTicketsAsync(tickets);
    }

    // Hittar ärendet, tilldelar eller byter handläggare och sparar ändringarna.
    public async Task UpdateTechnichianAssignedAsync(
        Guid ticketId,
        string technichianAssigned)
    {
        var tickets = await _ticketRepository.GetAllTicketsAsync();
        var ticket = tickets.FirstOrDefault(ticket => ticket.TicketID == ticketId)
            ?? throw new ArgumentException("Ticket not found.");

        ticket.UpdateTechnichianAssigned(technichianAssigned);

        await _ticketRepository.SaveAllTicketsAsync(tickets);
    }

    // Hittar ärendet, lägger till en ny kommentar och sparar ändringarna.
    public async Task AddCommentAsync(Guid ticketId, string comment)
    {
        var tickets = await _ticketRepository.GetAllTicketsAsync();
        var ticket = tickets.FirstOrDefault(ticket => ticket.TicketID == ticketId)
            ?? throw new ArgumentException("Ticket not found.");

        ticket.AddComment(comment);

        await _ticketRepository.SaveAllTicketsAsync(tickets);
    }

    public async Task SaveTicketChangesAsync(SaveTicketChangesRequest request)
    {
        var tickets = await _ticketRepository.GetAllTicketsAsync();
        var ticket = tickets.FirstOrDefault(ticket => ticket.TicketID == request.TicketID)
            ?? throw new ArgumentException("Ticket not found.");

        ticket.InputTitleValidation(request.TicketTitle);
        ticket.InputDescriptionValidation(request.TicketDescription);

        if ((request.Status == TicketStatus.InProgress ||
               request.Status == TicketStatus.Closed) &&
               string.IsNullOrWhiteSpace(request.TechnichianAssigned))
        {
            throw new InvalidOperationException(
                "A technician must be assigned before changing the status.");
        }
        ticket.UpdateTitleAndDescription(request.TicketTitle, request.TicketDescription);
        ticket.UpdatePriority(request.Priority);
        if (request.Status == TicketStatus.New)
        {
            ticket.UpdateStatus(request.Status);
            ticket.UpdateTechnichianAssigned(request.TechnichianAssigned);
        }
        else
        {
            ticket.UpdateTechnichianAssigned(request.TechnichianAssigned);
            ticket.UpdateStatus(request.Status);
        }
        if (!string.IsNullOrWhiteSpace(request.CommentText))
        {
            ticket.AddComment(request.CommentText);
        }
        await _ticketRepository.SaveAllTicketsAsync(tickets);
    }
}