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

## Phase 3: Application Layer & CQRS (MediatR)

- [x] Task: Set up MediatR and CQRS Structure [0819a13]
    - [x] Write unit tests for command/query dispatching logic
    - [x] Configure MediatR in the `Application` project
    - [x] Create folders for `Commands`, `Queries`, and `Common` behaviors

- [x] Task: Implement Initial Application Use Cases (Skeleton) [0135231]
    - [x] Write unit tests for a sample `CreateSmartPoleCommand`
    - [x] Implement `CreateSmartPoleCommand` handler in the `Application` layer

- [ ] Task: Conductor - User Manual Verification 'Phase 3: Application Layer & CQRS (MediatR)' (Protocol in workflow.md)

## Phase 4: WebAPI Layer & ODD Configuration

- [ ] Task: Configure Observability (ODD)
    - [ ] Write integration tests for health check and logging configuration
    - [ ] Configure Serilog for structured logging and health checks (`/health`)
    - [ ] Configure basic OpenTelemetry tracing (optional skeleton)

- [ ] Task: Integrate Observability with Tests
    - [ ] Write unit tests for the test logging sink
    - [ ] Configure test projects to use Serilog sink for capturing output during test runs
    - [ ] Implement tracing middleware for integration tests (optional)

- [ ] Task: Implement API Endpoints (Skeleton)
    - [ ] Write integration tests for `SmartPoleController`
    - [ ] Implement `SmartPoleController` in the `WebAPI` layer

- [ ] Task: Conductor - User Manual Verification 'Phase 4: WebAPI Layer & ODD Configuration' (Protocol in workflow.md)
