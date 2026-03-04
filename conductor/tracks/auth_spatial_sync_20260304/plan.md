# Implementation Plan: Authentication, Spatial Queries, and Offline Sync

## Phase 1: JWT Authentication Service

- [ ] Task: Configure JWT Authentication in WebAPI
    - [ ] Create unit tests for JWT token generation and validation logic
    - [ ] Implement `IJwtService` and its implementation using `Microsoft.AspNetCore.Authentication.JwtBearer`
    - [ ] Configure authentication and authorization services in `Program.cs`
    - [ ] Add JWT settings to `appsettings.json`

- [ ] Task: Implement Login Endpoint
    - [ ] Write integration tests for the `/api/auth/login` endpoint
    - [ ] Implement `LoginCommand` and its handler
    - [ ] Create `AuthController` with the `Login` action
    - [ ] Verify RBAC (Role-Based Access Control) setup for future use

- [ ] Task: Conductor - User Manual Verification 'Phase 1: JWT Authentication Service' (Protocol in workflow.md)

## Phase 2: Spatial Query for Smart Poles

- [ ] Task: Implement Spatial Query Logic in Application Layer
    - [ ] Write unit tests for the spatial filtering logic (NetTopologySuite)
    - [ ] Create `GetPolesInZoneQuery` and its handler
    - [ ] Implement the spatial filtering logic using EF Core's `.Within()` or similar spatial methods

- [ ] Task: Develop `GET /api/postes/zona` Endpoint
    - [ ] Write integration tests for retrieving poles within a bounding box/polygon
    - [ ] Implement the `GetInZone` action in `SmartPoleController`
    - [ ] Ensure the response supports both GeoJSON and simplified DTO formats
    - [ ] Apply `[Authorize]` attribute to the endpoint

- [ ] Task: Conductor - User Manual Verification 'Phase 2: Spatial Query for Smart Poles' (Protocol in workflow.md)

## Phase 3: Offline Synchronization Mechanism

- [ ] Task: Implement Sync Logic in Application Layer
    - [ ] Write unit tests for batch processing and "Client Wins" conflict resolution
    - [ ] Create `SyncInspectionsCommand` and its handler
    - [ ] Implement logic to process a list of inspection DTOs and update the database

- [ ] Task: Develop `POST /api/sync` Endpoint
    - [ ] Write integration tests for syncing a batch of inspections
    - [ ] Implement the `Sync` action in a new `SyncController` (or existing one)
    - [ ] Ensure the endpoint is restricted to the `Technician` role
    - [ ] Implement detailed response reporting (success/failure per record)

- [ ] Task: Conductor - User Manual Verification 'Phase 3: Offline Synchronization Mechanism' (Protocol in workflow.md)
