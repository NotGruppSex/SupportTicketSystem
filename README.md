# SupportTicketSystem
- Applikationen hjälper användaren att registrera kunder och ärenden.
- De kan skrivas se aktuella ärenden som direkt kopplas till kunderna.
- De kan redigera ärendena utefter arbetets gång.
- Underlättar hantering av ärenden och sparar informationen mellan appens användningar. 
       
# Studenter och Ansvarsområde

- William - Kundhantering                                        
- Love - Ärenderegistrering                                       
- Emilia - Ärendehantering                                            
- Tomas  - Överblick

# Gruppens regler

**Arbetsflöde**

1. Vi avsätter gemensamma tider att sitta ner tillsammans i gruppen där vi gör följande:

- Går igenom vart vi är i projektet
- Går igenom ens kod, frågar och förklarar
- Hjälper varandra vid behov
- Tider: Måndagar kl:10-12 / Torsdagar kl. 10-12 

2. Vi försöker använda oss av ticketsystemet i GitHub.

**Kataloger och filer**

- Varje indelad uppgift har sin egen katalog per lager för att undvika merge-konflikter
- Kataloger namnges i plural, filer i singular
- Följer DDD enligt uppgiftens beskrivning
- Filer/klasser/metoder som har med Async, JSON osv att göra har respektive i sina namn
- Vi använder oss av MVVM

**Beroenden och ansvar**

    Lager              |    Beroende
    -------------------------------------------------------------------------------------
    Domain             |    Oberoende av de övriga projekten, gränssnitt och filhantering
    Application        |    Domain (använd repo-interface för JSON-implementationerna)
    Infrastructure     |    Application + Domain
    Presentation       |    Application + Interface


# FlowChart
<img width="966" height="821" alt="image" src="https://github.com/user-attachments/assets/ab1e92aa-e976-4790-89d3-390023be4394" />


**LINQ**

    använder vi LINQ?

**DI**
Dependency injection skriver vi per feature. Det kan antingen följa med i samma branch som featuren eller som en separat pull request.

Vi har använt oss av DI som kontaktnät i vår applikation.

**Records och Value Objects**
Var utvecklare bestämmer om record eller value objects används i sin feature. 

# Projektens ansvar

**Application:** 

    ex: "Samordnar applikationens funktioner och beskriver de kontrakt som behövs"
Samordnar hela applikationens funktioner genom separata services för de olika funktionerna. Den beskriver också vilka interfaces som behövs genom en DI serviceklass.

**Infrastructure:**

    "Läser och skriver information i och från JSON-filer

Skriver och läser information till och från JSON-filer. Beskriver vilka interfaces den behöver genom DI serviceklass.


**Presentation:**

    "Visar information, hanterar anvöndarens inmatning och navigering

Visar all information genom ViewModels och pages. Hanterar inmatning, knappar och design av applikationen. Hanterar även navigeringen mellan de olika sidorna som användaren kan bläddra mellan.

**Domain:**

    "Verksamhetens modeller och regler för deras giltiga tillstånd"

Innehåller applikationens modeller, enums, DTOs och applikationens interfaces OBS ÄNDRA EV DENNA BEROENDE PÅ HUR VI GÖR

# JSON-filerna
    //Beskriv var filerna sparas och hur applikationen kan köras med egna exempeldata


# Resultat av manuell kontroll
    //Beskriv ???

# AI-användning + kontroll

**Customer**
 //FYLL PÅ

**TicketRegistration:** 

- Använt som bollande verktyg: "Jag vill göra X, tipsa/guida hur jag kan göra det?"
- Följdfrågor och specificeringar, förklaringar.
- Har ej bett AI generera kod inuti vår repo.
- Bett den gå igenom vilka steg som behövs för funktion x där man behövt komplettera föreläsningarna.
- Bett den kika på min feature och komma med förbättringsförslag och hitta brister 

**TicketEditing**
 //FYLL PÅ

**Overview**
 //FYLL PÅ

