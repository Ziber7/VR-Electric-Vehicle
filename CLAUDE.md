# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**VR Electric Vehicle** — a VR training/simulation application for electric vehicle battery diagnostics, built by Ziber Art Studio. Users follow a guided multi-step procedure: wear safety gear, open the battery latch, visually inspect battery components (casing, terminals, leakage), connect an OBD scanner, read diagnostic data, and answer quiz questions.

**Target platforms**: Meta Quest (Android) and Windows Standalone (Oculus/OpenXR).

## Tech Stack

- **Engine**: Unity 2022.3 LTS (URP 14.x, template `com.unity.template.urp-blank`)
- **VR**: XR Interaction Toolkit 2.3.2, Oculus XR Plugin 4.0.0, OpenXR 1.7.0
- **Rendering**: Universal Render Pipeline (URP)
- **UI**: Unity UI (uGUI) + TextMesh Pro
- **Input**: Unity Input System (New) — `activeInputHandler: 2`
- **Scripting Backend**: IL2CPP (Android), Mono (Standalone)
- **Third-party**: QuickOutline by Chris Nolet, Oculus Hands animation

## Build & Development

Open the project in Unity 2022.3 LTS (or compatible). There is no CI or CLI build pipeline — builds are made through the Unity Editor (File → Build Settings).

### Scene Build Order

1. `Assets/Scenes/1 Start Scene.unity` — main menu (build index 0, configured in EditorBuildSettings as "Backup 7")
2. `Assets/Scenes/2 Game Scene.unity` — gameplay scene (build index 1, configured as "Backup 6")

> **Note**: Build settings reference `Backup 6.unity` and `Backup 7.unity`. Multiple backup scenes exist in `Assets/Scenes/Backup *.unity`. When switching build scenes, update `ProjectSettings/EditorBuildSettings.asset`.

### Testing

Run tests via Unity Test Runner (Window → General → Test Runner). The project has `com.unity.test-framework` 1.1.33 installed. There are no pre-existing test scripts — tests would need to be written for PlayMode or EditMode.

### Key Scripting Define Symbols

```
USE_INPUT_SYSTEM_POSE_CONTROL
```
Applied to Android, Standalone, and Windows Store Apps targets.

## Architecture

### Scene Flow

```
Start Scene (menu) ──(fade transition)──▶ Game Scene (battery diagnostic)
```

- `SceneTransitionManager` / `FadeScreen` — singleton-based scene switching with fade-to-black using material color animation curves.
- `GameStartMenu` — main menu with Start, Options (volume + turn type), About, and Quit.

### VR Setup

- XR Interaction Toolkit provides locomotion (snap turn and continuous turn), interactables, and UI ray interaction.
- `SetTurnTypeFromPlayerPref` toggles between `ActionBasedSnapTurnProvider` and `ActionBasedContinuousTurnProvider`, persisted via `PlayerPrefs("turn")`.
- `SetOptionFromUI` binds a volume slider (sets `AudioListener.volume`) and the turn type dropdown.
- XRI Examples (in `Assets/Samples/` and `Assets/XRI_Examples/`) provide reference implementations for sockets, physics doors, 3D UI controls (buttons, knobs, sliders, joysticks), hover highlights, and walkthroughs.

### Battery Diagnostic System (Core Gameplay)

`BaterryDiag` (`Assets/Script/BaterryDiag.cs`) is the central controller — a 6-step linear procedure:

| Step | Content |
|------|---------|
| 0 | Put on gloves and safety glasses |
| 1 | Open the battery latch (arrow hints, latch UI) |
| 2 | Visual inspection — classify casing, terminal, leakage conditions via toggle groups |
| 3 | Connect OBD scanner to the vehicle |
| 4 | Read diagnostic data from the OBD screen |
| 5 | Quiz — answer condition questions, see result |

**Task tracking**: `TaskGroup` (serializable class) holds per-step task statuses (`bool[] TaskStatus`) and result images (`Image[] TaskResultImage`). `checkTask()` compares completed tasks against total per step to enable the Next button. `ResultPractice()` compiles all task results onto a clipboard view.

**Supporting scripts** (all in `Assets/Script/`):

| Script | Role |
|--------|------|
| `ToogleGroup` | Manages the three inspection toggle groups (casing, terminal, leakage); validates answers and reports to `BaterryDiag` |
| `ToogleView` | Toggles vehicle exterior visibility: Full, Half, No Exterior |
| `ColliderManager` | Trigger-based interaction handler — detects when objects (e.g., hands) enter trigger zones to mark tasks done; resets objects to original position; communicates with `BaterryDiag` via `TaskScenario`/`IndexTaskGroup`/`TaskIndex` |
| `ComponentConfig` | Attached to inspectable components — manages Outline highlighting and "look-at-camera" UI hover panels |
| `ToggleHover` | Tooltip display on pointer enter/exit (IPointerEnterHandler/IPointerExitHandler) |
| `slideShowManager` | Generic previous/next slide navigator using sprite arrays |
| `sceneLoader` | Simple scene loading with async support and app quit |
| `switchMaterial` | Placeholder for material switching (currently empty implementation) |
| `CiihuyCurvedMesh` | Procedural mesh generation along Catmull-Rom spline paths (editor + runtime) |

### Audio

`AudioManager` (`Assets/Scripts/AudioManager.cs`) — singleton, `DontDestroyOnLoad`. Serializable `Sound` class holds clip, volume, pitch, loop settings. Plays sounds by string name via `Play(string name)`. `UIAudio` component hooks Unity UI pointer events (`IPointerClickHandler`, `IPointerEnterHandler`, `IPointerExitHandler`) to audio names.

### 3D Assets

- `Assets/Lift Car/` — lift table model (FBX), prefab, textures (PSD), and `Moveliftcar.cs` (keyboard-controlled movement between forward/rear positions)
- `Assets/3D/Sccicor Lift Table/` — scissor lift OBJ model with texture maps
- `Assets/Tables and Chairs/` — furniture models, prefabs, and materials with PBR texture maps (height, metallic, normal, roughness)
- `Assets/QuickOutline/` — Chris Nolet's mesh outline system (shaders + materials + `Outline.cs` component)

## Code Conventions

- Scripts are split across two directories: `Assets/Scripts/` (core: Audio, Menu, Scene, Options) and `Assets/Script/` (gameplay mechanics)
- Serialized fields use a mix of `public` and `[SerializeField] private` — both patterns exist
- Variable naming has some inconsistencies: `BaterryDiag` (misspelling of "BatteryDiag"), `ToogleGroup` (misspelling of "ToggleGroup"), mixed Indonesian/English in comments and debug strings
- Inspector-exposed references are preferred over runtime `Find` calls — most dependencies are wired through the Unity Editor
- No namespaces are used
- Coroutines are used for scene transitions and async loading
