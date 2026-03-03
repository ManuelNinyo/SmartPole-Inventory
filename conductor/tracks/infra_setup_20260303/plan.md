# Implementation Plan: Infrastructure Setup - CI Pipelines and Docker Compose

## Phase 1: Local Infrastructure with Docker Compose

- [x] Task: Create `docker-compose.yml` for PostgreSQL and PostGIS [87bb5ed]
    - [x] Define PostgreSQL 16 image with PostGIS extension (`postgis/postgis:16-3.4`)
    - [x] Configure environment variables for credentials and database name
    - [x] Define persistent volume for PostgreSQL data storage
    - [x] Map host port 5432 to container port 5432

- [~] Task: Add pgAdmin to `docker-compose.yml`
    - [ ] Define pgAdmin 4 image (`dpage/pgadmin4`)
    - [ ] Configure login credentials for pgAdmin (email/password)
    - [ ] Expose pgAdmin on host port 5050
    - [ ] Configure server connection file (servers.json) for automatic discovery (optional but recommended)

- [ ] Task: Verify local infrastructure
    - [ ] Run `docker compose up -d` and check container logs
    - [ ] Connect to PostgreSQL via pgAdmin and run `SELECT PostGIS_Full_Version();`
    - [ ] Verify data persistence by restarting the container and checking for existing data

- [ ] Task: Conductor - User Manual Verification 'Phase 1: Local Infrastructure with Docker Compose' (Protocol in workflow.md)

## Phase 2: Backend CI Pipeline

- [ ] Task: Create GitHub Actions workflow for Backend
    - [ ] Create `.github/workflows/backend-ci.yml`
    - [ ] Define triggers for push and pull requests on the main branch
    - [ ] Implement steps for: Setup .NET SDK, Restore dependencies

- [ ] Task: Add Lint, Build, and Test steps to Backend CI
    - [ ] Add step for `dotnet format --verify-no-changes` to enforce code style
    - [ ] Add step for `dotnet build --no-restore`
    - [ ] Add step for `dotnet test --no-build` to verify functionality

- [ ] Task: Add Artifact Generation to Backend CI
    - [ ] Add step to publish the API project (`dotnet publish`)
    - [ ] Add step to upload the published project as a GitHub Actions artifact

- [ ] Task: Conductor - User Manual Verification 'Phase 2: Backend CI Pipeline' (Protocol in workflow.md)

## Phase 3: MAUI App CI Pipeline

- [ ] Task: Create GitHub Actions workflow for MAUI App
    - [ ] Create `.github/workflows/maui-app-ci.yml`
    - [ ] Define triggers for push and pull requests on the main branch
    - [ ] Implement steps for: Setup .NET SDK with MAUI workloads (using `maui-workload-action`)

- [ ] Task: Add Build and Test steps to MAUI App CI
    - [ ] Add step for `dotnet build` for targeted platforms (e.g., Windows, Android)
    - [ ] Add step to run unit tests for shared libraries

- [ ] Task: Add Artifact Generation to MAUI App CI
    - [ ] Add step to upload the generated app packages as GitHub Actions artifacts

- [ ] Task: Conductor - User Manual Verification 'Phase 3: MAUI App CI Pipeline' (Protocol in workflow.md)
