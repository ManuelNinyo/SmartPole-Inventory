# SmartPole-Inventory Project Context

## Project Overview
**SmartPole-Inventory** is a comprehensive system for real-time management of smart pole infrastructure. It consists of a high-performance ASP.NET Core backend and a cross-platform .NET MAUI mobile application, designed for infrastructure teams and maintenance technicians to track, monitor, and maintain smart poles.

### Key Features
- **Inventory Tracking:** Cataloging smart pole locations and specifications.
- **Inspection Management:** Interactive map-based inspections and maintenance logs.
- **Offline Support:** Reliable field synchronization using SQLite and offline map support via MBTiles.
- **Spatial Data:** Advanced geographic handling using PostGIS.
- **Secure Access:** Role-based access control with JWT authentication.

## Technical Stack
- **Languages:** C# (.NET 9.0)
- **Backend:**
    - **Framework:** ASP.NET Core (Onion Architecture)
    - **CQRS/Mediator:** MediatR
    - **Databases:** PostgreSQL + PostGIS (structured/spatial), MongoDB (logs/unstructured)
    - **Auth:** JWT Bearer
    - **Logging:** Serilog
- **Mobile (MAUI):**
    - **Architecture:** MVVM (CommunityToolkit.Mvvm)
    - **Mapping:** Mapsui (MBTiles for offline maps)
    - **Local DB:** SQLite (sqlite-net-pcl)
- **Infrastructure:**
    - **Containerization:** Docker (PostGIS, pgAdmin)
    - **Management:** `conductor` extension for task/phase tracking

## Project Structure
```text
SmartPole-Inventory/
├── conductor/               # Project management & guidelines (Source of Truth)
├── src/                     # Backend Solutions
│   ├── Application/         # Business logic & MediatR handlers
│   ├── Domain/              # Core entities & interfaces
│   ├── Infrastructure/      # Data persistence & external services
│   ├── WebAPI/              # API endpoints & configuration
│   └── *Tests/              # Unit & Integration tests
├── mobile/                  # Mobile Solution
│   ├── SmartPole.Inventory.App/        # MAUI UI Project
│   ├── SmartPole.Inventory.MobileCore/ # Shared mobile logic & Persistence
│   └── SmartPole.Inventory.UnitTests/  # Mobile unit tests
└── docker-compose.yml       # Infrastructure setup
```

## Development Workflow
The project follows a strict **TDD (Test-Driven Development)** workflow managed via the `conductor` extension.

1. **Plan Driven:** Refer to `conductor/tracks.md` and specific track `plan.md` files for tasks.
2. **Red-Green-Refactor:** Always write failing tests before implementation.
3. **Quality Gates:** 
    - >90% code coverage.
    - Strict adherence to `conductor/code_styleguides/`.
    - Verification protocol for every phase (checkpointing).
4. **Task Lifecycle:** Mark tasks as in-progress `[~]`, completed `[x]`, and attach git notes with summaries.

## Building and Running

### Prerequisites
- .NET 9.0 SDK
- Docker Desktop

### Infrastructure
```bash
docker-compose up -d
```

### Backend
```bash
# From root
dotnet run --project src/SmartPole.Inventory.WebAPI/SmartPole.Inventory.WebAPI.csproj
```

### Mobile
```bash
# Build (Windows/Android/iOS/Mac)
dotnet build mobile/SmartPole.Inventory.Mobile.sln
```

### Testing
```bash
# Run all tests
dotnet test

# Run tests with coverage (example for Linux/macOS)
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov
```

## Documentation
- **Product Vision:** `conductor/product.md`
- **Tech Stack Details:** `conductor/tech-stack.md`
- **Workflow Guide:** `conductor/workflow.md`
- **Style Guides:** `conductor/code_styleguides/`
