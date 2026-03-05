# Implementation Plan: Authentication, Spatial Queries, and Offline Sync

## Phase 1: JWT Authentication Service [checkpoint: a8df186]

- [x] Task: Configure JWT Authentication in WebAPI [73cef2e]
    - [x] Create unit tests for JWT token generation and validation logic
    - [x] Implement `IJwtService` and its implementation using `Microsoft.AspNetCore.Authentication.JwtBearer`
    - [x] Configure authentication and authorization services in `Program.cs`
    - [x] Add JWT settings to `appsettings.json`

- [x] Task: Implement Login Endpoint [c51fc45]
    - [x] Write integration tests for the `/api/auth/login` endpoint
    - [x] Implement `LoginCommand` and its handler
    - [x] Create `AuthController` with the `Login` action
    - [x] Verify RBAC (Role-Based Access Control) setup for future use

- [x] Task: Conductor - User Manual Verification 'Phase 1: JWT Authentication Service' (Protocol in workflow.md) [a8df186]

## Phase 2: Spatial Query for Smart Poles [checkpoint: 97a3280]

- [x] Task: Implement Spatial Query Logic in Application Layer [8662344]
    - [x] Write unit tests for the spatial filtering logic (NetTopologySuite)
    - [x] Create `GetPolesInZoneQuery` and its handler
    - [x] Implement the spatial filtering logic using EF Core's `.Within()` or similar spatial methods

- [x] Task: Develop `GET /api/postes/zona` Endpoint [8662344]
    - [x] Write integration tests for retrieving poles within a bounding box/polygon
    - [x] Implement the `GetInZone` action in `SmartPoleController`
    - [x] Ensure the response supports both GeoJSON and simplified DTO formats
    - [x] Apply `[Authorize]` attribute to the endpoint

- [x] Task: Conductor - User Manual Verification 'Phase 2: Spatial Query for Smart Poles' (Protocol in workflow.md) [97a3280]

## Phase 3: Offline Synchronization Mechanism [checkpoint: 254c467]

- [x] Task: Implement Sync Logic in Application Layer [6ab4194]
    - [x] Write unit tests for batch processing and "Client Wins" conflict resolution
    - [x] Create `SyncInspectionsCommand` and its handler
    - [x] Implement logic to process a list of inspection DTOs and update the database

- [x] Task: Develop `POST /api/sync` Endpoint [6ab4194]
    - [x] Write integration tests for syncing a batch of inspections
    - [x] Implement the `Sync` action in a new `SyncController` (or existing one)
    - [x] Ensure the endpoint is restricted to the `Technician` role
    - [x] Implement detailed response reporting (success/failure per record)

- [x] Task: Conductor - User Manual Verification 'Phase 3: Offline Synchronization Mechanism' (Protocol in workflow.md) [254c467]
