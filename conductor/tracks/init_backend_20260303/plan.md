# Implementation Plan: Initialize ASP.NET Core Web API with Onion Architecture

## Phase 1: Solution and Layer Initialization [checkpoint: 9c6ec01]

- [x] Task: Create .NET Solution and Layer Projects [484de97]
    - [x] Create `SmartPole.Inventory.sln`
    - [x] Create `SmartPole.Inventory.Domain`, `Application`, `Infrastructure`, and `WebAPI` projects
    - [x] Create `SmartPole.Inventory.UnitTests` and `SmartPole.Inventory.IntegrationTests` projects
    - [x] Add all projects to the solution

- [x] Task: Configure Project Dependencies [484de97]
    - [x] Set `Application` to depend on `Domain`
    - [x] Set `Infrastructure` to depend on `Application`
    - [x] Set `WebAPI` to depend on `Infrastructure` and `Application`
    - [x] Configure Test projects to depend on the layers they verify

- [x] Task: Conductor - User Manual Verification 'Phase 1: Solution and Layer Initialization' (Protocol in workflow.md) [9c6ec01]

## Phase 2: Domain Layer & DDD Foundation [checkpoint: 26bed25]

- [x] Task: Implement Base DDD Abstractions [22d4698]
    - [x] Write unit tests for `Entity`, `ValueObject`, and `DomainEvent`
    - [x] Implement base classes in the `Domain` layer
    - [x] Implement `IAggregateRoot` interface

- [x] Task: Scaffold Initial Domain Entities [64398e8]
    - [x] Write unit tests for `SmartPole`, `MaintenanceRecord`, and `User`
    - [x] Implement `SmartPole`, `MaintenanceRecord`, and `User` entities in the `Domain` layer

- [x] Task: Conductor - User Manual Verification 'Phase 2: Domain Layer & DDD Foundation' (Protocol in workflow.md) [26bed25]

## Phase 3: Application Layer & CQRS (MediatR) [checkpoint: 7c92e0f]

- [x] Task: Set up MediatR and CQRS Structure [0819a13]
    - [x] Write unit tests for command/query dispatching logic
    - [x] Configure MediatR in the `Application` project
    - [x] Create folders for `Commands`, `Queries`, and `Common` behaviors

- [x] Task: Implement Initial Application Use Cases (Skeleton) [0135231]
    - [x] Write unit tests for a sample `CreateSmartPoleCommand`
    - [x] Implement `CreateSmartPoleCommand` handler in the `Application` layer

- [x] Task: Conductor - User Manual Verification 'Phase 3: Application Layer & CQRS (MediatR)' (Protocol in workflow.md) [7c92e0f]

## Phase 4: WebAPI Layer & ODD Configuration [checkpoint: 373225f]

- [x] Task: Configure Observability (ODD) [48662d3]
    - [x] Write integration tests for health check and logging configuration
    - [x] Configure Serilog for structured logging and health checks (`/health`)
    - [x] Configure basic OpenTelemetry tracing (optional skeleton)

- [x] Task: Integrate Observability with Tests [48662d3]
    - [x] Write unit tests for the test logging sink
    - [x] Configure test projects to use Serilog sink for capturing output during test runs
    - [x] Implement tracing middleware for integration tests (optional)

- [x] Task: Implement API Endpoints (Skeleton) [48662d3]
    - [x] Write integration tests for `SmartPoleController`
    - [x] Implement `SmartPoleController` in the `WebAPI` layer

- [x] Task: Conductor - User Manual Verification 'Phase 4: WebAPI Layer & ODD Configuration' (Protocol in workflow.md) [373225f]
