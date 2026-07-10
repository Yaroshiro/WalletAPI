WebAPI - Wallets Example
=========================

Overview
--------
This is a small ASP.NET Core Web API that demonstrates a basic RESTful CRUD service for a "Wallet" resource using Entity Framework Core (InMemory provider by default). 
It includes DTO mapping, async endpoints, and OpenAPI support. Implemented automated tests for ASP.NET Core Web API endpoints using xUnit and Entity Framework Core InMemory provider.

Tech
----
- .NET 10
- ASP.NET Core Web API
- EF Core (InMemory provider)
- OpenAPI (development only)

Quick start
-----------
Prerequisites:
- .NET 10 SDK installed (dotnet --version)

Run locally:
1. Open a terminal in the project folder (solution root where WebAPI.csproj lives).
2. Restore/build and run:
   dotnet restore
   dotnet run
3. The API will be available at https://localhost:5001 (or http://localhost:5000) depending on Kestrel configuration.

OpenAPI / Swagger
-----------------
OpenAPI is enabled in development (see Program.cs). Visit the configured OpenAPI/Swagger endpoint (for example /swagger or /openapi) to explore the API interactively.

Endpoints
---------
Base route: /api/Wallets

- GET /api/Wallets
  - Returns list of wallets (WalletDTO)
  - curl: curl https://localhost:5001/api/Wallets

- GET /api/Wallets/{id}
  - Returns wallet by id
  - curl: curl https://localhost:5001/api/Wallets/1

- POST /api/Wallets
  - Creates a new wallet. Body: JSON WalletDTO without Id.
  - Example body:
	{
	  "name": "My Wallet",
	  "balance": 100.50
	}
  - curl: curl -X POST -H "Content-Type: application/json" -d '{"name":"My","balance":10.0}' https://localhost:5001/api/Wallets

- PUT /api/Wallets/{id}
  - Replaces/updates Name and Balance using WalletDTO.

- PATCH /api/Wallets/{id}
  - Partially update Name or Balance using WalletDTO (nullable fields allowed).

- DELETE /api/Wallets/{id}
  - Deletes wallet.

Notes
-----
- The project uses an in-memory database by default (Program.cs: UseInMemoryDatabase("WalletDB")). This is convenient for demos/tests but not for production.
- To use SQL Server or another provider, update Program.cs to UseSqlServer with a connection string and add EF Core migrations.
- There is minimal validation and error handling in the sample. Add FluentValidation or DataAnnotations + a global error handler for production readiness.

License
-------
Add a LICENSE file to the repo (MIT recommended for portfolio/demo projects).

Contact
-------
This repo is a small demo. If you want, I can add tests, CI, or a Dockerfile — tell me which and I will create them.
