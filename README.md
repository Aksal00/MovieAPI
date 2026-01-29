# MovieAPI

This is an **ASP.NET Core C# Web API** that fetches Spider-Man movie data from the **OMDb public API**, stores it in a **MS SQL Server database**, and exposes it via RESTful endpoints. The API implements **local caching**: if data is already in the database, it will be returned from there; otherwise, it fetches from OMDb, saves it, and returns it.

---

## Features

- Fetches movie data from [OMDb API](https://www.omdbapi.com/)
- Stores data in **MS SQL Server** without using an ORM (raw SQL queries)
- RESTful endpoints:
  - Get all movies
  - Get a movie by IMDb ID
- Local caching: data is fetched from the API only if not in the database

---

## Technologies & Libraries

- **.NET 9.0 / ASP.NET Core 9 Web API**
- **Microsoft.Data.SqlClient** – database access without ORM
- **Scalar.AspNetCore** – OpenAPI documentation
- **HttpClient** – for external API calls
- **SQL Server LocalDB** – lightweight database engine for development

---

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- Visual Studio 2022+ (optional, recommended) or VS Code
- OMDb API key (register [here](https://www.omdbapi.com/apikey.aspx))
- **SQL Server LocalDB**:  
  - Installed automatically with Visual Studio if using workloads: 
    - “.NET desktop development” or “ASP.NET and web development”
  - Otherwise, install via [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

---

## 1. Clone the repository

```bash
git clone <repository-url>
cd MovieAPI
```
## 2. Set environment variable for OMDb API key

- Windows (PowerShell):
```bash
setx OMDB_API_KEY "your_api_key_here"
```

- Linux/macOS::
```bash
export OMDB_API_KEY="your_api_key_here"
```

## 3. Set up the database

- Open PowerShell or Command Prompt and run:

```bash
sqllocaldb create "MoviesDb"
```
```bash
sqllocaldb start "MoviesDb"
```

- Open SQL Server Management Studio (SSMS) (or any SQL client that can connect to LocalDB), connect to the instance (localdb)\MoviesDb, and execute the included DatabaseSchema.sql file.

- The API is configured to use this LocalDB instance by default in appsettings.json

## 4. Run the API

- Restore dependencies and build:
```bash
dotnet build
```
- Run the API:
```bash
dotnet run
```
