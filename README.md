# Frågor att ställa
1. Ska våra services utgå från interfaces?
2. 

# SupportTicketSystem
    //Beskriv applikationens syfte
       
# Studenter och Ansvarsområde (Ej tilldelade än)

- Love - Kundhantering

      - Kundvyer               []
      - Validering             []
      - Service                []
      - Lagring av kunder      []
                                             
- Tomas - Ärenderegistrering

      - Formulär nya ärenden   []
      - Kundval                []
      - Registreringsflöde     []
                                             
- William - Ärendehantering

      - Detaljvy               []
      - Tilldelning            []
      - Status                 []
      - Priopritet             []
      - Kommentarer            []
                                             
- Emilia  - Överblick

      - Ärendelista            []
      - Sökning                []
      - Filtrering             []
      - Sammanställning        []

# Gruppens regler

**Katalog och filer**

    Förslag: Varje indelad uppgift har sin egen katalog per lager för att undvika merge-konflikter
    Förslag: Kataloger namnges i plurar, filer i singular
    Förslag: Följer DDD enligt uppgiftens beskrivning
    Förslag: Filer som har med Async, JSON osv att göra har respektive i sina namn

**Beroenden och ansvar**

    Lager              |    Beroende
    -------------------------------------------------------------------------------------
    Domain             |    Oberoende av övriga projekten, gränssnitt och filhantering
    Application        |    Domain (använd repo-interfce för JSON-implementationerna)
    Infrastructure     |    Application + Domain
    Presentation       |    Application + Interface


**MVVM**

    Följer vi MVVM? (ej krav)

**LINQ**

    använder vi LINQ?

**DI**

    Skriver vi DI först och fyller på eller i efterhand?

**Records**

    Använder vi Records?

**Value Objects**

    Använder vi value objects?

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

