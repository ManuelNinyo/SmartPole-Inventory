# SmartPole-Inventory

**SmartPole-Inventory** is a comprehensive solution for real-time management of smart pole infrastructure. It consists of a high-performance ASP.NET Core backend and a cross-platform .NET MAUI mobile application, designed for infrastructure teams and maintenance technicians to track, monitor, and maintain smart poles.

## 🚀 Key Features

- **Inventory Tracking:** Cataloging smart pole locations and specifications.
- **Inspection Management:** Interactive map-based inspections and maintenance logs.
- **Offline Support:** Reliable field synchronization using SQLite and offline map support via MBTiles.
- **Spatial Data:** Advanced geographic handling using PostGIS.
- **Secure Access:** Role-based access control with JWT authentication.

## 🛠️ Technical Stack

### Backend
- **Framework:** ASP.NET Core (Onion Architecture)
- **CQRS/Mediator:** MediatR
- **Databases:** PostgreSQL + PostGIS (structured/spatial), MongoDB (logs/unstructured)
- **Auth:** JWT Bearer
- **Logging:** Serilog

### Mobile (MAUI)
- **Architecture:** MVVM (CommunityToolkit.Mvvm)
- **Mapping:** Mapsui (MBTiles for offline maps)
- **Local DB:** SQLite (sqlite-net-pcl)

### Infrastructure
- **Containerization:** Docker (PostGIS, pgAdmin)
- **Management:** `conductor` extension for task/phase tracking

## 📂 Project Structure

```text
SmartPole-Inventory/
├── conductor/               # Project management & guidelines (Source of Truth)
├── src/                     # Backend Solutions (ASP.NET Core)
│   ├── Application/         # Business logic & MediatR handlers
│   ├── Domain/              # Core entities & interfaces
│   ├── Infrastructure/      # Data persistence & external services
│   ├── WebAPI/              # API endpoints & configuration
│   └── *Tests/              # Unit & Integration tests
├── mobile/                  # Mobile Solution (.NET MAUI)
│   ├── SmartPole.Inventory.App/        # MAUI UI Project
│   ├── SmartPole.Inventory.MobileCore/ # Shared mobile logic & Persistence
│   └── SmartPole.Inventory.UnitTests/  # Mobile unit tests
└── docker-compose.yml       # Infrastructure setup
```

## 🏗️ Getting Started

### Prerequisites
- .NET 9.0 SDK
- Docker Desktop

### Infrastructure Setup
Spin up the required databases and infrastructure:
```bash
docker-compose up -d
```

### Backend
Run the backend API:
```bash
dotnet run --project src/SmartPole.Inventory.WebAPI/SmartPole.Inventory.WebAPI.csproj
```

### Mobile
Build the mobile solution (Windows/Android/iOS/Mac):
```bash
dotnet build mobile/SmartPole.Inventory.Mobile.sln
```

### Testing
Run all tests:
```bash
dotnet test
```

## 📝 License
This project is licensed under the MIT License - see the LICENSE file for details.
