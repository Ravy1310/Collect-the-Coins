# CHANGELOG — Collect the Coins

Mencatat **perubahan yang telah disetujui**. Format mengikuti Keep a Changelog.

## [Unreleased]
### Planned
- Implementasi sesuai `TODO.md` (T-001 s.d. T-055), satu task per waktu.
- Tahap 7 (NPC, NavMesh, Behavior Tree) menunggu MVP selesai dan persetujuan perubahan requirement.

## [0.6.0-stage4] - 2026-11-14
### Fixed
- Koin ter-collect dari jarak jauh: `SphereCollider` radius diperkecil ke 0.6 dan `isTrigger` diset via kode untuk mencegah override. Collect hanya terjadi saat `OnTriggerEnter` dengan objek ber-Tag `Player`.

### Added (T-030, T-032)
- `Assets/Scripts/Collectibles/Coin.cs`: rotasi, `OnTriggerEnter` hanya untuk Tag `Player`, SFX via `AudioSource.PlayClipAtPoint`, nonaktif dan hancur setelah dikumpulkan.
- `Assets/Prefabs/Coins/Coin.prefab`: Cylinder mesh, `SphereCollider` radius 0.6 isTrigger, Layer `Coin`, Tag `Coin`, komponen `Coin.cs`.

### Added (T-033)
- `Assets/Scripts/GameManager/CoinCounter.cs`: singleton, event `OnCoinCountChanged(int,int)` dan `OnAllCoinsCollected`, `RegisterCoin()` dipanggil oleh `Coin`.
- `Assets/Scripts/UI/CoinCounterView.cs`: subscribe event CoinCounter, update TextMeshPro "Coins: n / 10" dan teks Objective.
- `Assets/Scripts/Level/Gate.cs`: diupdate subscribe `OnAllCoinsCollected` via `OnEnable/OnDisable`.

## [0.5.0-stage3] - 2026-11-14
### Added (T-020, T-021, T-022, T-023)
- Material URP untuk semua elemen map: `Mat_Ground`, `Mat_Island`, `Mat_Rock`, `Mat_Tree`, `Mat_Obstacle`, `Mat_Gate`, `Mat_Gate_Locked`.
- `Assets/Scripts/Level/Gate.cs`: logika terkunci/terbuka gerbang; `Open()` menonaktifkan barrier dan mengaktifkan LevelCompleteTrigger; visual material berubah saat terbuka.
- `Assets/Scripts/Level/LevelCompleteTrigger.cs`: trigger zone yang memanggil `GameManager.CompleteLevel()` saat pemain masuk.
- `Assets/Scripts/GameManager/GameManager.cs`: stub GameManager dengan enum `GameState` (Playing/Paused/Completed) dan signature method lengkap; implementasi penuh pada Tahap 5.
- Panduan scene building Level1 (lihat ringkasan di bawah) — semua elemen map dibangun di Unity Editor dengan Unity primitives sesuai layout Area A–D.

## [0.4.0-stage2] - 2026-11-14
### Added (T-010)
- Prefab `Assets/Prefabs/Player/Player.prefab` dengan `CharacterController` (tinggi 2, radius 0.5, center Y=1), Tag `Player`, Layer `Player` (layer 8).
- Model DogKnight (`DogPolyart`) digunakan sebagai visual child — diimport dari Asset Store ke `Assets/DogKnight/`.

### Added (T-011, T-012)
- `Assets/Scripts/Player/PlayerController.cs`: gerak WASD, lari Shift, rotasi mengikuti arah gerakan, lompat Space, gravity, drive Animator parameter `Speed` dan `IsJumping`. Semua nilai gameplay via `[SerializeField]`.
- `Assets/Animations/PlayerAnimator.controller`: Animator Controller dengan parameter `Speed` (float) dan `IsJumping` (bool), state Idle/Walk/Run menggunakan animasi DogKnight.

### Added (T-013)
- `Assets/Scripts/Player/CameraFollow.cs`: kamera third-person via `LateUpdate`, offset distance/height/smoothSpeed di Inspector. `SetTarget()` untuk setup runtime.

### Added (T-014)
- Layer `Player` (8), `Coin` (9), `Ground` (10), `Obstacle` (11) dan Tag `Player`, `Coin` di `ProjectSettings/TagManager.asset`. Collision via `CharacterController`.

## [0.3.0-stage1] - 2026-11-14
### Added (T-001)
- Struktur folder Assets sesuai ARCHITECTURE.md §3: `Animations/`, `Audio/`, `Materials/`, `Models/Characters/`, `Models/Environment/`, `Models/Collectibles/`, `Prefabs/Player/`, `Prefabs/Coins/`, `Prefabs/Environment/`, `Scripts/Player/`, `Scripts/Collectibles/`, `Scripts/GameManager/`, `Scripts/UI/`, `Scripts/Level/`, `UI/`.
- Folder `Documentation/` di root project (di luar Assets/).
- File `.meta` untuk setiap folder baru agar terdaftar di Unity Asset Database.

### Added (T-002)
- Git sudah terinisialisasi dan `.gitignore` standar Unity sudah ada (mencakup `Library/`, `Temp/`, `Logs/`, `obj/`, `Build/`). Tidak ada perubahan diperlukan.

### Added (T-003)
- Scene `Assets/Scenes/MainMenu.unity` (scene kosong dengan Camera dan Directional Light).
- Scene `Assets/Scenes/Level1.unity` (scene kosong dengan Camera dan Directional Light).
- Kedua scene didaftarkan di `ProjectSettings/EditorBuildSettings.asset` (index 0: MainMenu, index 1: Level1).
- `SampleScene.unity` tetap ada tapi dinonaktifkan (enabled: 0) di Build Settings.

## [0.2.0-docs] - 2026-11-14
### Changed
- Seluruh dokumen diselaraskan dengan Game Design Document (3D, Level 1, Coin Island).
- PRD v2.0: 13 FR dan 5 NFR dari GDD; Definition of Done dipetakan ke requirement.
- ARCHITECTURE v2.0: struktur folder mengikuti GDD, komponen dan event baru, bagian kesiapan NPC/Behavior Tree.
- TODO v2.0: task disusun mengikuti 7 tahap roadmap GDD (Tahap 7 sebagai backlog).
- AI_RULES v2.0: aturan urutan tahap, larangan implementasi AI sebelum disetujui.

### Removed (dari draf 0.1.0-docs yang berdasar asumsi keliru)
- Game 2D top-down, timer 60 detik, target 20 dari 25 koin, kondisi kalah, high score, GameConfig ScriptableObject, test otomatis, folder `_Project`.

### Notes
- Asumsi baru A-01..A-06 dan pertanyaan terbuka OQ-01..OQ-05 di PRD menunggu konfirmasi pemilik.
- Belum ada kode implementasi.

## [0.1.0-docs] - 2026-11-14
### Added
- Draf awal lima dokumen berdasarkan asumsi (digantikan oleh 0.2.0-docs).
