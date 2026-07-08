# VR Electric Vehicle

**VR Electric Vehicle** is a VR training simulation for electric vehicle battery diagnostics, built by **Ziber Art Studio** using Unity 2022.3 LTS.

Players follow a guided multi-step diagnostic procedure:

1. **Safety First** — Put on gloves and safety glasses
2. **Battery Access** — Open the battery compartment latch
3. **Visual Inspection** — Inspect the battery casing, terminals, and check for leakage
4. **OBD Connection** — Connect the OBD-II scanner to the vehicle
5. **Data Reading** — Read and interpret diagnostic data from the OBD screen
6. **Quiz** — Answer questions about the battery condition to complete the diagnosis

## Target Platforms

- **Meta Quest** (Android / IL2CPP)
- **Windows Standalone** (Oculus / OpenXR)

## Tech Stack

| Category | Technology |
|----------|------------|
| Engine | Unity 2022.3 LTS |
| Rendering | Universal Render Pipeline (URP) |
| VR Framework | XR Interaction Toolkit + Oculus XR Plugin + OpenXR |
| UI | Unity UI (uGUI) + TextMesh Pro |
| Input | Unity Input System (New) |
| Scripting | C# (IL2CPP on Android, Mono on Standalone) |

## Getting Started

1. Open the project in **Unity 2022.3 LTS** (or compatible)
2. Open `Assets/Scenes/1 Start Scene.unity` for the main menu
3. Open `Assets/Scenes/2 Game Scene.unity` for the gameplay scene
4. Build via **File → Build Settings** for your target platform

## Project Structure

| Path | Content |
|------|---------|
| `Assets/Scripts/` | Core systems (Audio, Menu, Scene, Options) |
| `Assets/Script/` | Gameplay mechanics (diagnostics, inspection, interactions) |
| `Assets/Scenes/` | Scene files and backups |
| `Assets/3D/` | 3D models and textures |
| `Assets/QuickOutline/` | Mesh outline system for object highlighting |
