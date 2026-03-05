# Implementation Plan: MAUI - Offline Geolocation and Maps Module

This plan outlines the steps for integrating offline maps and geolocation services into the SmartPole-Inventory mobile application using .NET MAUI and Mapsui.

## Phase 1: Setup and Infrastructure (Mapsui & Native Permissions) [checkpoint: ba5bccd]
- [x] Task: Install `Mapsui.Maui` NuGet package in the `SmartPole.Inventory.App` project. 7554fb7
- [x] Task: Configure native location permissions in `AndroidManifest.xml` (Android) and `Info.plist` (iOS). c314e67
- [x] Task: Create a `ILocationService` interface in `SmartPole.Inventory.MobileCore` to abstract geolocation operations. 8400d2d
- [x] Task: Implement `LocationService` in `SmartPole.Inventory.App` using MAUI Essentials `Geolocation`. 343c650
- [x] Task: Write unit tests for `LocationService` (Mocking MAUI Geolocation where possible). 07f0c73
- [x] Task: Conductor - User Manual Verification 'Phase 1: Setup and Infrastructure' (Protocol in workflow.md)

## Phase 2: Core Map Logic (Loading MBTiles and Tracking Location) [checkpoint: 549f65a]
- [x] Task: Add a sample `.mbtiles` file to the app's `Resources/Raw` directory (configured as `MauiAsset`). 5e3d378
- [x] Task: Implement a `MapHelper` class in `SmartPole.Inventory.MobileCore` to handle `Mapsui` map object creation and layer setup. e9545fa
- [x] Task: Add logic to `MapHelper` to load and display `.mbtiles` from the bundled assets. 2139cf7
- [x] Task: Implement location tracking logic in the Map ViewModel to update the map view with the user's current position. 583e4d0
- [x] Task: Write tests for `MapHelper` logic (Ensure correct layer types are created). 9468715
- [x] Task: Conductor - User Manual Verification 'Phase 2: Core Map Logic' (Protocol in workflow.md)

## Phase 3: Domain Data Integration (Rendering Pins from SQLite)
- [x] Task: Extend `ILocalDbService` to provide a method for retrieving poles with spatial data (Latitude/Longitude). a787116
- [x] Task: Implement logic in the Map ViewModel to fetch poles from the local database on initialization. 1f2196e
- [ ] Task: Implement a method in `MapHelper` to create a `PointLayer` from a collection of pole coordinates.
- [ ] Task: Write unit tests for the data retrieval and pin generation logic.
- [ ] Task: Conductor - User Manual Verification 'Phase 3: Domain Data Integration' (Protocol in workflow.md)

## Phase 4: Map UI and Interactions (View & ViewModel Implementation)
- [ ] Task: Create `MapViewModel` inheriting from `BaseViewModel` with properties for current location and pole markers.
- [ ] Task: Implement `MapPage.xaml` using the `Mapsui.UI.Maui.MapView` control.
- [ ] Task: Bind the `MapViewModel` to `MapPage.xaml`.
- [ ] Task: Implement "Start Inspection" navigation command in `MapViewModel` triggered by pin selection.
- [ ] Task: Verify that the map correctly handles zooms and pans with the offline background.
- [ ] Task: Conductor - User Manual Verification 'Phase 4: Map UI and Interactions' (Protocol in workflow.md)