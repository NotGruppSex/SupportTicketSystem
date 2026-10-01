using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistration_Interfaces;
using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SupportTicketSystem.Infrastructure.Features.TicketRegistration.TicketRegistrationRepositories;

public class Json_TicketRegistration_Repository : IJson_TicketRegistration_Repository //TODO - lös!
{

    //________________________Prepare the file path and data________________________//

    //Skapa sökväg genom att kombinera LocalApplicationData med mappen SupportTicketSystem och filnamnet tickets.json - what
    private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "SupportTicketSystem",
        "tickets.json" //TODO - Ev. ändra namn så vi får samma Json-fil som i andra projektet.
        );

    //Förbereder innehållet inför sparande i Json. 
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        //Gör utskrift "fin"
        WriteIndented = true,

        //Gör innehållet case-insensitive
        PropertyNameCaseInsensitive = true,

        //Konverterar mina enums till text istället för siffror.
        Converters = { new JsonStringEnumConverter() }
    };


    //____________________________Save tickets____________________________//

    public async Task SaveAllTicketsAsync(List<TicketModel> tickets) //TODO - denna var tidigare Ienum - varför?
    {
        //Skapar mappen om den inte finns genom metoden CreatDirectory. Vi stoppar sen in sökvägen till filen i metoden Path.GetDirectoryName som tar bort filnamnet och returnerar mappens sökväg.
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath));

        //Ändrar formatet från c# av listan av tickets till Json text, vi använder formatteringen som vi beskrivit i _options.
        string json = JsonSerializer.Serialize(tickets, _options);

        //Temporär path för failsafe om något går sönder, så vi inte råkar förstöra grundfilen.
        string tempFilePath = _filePath + ".tmp";

        //Skriver filen genom metod WriteAllTextAsync genom att skicka in vår temporära filepath och vår serialiserade json text.
        await File.WriteAllTextAsync(tempFilePath, json);

        //Vi flyttar nu filen från temp. till riktig och skriver över. 
        File.Move(tempFilePath, _filePath, overwrite: true);
    }



    //____________________________Read tickets____________________________//

    public async Task<List<TicketModel>> GetAllTicketsAsync()
    {
        //Om filen inte finns, returnera en tom lista för att förhindra krash
        if (!File.Exists(_filePath))
            return [];

        //Läser in filen och returnerar en lista av TicketModel.
        string json = await File.ReadAllTextAsync(_filePath);

        //Text om det hela misslyckas. Annars skapar lista av ticketmodel som vi deserialiserat från json. ?? kollar om det är null. Om det är null -> felmeddelande. Om inte null -> returnera listan.
        var tickets = JsonSerializer.Deserialize<List<TicketModel>>(json, _options)
            ?? throw new JsonException("The file must contain a valid Json list");

        //Skickar tillbaka listan
        return tickets;
    }
}