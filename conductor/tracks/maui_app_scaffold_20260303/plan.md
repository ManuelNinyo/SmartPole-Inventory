# Implementation Plan: .NET MAUI Mobile Application Scaffolding

## Phase 1: Solution and Infrastructure Setup [checkpoint: 48d9dcc]

- [x] Task: Initialize .NET MAUI Solution [b936eaf]
    - [x] Create a new .NET MAUI App project (`SmartPole.Inventory.App`)
    - [x] Create a Class Library project for shared logic (`SmartPole.Inventory.MobileCore`)
    - [x] Add both projects to a new solution (`SmartPole.Inventory.Mobile.sln`)
    - [x] Set up project references (App -> MobileCore)

- [x] Task: Integrate MVVM and Local Storage Packages [b936eaf]
    - [x] Install `CommunityToolkit.Mvvm` in `MobileCore` and `App`
    - [x] Install `sqlite-net-pcl` in `MobileCore`
    - [x] Initialize Toolkit configuration in `MauiProgram.cs`

- [x] Task: Conductor - User Manual Verification 'Phase 1: Solution and Infrastructure Setup' (Protocol in workflow.md) [48d9dcc]

## Phase 2: Domain and Local Persistence Layer

- [ ] Task: Implement Local Models and State Enum
    - [ ] Write unit tests for local models and enum serialization
    - [ ] Define `SyncStatus` enum (`New`, `Pending`, `Synced`, `Error`)
    - [ ] Create simplified local models for `Poste`, `Inspeccion`, and `Fraude` with SQLite attributes

- [ ] Task: Develop `LocalDbService`
    - [ ] Write unit tests for database initialization and CRUD operations (using a test SQLite path)
    - [ ] Implement `ILocalDbService` and `LocalDbService`
    - [ ] Implement `InitAsync()` to create tables if they don't exist
    - [ ] Implement `SaveInspectionAsync`, `GetPendingInspectionsAsync`, and basic CRUD for poles

- [ ] Task: Conductor - User Manual Verification 'Phase 2: Domain and Local Persistence Layer' (Protocol in workflow.md)

## Phase 3: MVVM Scaffolding and DI Registration

- [ ] Task: Scaffold Initial ViewModels
    - [ ] Write unit tests for `BaseViewModel` logic (e.g., IsBusy status)
    - [ ] Implement `BaseViewModel` (inheriting from `ObservableObject`)
    - [ ] Implement `MainViewModel` and `InspectionsViewModel` skeletons
    - [ ] Write basic unit tests for command execution in ViewModels

- [ ] Task: Configure Dependency Injection
    - [ ] Register `ILocalDbService` as a singleton in `MauiProgram.cs`
    - [ ] Register all ViewModels and Views in `MauiProgram.cs`
    - [ ] Verify DI resolution via a simple start-up test or manual check

- [ ] Task: Conductor - User Manual Verification 'Phase 3: MVVM Scaffolding and DI Registration' (Protocol in workflow.md)
