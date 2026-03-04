# Track Specification: Domain Modeling and Database Persistence Setup

## Overview
This track involves establishing the core domain models for the **SmartPole-Inventory** system and configuring the database persistence layer using Entity Framework Core and PostgreSQL with PostGIS support. It focuses on the primary entities: `SmartPole`, `Inspection`, `FraudFinding`, and `TelcoOperator`.

## Functional Requirements
- **Domain Modeling:**
    - **SmartPole Entity:** Include a `Point` property for geographic location (using NetTopologySuite) with SRID 3857 (Web Mercator).
    - **Inspection Entity:** Represent periodic checks on smart poles. Relationship: One SmartPole to Many Inspections.
    - **FraudFinding Entity:** Capture details of unauthorized use or tampering detected during inspections. Relationship: One Inspection to Many FraudFindings.
    - **TelcoOperator Entity:** Represent the telecommunications companies associated with the infrastructure.
- **Auditing:**
    - Implement `CreatedBy`, `CreatedAt`, `UpdatedBy`, and `UpdatedAt` fields for all entities.
- **Persistence Layer:**
    - Configure `SmartPoleDbContext` to use PostgreSQL and the PostGIS extension.
    - Implement entity mappings using the **Fluent API** for maximum control and separation of concerns.
    - Enable NetTopologySuite support in EF Core for spatial data handling.
- **Database Initialization:**
    - Create and apply the initial EF Core migration to set up the schema in PostgreSQL.

## Non-Functional Requirements
- **Data Integrity:** Ensure foreign key relationships and spatial constraints are correctly defined.
- **Extensibility:** Design the models to allow for additional properties and relationships in future tracks.
- **Testability:** Ensure the `DbContext` can be easily mocked or used with an in-memory database for unit/integration testing.

## Acceptance Criteria
- [ ] Domain models for `SmartPole`, `Inspection`, `FraudFinding`, and `TelcoOperator` are implemented in the Domain layer.
- [ ] Audit fields are present and correctly handled (automatic timestamps).
- [ ] `SmartPoleDbContext` is configured with Fluent API mappings.
- [ ] Geographic `Point` data is successfully stored and retrieved from PostgreSQL.
- [ ] Initial migration is created and applied to the local PostgreSQL database.

## Out of Scope
- Implementation of Repository or Service patterns (to be handled in future tracks).
- API endpoints for these entities.
- Complex spatial queries or analytics.
