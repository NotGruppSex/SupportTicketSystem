using SupportTicketSystem.Domain.Features.TicketRegistration.TicketRegistrationModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SupportTicketSystem.Infrastructure.Features.TicketRegistration.TicketRegistrationRepositories;

public class Json_TicketRegistration_Repository : IJson_TicketRegistration_Repository
{

    //____________________________Prepare the file path and data____________________________

    //Skapa sökväg genom att kombinera LocalApplicationData med mappen SupportTicketSystem och filnamnet tickets.json - what
    private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "SupportTicketSystem",
        "tickets.json"
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

    //____________________________Save tickets____________________________

    //Create
    public async Task SaveAllTicketsAsync(IEnumerable<TicketModel> tickets)
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

}
//Read

//Läsa av Customers 

