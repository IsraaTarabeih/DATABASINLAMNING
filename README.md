README - DatabasInlämning 

Översikt - 
Det här är ett konsolprogram för att hantera en liten butik, byggt i C# och Entity Framework Core.
Applikationen hanterar Kategorier, Kunder, Produkter, Ordrar och Orderrader och erbjuder fullständig 
CRUD-funktionalitet för alla entiteter. 

Systemet använder en lokal SQLite-databas och innehåller både migrationer och seeding av data.

Funktioner - 
CRUD för alla entiteter 
* Kategorier: Lista, skapa, redigera, ta bort.
* Kunder: Lista, skapa, redigera, ta bort.
* Produkter: Lista, skapa, redigera, ta bort.
* Ordrar: Lista, skapa, redigera, ta bort.
* Orderrader: Skapas som en del av orderflödet.

Databas - 
* Byggd med Entity Framework Core
* Lokal SQLite-databas (shop.db)
* Migrationer inkluderade
* Automatisk seeding av:
* Kategorier
* Kunder
* Produkter
* Ordrar och Orderrader

Validering & Felhantering -
* Kontrollerar ID:n, namn, priser, e-post och datum
* Förhindrar ogiltliga relationer
* Visar tydliga felmeddelanden

Navigering i menyn -
* Tydlig huvudmeny
* Undermenyer för varje entitet
* Användaren kan alltid gå tillbaka till huvudmenyn

Körning -
1. Klona projektet
2. Öppna i Visual Studio
3. Kör applikationen
4. Databasen skapas automatiskt och fylls med initial data

Krav -
* .NET 8.0
* Entity Framework Core
* SQLite

  Lycka till!
  
