# SupportTicketSystem
Applikationen är en kund- och ärendehanteringsapplikation som hjälper användaren att registrera och hantera sina kunder och ärenden. Den är anpassad så användaren kan avgöra vilka typer av ärenden som ska köras för flexibilitet till användarens fördel. Det finns även en överblicksvy över samtliga ärenden för att underlätta överskådlighet, få all information samtidigt av flera ärenden samt kunna söka och filtrera mellan ärenden. Samtliga uppgifter såsom t.ex. kundens namn, epost, ärendets unika ID, titel, beskrivning, ärendets kund, status, datum, handläggare och kommentarer sparas i form av JSON lokalt. 

       
# Studenter och Ansvarsområde

- William - Kundhantering - för att registrera och uppdatera kunder. Filsparning JSON.                               
- Love - Ärenderegistrering - för att registrera ärenden kopplat till kunden. Filsparning JSON.         
- Emilia - Ärendehantering - för att uppdatera ett ärende, tilldelning av handläggare osv.             
- Tomas  - Översikt - skapar överblick över samtliga ärenden och dess kunder/handläggare samt navigering av applikationen. 

## Kontrollista
- [x] Översiktssidan laddas vid start
- [x] Navigeringsbaren syns och knapparna funkar
- [x] Styles för knappar och headings funkar
- [x] Felmeddelanden om properties saknad när man skapar kund
- [x] Kan skapa en kund och informationen sparas
- [x] Ny kund syns på lista nedan
- [x] Felmeddelanden om properties saknas när man skapar nytt ärende
- [x] Kan välja mellan sparade kunder
- [x] Kan välja mellan prioriteter
- [x] Meddelande om lyckad sparning av ärende
- [ ] LÄGG TILL EMILIAS DELAR
- [x] Räkning av antal ärenden funkar
- [x] Alla ärenden och dess information syns i en lista på översikten
- [x] Sökning funkar
- [x] Filter funkar

### Uppfyllda krav
**Kundhantering**
- Registrera kund med namn och epost
- Kundvy vid registrering
- Validera och trimning inmatning från användaren
- Spara kunder i JSON
- Vid extra tid: kunna redigera kundinformationen

**Ärenderegistrering**
- Registrera ett nytt ärende med krav på titel, beskrivning, prioritet och aktuell kund
- Validering och trimning inmatning från användaren
- Ärendevy vid registrering
- Spara kunder i JSON
- Gemensam funktion: designa styles för gemensamma buttons och headings

**Ärendehantering**
- Ärendehanteringsvy vid hantering av ärenden
- Kunna ändra samtlig data i ärendet förutom statiska properties som t.ex. ID.
- Tilldelning och ändring av handläggare
- Validering av ???
-        Ev fyll på med Emilia

**Översikt**
- Ärendelista så man ser alla ärenden samtidigt
- Sökfunktion av ärenden, titel/namn
- Filtrering av ärenden
- Sammanställning t.ex. av antal ärenden
- Felhantering vid sökning/filtrering
- Separation detaljinformation
-        LÄGG TILL EDITSIDAN
- Gemensam funktion: Skapa navigeringsbaren och dess knappar till separata pages
  
### Kända begränsningar

**Applikationsöverskridande**
- Utvecklad i WinUI, kan därför ej köras i andra OS än Windows.
- Använder JSON så filerna bara sparas lokalt per enhet.
- Saknar skyddande funktioner.

**Kundhantering**
- Inga kända

**Ärenderegistrering**
- Sparar inte JSON filerna där vi skrivit att de ska sparas, Windows sparar annan plats. Kan det vara för att kunderna sparar på en annan plats? 
- Ser ej registrerat ärende i samma vy som det skapades när man registrerat det. 

**Ärendehantering**
- dsa

**Översikt**
- Ökad flexibilitet genom att kunna trycka på varje ärende och komma direkt till hanteringen av det ärendet.
- Inget felmeddelande av sökfunktionen för om det lyckas eller ej

