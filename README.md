# Att göra:
- Installera toolkit
- ändra från .net8 till .net 10


# Frågor att ställa till Hans
1. Ska våra services utgå från interfaces?
2. 

# SupportTicketSystem
    //Beskriv applikationens syfte
       
# Studenter och Ansvarsområde

- William - Kundhantering

      - Kundvyer               []
      - Validering             []
      - Service                []
      - Lagring av kunder      []
                                             
- Love - Ärenderegistrering

      - Formulär nya ärenden   []
      - Kundval                []
      - Registreringsflöde     []
                                             
- Emilia - Ärendehantering

      - Detaljvy               []
      - Tilldelning            []
      - Status                 []
      - Prioritet             []
      - Kommentarer            []
                                             
- Tomas  - Överblick

      - Ärendelista            []
      - Sökning                []
      - Filtrering             []
      - Sammanställning        []

# Gruppens regler

**Arbetsflöde**

1. Vi avsätter gemensamma tider att sitta ner tillsammans i gruppen där vi gör följande:

- Går igenom vart vi är i projektet
- Hjälper varandra
- Sitter och arbetar tillsammans (t.ex. gör sina delar men hjälper varandra, granskar kod och ger feedback)
- Tider: Måndagar kl:10-12 / Torsdagar kl. 10-12 

2. Vi försöker använda oss av ticketsystemet i GitHub

 

**Katalog och filer**

- Varje indelad uppgift har sin egen katalog per lager för att undvika merge-konflikter
- Kataloger namnges i plurar, filer i singular
- Följer DDD enligt uppgiftens beskrivning
- Filer/klasser/metoder som har med Async, JSON osv att göra har respektive i sina namn
- Vi använder oss av MVVM
+ + fler regler vi vill ha

**Beroenden och ansvar**

    Lager              |    Beroende
    -------------------------------------------------------------------------------------
    Domain             |    Oberoende av övriga projekten, gränssnitt och filhantering
    Application        |    Domain (använd repo-interfce för JSON-implementationerna)
    Infrastructure     |    Application + Domain
    Presentation       |    Application + Interface


# FlowChart
<img width="966" height="821" alt="image" src="https://github.com/user-attachments/assets/ab1e92aa-e976-4790-89d3-390023be4394" />


**LINQ**

    använder vi LINQ?

**DI**

    Skriver vi DI först och fyller på eller i efterhand?

**Records**

    Använder vi Records?

    //Ej bestämt än då vi inte förstår detta

**Value Objects**

    Använder vi value objects?

    //Ej bestämt än då vi inte förstår detta

# Projektens ansvar

**Application:** 

    ex: "Samordnar applikationens funktioner och beskriver de kontrakt som behövs"

**Infrastructure:**

    "Läser och skriver information i och från JSON-filer

**Presentation:**

    "Visar information, hanterar anvöndarens inmatning och navigering

**Domain:**

    "Verksamhetens modeller och regler för deras giltiga tillstånd"

# JSON-filerna
    //Beskriv var filerna sparas och hur applikationen kan köras med egna exempeldata


# Resultat av manuell kontroll
    //Beskriv ???

# AI-användning + kontroll
      //Beskriv

