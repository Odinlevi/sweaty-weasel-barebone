# Client Registry API 🏢

A  RESTful API built with **ASP.NET Core** for managing client base of individual entrepreneurs (IEs) and legal entities (LLCs), along with their founders.

This project was developed as a technical assessment. Text of assesment can be found in [CHALLENGE.MD](./CHALLENGE.MD).

## 🏗 Architecture & Patterns

*   **Domain-Driven Design (DDD):** Rich domain models (`Client`, `Founder`) with encapsulated business rules.
*   **CQRS (via MediatR):** Strict separation of read (Queries) and write (Commands) operations.
*   **Strongly-Typed IDs:** Uses custom Value Objects (`ClientId`, `FounderId`) to prevent ID-swapping bugs at compile time.
*   **Repository Pattern:** Abstracts Entity Framework Core logic away from the Application layer.
*   **Smart Validation:** FluentValidation pipeline integrated directly into MediatR for fail-fast structural validation.

## 🚀 Technologies Used

*   **.NET 10** (ASP.NET Core Web API)
*   **Entity Framework Core** (Code-First)
*   **PostgreSQL** (Running via Docker)
*   **MediatR** (CQRS implementation)
*   **FluentValidation** (Request validation)
*   **Swagger / OpenAPI** (API Documentation & Testing UI)

## 📋 Prerequisites

To run this project locally, ensure you have the following installed:
*   [.NET 10 SDK](https://dotnet.microsoft.com/download)
*   [Docker Desktop](https://www.docker.com/products/docker-desktop) (or Docker Engine + Docker Compose)

## 🛠 Getting Started

### 1. Spin up the Database
A `docker-compose.yml` file is included in the root directory to easily spin up a PostgreSQL instance.
```bash
docker-compose up -d
```

### 2. Apply Database Migrations
The application uses EF Core Code-First.
To generate the tables, run commands from [Migration.txt](./src/Modules/Modules.ClientRegistry.Infrastructure/EfCore/Migration.txt).

### 3. Run the API
Start the application via your IDE or using the CLI:

```bash
dotnet run
```

### 4. Explore the API
Once running, navigate to the Swagger UI to explore and test the endpoints:
👉 https://localhost:<port>/

You can also find http request examples in [http](./http) folder.

### 5. Notes
 - **Individual Entrepreneurs (ИП)** do not have founders; their INN (ИНН) should be exactly 12 digits long.
 - **Legal Entities (ЮЛ)** must have at least one founders; their INN should be exactly 10 digits long.
 - *Funders (Учредители)* are never considered to be LE; their INN should be exactly 12 digits long; amount of founders is limited to 100 per LE.
 - INNs are only checked for uniquenes among founders of one client;~~~~
