# SmartPole-Inventory Tech Stack

## Programming Language
- **C#:** A versatile, strongly typed language, ideal for building robust enterprise-grade applications.
- **.NET:** A unified developer platform for building any type of application across different operating systems and devices.

## Backend Framework
- **ASP.NET Core:** A high-performance, cross-platform framework for building modern, cloud-enabled web APIs and services. It will serve as the core backend for the SmartPole-Inventory system.
- **Onion Architecture:** The backend is organized into concentric layers (Domain, Application, Infrastructure, WebAPI) to ensure high decoupling and testability.
- **MediatR:** Used to implement the CQRS pattern and decouple use case logic from the presentation layer.
- **JWT Bearer Authentication:** Standard-based secure authentication for API endpoints.

## Frontend Framework
- **.NET MAUI (Multi-platform App UI):** A cross-platform framework for creating native mobile and desktop apps with C# and XAML. This will provide a consistent user experience for infrastructure and maintenance teams across various devices.

## Databases
- **PostgreSQL:** A powerful, open-source object-relational database system for structured inventory data. Includes the **PostGIS** extension for high-performance spatial data handling.
- **MongoDB:** A flexible, document-oriented NoSQL database, ideal for handling unstructured or semi-structured data, such as real-time sensor logs and status updates.

## Infrastructure & Deployment
- **Serilog:** Used for structured logging to both console and persistent files, enabling better observability.
- **Docker:** Containerization for consistent development, testing, and production environments.
- **CI/CD:** Automated pipelines for continuous integration and delivery to ensure rapid and reliable software releases.
- **Offline Sync Mechanism:** Batch-based data synchronization protocol for field mobile applications.
