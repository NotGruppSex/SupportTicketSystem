using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationInterfaces;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SupportTicketSystem.Infrastructure.Features.TicketRegistration.TicketRegistrationRepositories;

public class JsonTicketRegistrationRepository : IJsonTicketRegistrationRepository
{

    //________________________Prepare the file path and data________________________//

    //Skapar sökväg till filen genom kombinera infromationen i LocalApplicationData (vår .cs fil?) med filnamnet tickets.json. 
    private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "SupportTicketSystem",
        "tickets.json" //TODO - Ev. ändra namn så vi får samma Json-fil som i andra projektet.
        );

    //Förbereder innehållet inför sparande i Json. Gör fin + c-insensitive, konvertera enums till text istället för siffror.
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };


    //____________________________Save tickets____________________________//


    //Skapa mappen, stoppa in filpathen i den efter att den tagit bort filnamnet. Returnerar mappens sökväg. Serialiserar listan enligt optionsformatteringen. Skapat empfilpath för failsafe. Skriver själva filen i vår sökväg med info i json. Sist flyttar filen från temp och skriver över den riktigta filen. 
    public async Task SaveAllTicketsAsync(List<TicketModel> tickets)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath));

        string json = JsonSerializer.Serialize(tickets, _options);

        string tempFilePath = _filePath + ".tmp";

        await File.WriteAllTextAsync(tempFilePath, json);
      
        File.Move(tempFilePath, _filePath, overwrite: true);
    }

    //____________________________Read tickets____________________________//


    //Om filen inte finns, returnera tom lista (förhindra krash?). Läser in fil som finns i filepath till jsonstringen. Deserialisera den enligt optionsfromatteringen. Returnera listan av tickets i c# kod. 
    public async Task<List<TicketModel>> GetAllTicketsAsync()
    {
        if (!File.Exists(_filePath))
            return [];

        string json = await File.ReadAllTextAsync(_filePath);

        var tickets = JsonSerializer.Deserialize<List<TicketModel>>(json, _options)
            ?? throw new JsonException("The file must contain a valid Json list");

        return tickets;
    }
}