# Installation
       //När klart - lös ev packetering inför installation

# Gruppens regler

## Arbetsflöde

1. Vi avsätter gemensamma tider att sitta ner tillsammans i gruppen där vi gör följande:

- Går igenom vart vi är i projektet
- Går igenom ens kod, frågar och förklarar
- Hjälper varandra vid behov
- Tider: Måndagar kl:10-12 / Torsdagar kl. 10-12 

2. Vi försöker använda oss av ticketsystemet i GitHub.

## Kataloger och filer

- Varje indelad uppgift har sin egen katalog per lager för att undvika merge-konflikter
- Kataloger namnges i plural, filer i singular
- Följer DDD enligt uppgiftens beskrivning men även testat Clean Architechure
- Filer/klasser/metoder som har med Async, JSON osv att göra har respektive i sina namn
- Vi använder oss av MVVM

# Beroenden och ansvar

## Beroenden

    Lager              |    Beroende
    -------------------------------------------------------------------------------------
    Domain             |    Oberoende av de övriga projekten
    Application        |    Domain
    Infrastructure     |    Application + Domain
    Presentation       |    Application + Infrastructure +  Domain

<img width="554" height="388" alt="image" src="https://github.com/user-attachments/assets/0a65299e-7303-413a-8123-0279c9ad9c9f" />


## Projektens ansvar

### Application:
Samordnar och beskriver vad applikationens funktioner ska göra genom separata services.

### Infrastructure:
Beskriver hur information ska sparas och hur/vart den ska hämtas ifrån. Skriver och läser information till och från JSON-filer.

### Presentation:
Presenterar det grafiska för användaren. Visar all information genom ViewModels och pages. Hanterar inmatning, knappar och design av applikationen. Hanterar även navigeringen mellan de olika sidorna som användaren kan bläddra mellan. 

### Domain:
Innehåller applikationens modeller, enums, DTOs och vissa av applikationens interfaces.


# FlowChart
<img width="966" height="821" alt="image" src="https://github.com/user-attachments/assets/ab1e92aa-e976-4790-89d3-390023be4394" />


# Filsparning JSON

**Kunder (customers)**
Kunderna sparas i c:\Users\<användarnamn>\AppData\Local\ som tempfiler.

**Ärenden (tickets)**
Tanken är att ärenden ska sparas i en folder benämnt SupportTicketSystem och filen heter tickets.json

Fullständig sökväg ärenden:
C:\Users\<användarnamn>\AppData\Local\SupportTicketSystem\tickets.json

Dock verkar windows ta över vid sparning och de hamnar någon annan stans.

# Resultat av manuell kontroll
       //Lägg till!

       // Beskrivning från uppgiften: Kontrollera också hela arbetsflödet i gränssnittet: registrera kund, skapa ärende, navigera till detaljer, tilldela handläggare, ändra status, kommentera, söka och filtrera. Starta om applikationen och kontrollera att informationen finns kvar.

Sammanfatta resultatet i en kort checklista i README.md. Ni behöver inte skriva en separat rapport med tio manuella testfall.

Fel som behöver följas upp registreras som GitHub issues med en kort beskrivning av hur de uppstår och hur de rättades. Komplettera relevanta tester när ni rättar fel i testbar logik.
# AI-användning + kontroll

## Customer
 //FYLL PÅ

## TicketRegistration:
- Använt som bollande verktyg: "Jag vill göra X, tipsa/guida hur jag kan göra det?"
- Följdfrågor och specificeringar, förklaringar.
- Har ej bett AI generera kod inuti vår repo.
- Bett den gå igenom vilka steg som behövs för funktion x där man behövt komplettera föreläsningarna.
- Bett den kika på min feature och mina lösningar och komma med förbättringsförslag och hitta brister 

## TicketEditing
 //FYLL PÅ

## Overview 
 //FYLL PÅ

