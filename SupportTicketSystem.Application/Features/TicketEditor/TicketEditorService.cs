using SupportTicketSystem.Application.Features.TicketRegistration;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationInterfaces;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketPriorityEnum;
using static SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationEnums.TicketStatusEnum;

namespace SupportTicketSystem.Application.Features.TicketEditor;

public class TicketEditorService : ITicketEditorService
{
    private readonly IJsonTicketRegistrationRepository _ticketRepository;

    public TicketEditorService(IJsonTicketRegistrationRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<TicketModel> GetTicketByIdAsync(Guid ticketId)
    {
        var tickets = await _ticketRepository.GetAllTicketsAsync();

        return tickets.FirstOrDefault(ticket => ticket.TicketID == ticketId)
            ?? throw new ArgumentException("Ticket not found.");
    }

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

    public async Task UpdatePriorityAsync(Guid ticketId, TicketPriority priority)
    {
        var tickets = await _ticketRepository.GetAllTicketsAsync();
        var ticket = tickets.FirstOrDefault(ticket => ticket.TicketID == ticketId)
            ?? throw new ArgumentException("Ticket not found.");

        ticket.UpdatePriority(priority);

        await _ticketRepository.SaveAllTicketsAsync(tickets);
    }

    public async Task UpdateStatusAsync(Guid ticketId, TicketStatus status)
    {
        var tickets = await _ticketRepository.GetAllTicketsAsync();
        var ticket = tickets.FirstOrDefault(ticket => ticket.TicketID == ticketId)
            ?? throw new ArgumentException("Ticket not found.");

        ticket.UpdateStatus(status);

        await _ticketRepository.SaveAllTicketsAsync(tickets);
    }

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

    public async Task AddCommentAsync(Guid ticketId, string comment)
    {
        var tickets = await _ticketRepository.GetAllTicketsAsync();
        var ticket = tickets.FirstOrDefault(ticket => ticket.TicketID == ticketId)
            ?? throw new ArgumentException("Ticket not found.");

        ticket.AddComment(comment);

        await _ticketRepository.SaveAllTicketsAsync(tickets);
    }
}