# Track Specification: .NET MAUI Mobile Application Scaffolding

## Overview
This track involves initializing the mobile application for the **SmartPole-Inventory** system using **.NET MAUI**. It establishes the core architecture (MVVM), local persistence (SQLite), and foundational services to enable offline data capture and subsequent synchronization.

## Functional Requirements
- **Solution Initialization:**
    - Initialize a .NET MAUI solution with project separation (e.g., separate projects for the main App and Core/Shared logic).
- **Architecture & MVVM:**
    - Integrate `CommunityToolkit.Mvvm` for clean MVVM implementation.
    - Scaffold `BaseViewModel`, `MainViewModel`, and `InspectionsViewModel`.
    - Configure Dependency Injection (DI) in `MauiProgram.cs`.
- **Local Persistence (SQLite):**
    - Integrate `sqlite-net-pcl` for local storage.
    - Implement a `State Enum` to track synchronization status (e.g., `New`, `Pending`, `Synced`, `Error`).
    - Create local SQLite tables for simplified models of `Poste`, `Inspeccion`, and `Fraude`.
- **Foundational Services:**
    - Develop `LocalDbService` to handle CRUD operations:
        - Saving new inspections.
        - Updating local pole data.
        - Retrieving records pending synchronization.

## Non-Functional Requirements
- **Maintainability:** Clear separation of concerns between UI, Business Logic (ViewModels), and Data Access (Services).
- **Offline Capability:** Ensure the app can function and store data without an active network connection.
- **Robustness:** Proper error handling in database operations.

## Acceptance Criteria
- [ ] A .NET MAUI solution exists with at least two projects (App and Core/Logic).
- [ ] The solution compiles and runs on a mobile emulator (or targeted platform).
- [ ] `LocalDbService` correctly creates the database and required tables.
- [ ] New inspection records can be saved locally and retrieved via code.
- [ ] ViewModels and Services are correctly registered and resolved via the MAUI DI container.

## Out of Scope
- Implementation of the actual Sync logic with the Backend API (to be handled in a future track).
- Detailed UI/UX design (focus is on infrastructure and scaffolding).
- Map integration (to be handled in a future track).
