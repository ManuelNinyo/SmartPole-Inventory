# Track Specification: Authentication, Spatial Queries, and Offline Sync

## Overview
This track implements three critical backend features for the SmartPole-Inventory system: a secure JWT-based authentication service, a spatial query endpoint for geographic pole discovery, and a robust offline synchronization mechanism for field inspections.

## Functional Requirements

### 1. JWT Authentication Service
- **Login Endpoint:** Authenticate users and generate JWT tokens.
- **Token Validation:** Secure WebAPI endpoints using standard JWT bearer schemes.
- **Credential Management:** signing keys managed via configuration (`appsettings.json` for dev).
- **RBAC:** Support Role-Based Access Control (e.g., `Admin`, `Technician`).

### 2. Spatial Query: `GET /api/postes/zona`
- **Geographic Filtering:** Receive a Bounding Box or Polygon and return smart poles within that area.
- **Spatial Logic:** Utilize EF Core spatial extensions (NetTopologySuite) for high-performance PostgreSQL queries.
- **Zone Filtering:** Allow additional filters by status or type within the geographic scope.
- **Dual Format Support:** Return data as both GeoJSON objects and simplified DTOs (Lat/Long).
- **Security:** Access restricted to authenticated users.

### 3. Offline Sync: `POST /api/sync`
- **Batch Processing:** Receive batches of offline inspections in JSON format.
- **Conflict Resolution:** Implement a **Client Wins** strategy (incoming mobile data takes precedence).
- **Data Integrity:** Ensure inspections are correctly linked to SmartPoles and Technicians.
- **Security:** Access restricted to authenticated users with the `Technician` role.

## Non-Functional Requirements
- **Security:** No sensitive data in logs; strict validation of incoming sync batches.
- **Performance:** Spatial queries must be indexed and efficient for large pole datasets.
- **Usability:** Sync endpoint should provide clear feedback on successful vs. failed records in a batch.

## Acceptance Criteria
- [ ] Users can log in and receive a valid JWT.
- [ ] Authenticated requests can retrieve poles within a defined map zone.
- [ ] Poles returned in spatial queries match the requested geographic boundaries and filters.
- [ ] Offline batches are successfully processed, updating the database according to the "Client Wins" rule.
- [ ] Unauthorized requests to protected endpoints return 401/403 errors.

## Out of Scope
- Refresh token rotation logic.
- Advanced spatial analytics (e.g., heatmaps).
- Push notifications for sync completion.
