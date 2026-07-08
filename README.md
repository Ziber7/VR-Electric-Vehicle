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

---

# VR Electric Vehicle (Bahasa Indonesia)

**VR Electric Vehicle** adalah simulasi pelatihan VR untuk diagnostik baterai kendaraan listrik, dibuat oleh **Ziber Art Studio** menggunakan Unity 2022.3 LTS.

Pemain mengikuti prosedur diagnostik langkah demi langkah:

1. **Keselamatan Utama** — Kenakan sarung tangan dan kacamata pengaman
2. **Akses Baterai** — Buka kait kompartemen baterai
3. **Inspeksi Visual** — Periksa casing baterai, terminal, dan cek kebocoran
4. **Koneksi OBD** — Sambungkan pemindai OBD-II ke kendaraan
5. **Pembacaan Data** — Baca dan interpretasikan data diagnostik dari layar OBD
6. **Kuis** — Jawab pertanyaan tentang kondisi baterai untuk menyelesaikan diagnosis

## Platform Target

- **Meta Quest** (Android / IL2CPP)
- **Windows Standalone** (Oculus / OpenXR)

## Teknologi

| Kategori | Teknologi |
|----------|-----------|
| Mesin | Unity 2022.3 LTS |
| Rendering | Universal Render Pipeline (URP) |
| Framework VR | XR Interaction Toolkit + Oculus XR Plugin + OpenXR |
| UI | Unity UI (uGUI) + TextMesh Pro |
| Input | Unity Input System (New) |
| Skrip | C# (IL2CPP di Android, Mono di Standalone) |

## Memulai

1. Buka proyek di **Unity 2022.3 LTS** (atau versi kompatibel)
2. Buka `Assets/Scenes/1 Start Scene.unity` untuk menu utama
3. Buka `Assets/Scenes/2 Game Scene.unity` untuk scene permainan
4. Build melalui **File → Build Settings** untuk platform target

## Struktur Proyek

| Jalur | Konten |
|------|--------|
| `Assets/Scripts/` | Sistem inti (Audio, Menu, Scene, Opsi) |
| `Assets/Script/` | Mekanik permainan (diagnostik, inspeksi, interaksi) |
| `Assets/Scenes/` | File scene dan cadangan |
| `Assets/3D/` | Model 3D dan tekstur |
| `Assets/QuickOutline/` | Sistem outline mesh untuk penyorotan objek |
