using SupportTicketSystem.Application.Features.Overviews.Services.MockSupportTicketService;
using SupportTicketSystem.Domain.Features.Overviews.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SupportTicketSystem.Infrastructure.Features.Overviews;

public class JsonSupportTicketRepository : ISupportTicketRepository
{
    private readonly string _filePath = "supportTickets.json";

    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    public async Task AddSupportTicketAsync(SupportTicket supportTicket)
    {
        var allSupportTickets = await GetAllSupportTicketsAsync();

        var ticketList = allSupportTickets.ToList();

        ticketList.Add(supportTicket);

        var jsonFile = JsonSerializer.Serialize(ticketList);

        await File.WriteAllTextAsync(_filePath, jsonFile);

    }

    public async Task EditSupportTicketAsync(SupportTicket supportTicket)
    {
        var allsupportTickets = await GetAllSupportTicketsAsync();

        var ticketsList = allsupportTickets.ToList();

        var existingTickets = ticketsList.FirstOrDefault(ticket  => ticket.Id == supportTicket.Id);

        if (existingTickets is null)
            throw new ArgumentException("Support ticket could not be found.");

        var index = ticketsList.IndexOf(existingTickets);

        ticketsList[index] = supportTicket;

        var jsonFile = JsonSerializer.Serialize(ticketsList);

        await File.WriteAllTextAsync(_filePath, jsonFile);
    }

    public async Task<IReadOnlyList<SupportTicket>> GetAllSupportTicketsAsync()
    {
        if (!File.Exists(_filePath))
            return [];

        var jsonFile = await File.ReadAllTextAsync(_filePath);

        if (string.IsNullOrWhiteSpace(jsonFile))
            return [];

        var supportTickets = JsonSerializer.Deserialize<List<SupportTicket>>(jsonFile);

        return supportTickets ?? [];
    }
}
