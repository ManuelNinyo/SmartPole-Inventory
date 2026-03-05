# Track: MAUI - Offline Geolocation and Maps Module

## Overview
This track focuses on implementing native geolocation services and offline map visualization for the SmartPole-Inventory mobile application using .NET MAUI and Mapsui. The goal is to allow field technicians to see their current location and assigned smart poles on a map, even in areas without internet connectivity (rural zones).

## Functional Requirements
- **Native Permissions:** Implement a robust permission request and handling system for foreground location on Android and iOS.
- **Current Location:** Utilize `Geolocation.GetLocationAsync` with high accuracy (GPS) to obtain and update the user's position on the map.
- **Offline Map Visualization:**
    - Use `Mapsui.Maui` to render maps.
    - Support loading and displaying `.mbtiles` vectorial map files bundled within the application package.
- **Smart Pole Rendering:**
    - Read assigned smart poles from the local SQLite database.
    - Render poles as "Pins" on the map based on their latitude and longitude.
- **Interactions:**
    - User can tap on a smart pole "Pin" to trigger the "Start Inspection" action (navigates to the inspection form).
    - Map should center on the user's current location when requested.

## Non-Functional Requirements
- **Offline Capability:** The map must function entirely without an internet connection once the app is installed.
- **Performance:** Rendering the map and pins should be smooth and responsive, especially when panning or zooming.
- **Accuracy:** Location must prioritize GPS-level precision for accurate pole identification.

## Acceptance Criteria
- [ ] App requests and successfully obtains location permissions on both Android and iOS.
- [ ] User's current location is displayed as a distinct marker on the map.
- [ ] Offline map tiles (.mbtiles) are correctly loaded and displayed as the map background.
- [ ] Pins representing poles from the local database are visible at their correct geographic coordinates.
- [ ] Tapping a Pin successfully navigates the user to the corresponding inspection page.

## Out of Scope
- Offline map data downloads (out of scope for this track, data is bundled).
- Turn-by-turn navigation (only visual representation and "Start Inspection" are required).
- Background location tracking (foreground only).