# Track Specification: Infrastructure Setup - CI Pipelines and Docker Compose

## Overview
This track focuses on establishing the core CI/CD infrastructure using GitHub Actions for the Backend and MAUI application, along with a local development environment powered by Docker Compose featuring PostgreSQL with PostGIS.

## Functional Requirements
### CI Pipelines (GitHub Actions)
- **Backend CI:**
    - Triggered on push and pull requests to the main branch.
    - Steps:
        - Setup .NET SDK.
        - Run `dotnet restore`.
        - Run `dotnet format --verify-no-changes` (Lint/Format Check).
        - Run `dotnet build`.
        - Run `dotnet test`.
        - Publish the API project and generate artifacts.
- **MAUI App CI:**
    - Triggered on push and pull requests to the main branch.
    - Steps:
        - Setup .NET SDK (with MAUI workloads).
        - Run `dotnet restore`.
        - Run `dotnet build` for targeted platforms (e.g., Windows, Android).
        - Run unit tests for shared libraries and UI tests (if any).
        - Generate build artifacts for the app.

### Docker Compose
- **PostgreSQL Service:**
    - Image: `postgis/postgis:16-3.4` (PostgreSQL 16 with PostGIS 3).
    - Environment Variables: Configure credentials (e.g., `POSTGRES_USER`, `POSTGRES_PASSWORD`).
    - Volume: Configure persistent storage for data.
    - Port Mapping: Expose port `5432` to the host.
- **pgAdmin Service:**
    - Image: `dpage/pgadmin4`.
    - Configure login credentials.
    - Expose on a specific local port (e.g., `5050`).
    - Connect automatically to the PostgreSQL container.

## Non-Functional Requirements
- **Performance:** Pipelines should be optimized for speed (caching dependencies).
- **Maintainability:** YAML files should be well-commented and modular.
- **Security:** Do not hardcode sensitive credentials; use environment variables or GitHub Secrets (where applicable).

## Acceptance Criteria
- [ ] GitHub Actions workflows for both Backend and MAUI pass on a clean commit.
- [ ] Backend pipeline produces a published artifact.
- [ ] `docker compose up` successfully starts both PostgreSQL and pgAdmin.
- [ ] PostgreSQL is accessible via pgAdmin and contains the PostGIS extension.
- [ ] Data persists in the PostgreSQL container after a restart.

## Out of Scope
- Deployment to production environments (CD).
- Setting up a CI pipeline for mobile-specific signing (e.g., iOS certificates).
- Detailed unit testing implementation (only running existing/skeleton tests).
