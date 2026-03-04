# Implementation Plan: Domain Modeling and Database Persistence Setup

## Phase 1: Domain Entities Implementation [checkpoint: b3511db]

- [x] Task: Define Base Auditable Entity [2a22672]
    - [x] Create `AuditableEntity<TId>` base class in `Domain.Common`
    - [x] Add `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy` properties
    - [x] Write unit tests for the base auditable entity

- [x] Task: Implement Domain Entities [25c57c6]
    - [x] Create `SmartPole` entity with `Point` location (SRID 3857)
    - [x] Create `Inspection`, `FraudFinding`, and `TelcoOperator` entities
    - [x] Define relationships: Pole (1:N) Inspection (1:N) FraudFinding
    - [x] Write unit tests for each entity ensuring correct property initialization and relationships

- [x] Task: Conductor - User Manual Verification 'Phase 1: Domain Entities Implementation' (Protocol in workflow.md) [b3511db]

## Phase 2: Infrastructure Persistence Configuration

- [x] Task: Configure EF Core with PostgreSQL and NetTopologySuite [43155c5]
    - [x] Add necessary NuGet packages to `Infrastructure` project (`Npgsql.EntityFrameworkCore.PostgreSQL`, `Npgsql.EntityFrameworkCore.PostgreSQL.NetTopologySuite`)
    - [x] Implement `SmartPoleDbContext` inheriting from `DbContext`
    - [x] Register `SmartPoleDbContext` in `DependencyInjection.cs`

- [x] Task: Implement Fluent API Mappings [43155c5]
    - [x] Create `EntityTypeConfiguration` classes for each entity in the `Infrastructure` project
    - [x] Configure table names, keys, and indexes
    - [x] Configure spatial property for `SmartPole.Location` with SRID 3857
    - [x] Write integration tests for `DbContext` ensuring schema validity

- [ ] Task: Conductor - User Manual Verification 'Phase 2: Infrastructure Persistence Configuration' (Protocol in workflow.md)

## Phase 3: Database Initialization and Migration

- [ ] Task: Create and Apply Initial Migration
    - [ ] Create the initial migration using `dotnet ef migrations add InitialCreate`
    - [ ] Apply the migration to the local PostgreSQL database using `dotnet ef database update`

- [ ] Task: Verify Spatial Data Persistence
    - [ ] Write a verification script or integration test to insert and retrieve a `SmartPole` with a `Point`
    - [ ] Confirm SRID 3857 is preserved in the database

- [ ] Task: Conductor - User Manual Verification 'Phase 3: Database Initialization and Migration' (Protocol in workflow.md)
