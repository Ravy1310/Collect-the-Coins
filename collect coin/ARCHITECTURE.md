# ARCHITECTURE — Collect the Coins (MVP Level 1)

| Atribut | Nilai |
|---|---|
| Versi dokumen | 2.0 (Draft untuk review) |
| Otoritas | **Menentukan BAGAIMANA sistem dibuat** |
| Mengacu ke | `PRD.md` v2.0 |

## 1. Prinsip
1. Sederhana: tidak ada sistem yang tidak dibutuhkan FR/NFR di PRD.
2. Logika dipisah dari tampilan (UI hanya menerima event dan meminta aksi).
3. Sistem inti tidak boleh bergantung pada NPC/AI agar AI dapat ditambahkan nanti (NFR-04).
4. Nilai gameplay berupa field yang dapat diatur di Inspector (NFR-03).

## 2. Tech Stack
| Komponen | Pilihan | Status |
|---|---|---|
| Engine | Unity 3D (asumsi Unity 6 LTS) | Konfirmasi di OQ-02 |
| Bahasa | C# | Dikonfirmasi |
| Render | Template 3D bawaan Unity (URP atau Built-in sesuai template) | Asumsi |
| Input | Unity Input System package | Konfirmasi di OQ-03 |
| Karakter | CharacterController (gerak, lompat, gravity) | Asumsi |
| Kamera | Skrip follow sederhana (tanpa package tambahan) | Asumsi |
| UI | Unity UI (uGUI) + TextMeshPro | Asumsi |
| Version control | Git | Lihat §10 |

Package tambahan (misalnya Cinemachine) tidak dipakai kecuali disetujui pemilik.

## 3. Struktur Folder
Mengikuti struktur pada GDD. Dokumentasi berada di luar `Assets/` agar tidak diimpor Unity.

```
CollectTheCoins/
├── Documentation/
└── Assets/
    ├── Animations/
    ├── Audio/
    ├── Materials/
    ├── Models/
    │   ├── Characters/
    │   ├── Environment/
    │   └── Collectibles/
    ├── Prefabs/
    │   ├── Player/
    │   ├── Coins/
    │   └── Environment/
    ├── Scenes/
    │   ├── MainMenu.unity
    │   └── Level1.unity
    ├── Scripts/
    │   ├── Player/
    │   ├── Collectibles/
    │   ├── GameManager/
    │   ├── UI/
    │   └── Level/
    └── UI/
```
Folder `Scripts/AI/` baru dibuat pada Tahap 7 setelah disetujui.

## 4. Scene
| Scene | Isi | FR |
|---|---|---|
| MainMenu | Tampilan menu dengan Start dan Quit | FR-01 |
| Level1 | Pulau, Player, kamera, koin, gerbang, HUD, Pause, Level Completed | FR-02 s.d. FR-13 |

## 5. Komponen dan Tanggung Jawab
| Komponen | Lokasi skrip | Tanggung jawab | FR/NFR |
|---|---|---|---|
| PlayerController | Scripts/Player | Membaca input, jalan/lari, rotasi mengikuti arah, lompat, gravity | FR-02, FR-03, FR-04, FR-05, NFR-03 |
| CameraFollow | Scripts/Player | Kamera mengikuti pemain | FR-06 |
| Environment Level1 (objek scene/prefab, tanpa skrip logika) | Prefabs/Environment | Pulau, Area A–D, rintangan, pohon, batu, collider lingkungan | FR-05, FR-07 |
| Coin | Scripts/Collectibles | Berputar, deteksi trigger pemain, bunyikan SFX, lapor terkumpul, menghilang | FR-08, FR-09 |
| CoinCounter | Scripts/GameManager | Menyimpan jumlah koin, target (Inspector), menyiarkan perubahan dan event "semua terkumpul" | FR-10, FR-11, NFR-03, NFR-04 |
| GameManager | Scripts/GameManager | State level (Playing, Paused, Completed), pause/resume, restart, kembali ke menu | FR-12, FR-13 |
| Gate | Scripts/Level | Status terkunci/terbuka berdasarkan event CoinCounter | FR-11 |
| LevelCompleteTrigger | Scripts/Level | Mendeteksi pemain memasuki area gerbang terbuka dan memberi tahu GameManager | FR-12 |
| CoinCounterView, ObjectiveView | Scripts/UI | Menampilkan "n / 10" dan Objective | FR-10 |
| MainMenuView | Scripts/UI | Tombol Start dan Quit | FR-01 |
| PauseView, LevelCompleteView | Scripts/UI | Tampilan dan tombol Pause serta Level Completed | FR-12, FR-13 |

## 6. State Permainan
`Playing ⇄ Paused` dan `Playing → Completed`.
- Transisi hanya dilakukan oleh GameManager.
- `Completed` bersifat final untuk satu sesi level; Restart memuat ulang scene Level1.
- Saat `Paused` atau `Completed`, input gerak pemain dinonaktifkan.

## 7. Alur Data dan Komunikasi
- Antar-komponen memakai **C# event/Action**: Coin → CoinCounter → (CoinCounterView, Gate). Gate terbuka → LevelCompleteTrigger aktif → GameManager → LevelCompleteView.
- UI tidak mengubah state langsung; UI hanya meminta aksi ke GameManager.
- Singleton tidak dipakai kecuali disetujui dan dicatat di CHANGELOG.

## 8. Aturan Teknis
- Layer/Tag: `Player`, `Coin`, `Ground`, `Obstacle`.
- Penamaan: kelas/metode PascalCase, field privat `_camelCase`, satu kelas per file, nama file = nama kelas.
- Dilarang: `Find*` di dalam `Update`, magic number di logika gameplay, logika game di dalam kelas View.
- Nilai yang dapat diatur lewat `[SerializeField]`: kecepatan jalan/lari, tinggi lompat, gravity, jumlah koin target.
- Aset low-poly; hanya aset yang benar-benar dipakai yang diimpor.

## 9. Strategi Pengujian
Sesuai GDD Tahap 6, pengujian dilakukan **manual** dengan checklist Definition of Done pada `PRD.md` §9, ditambah pengecekan NFR-02 dan NFR-05. Test otomatis tidak dijadwalkan di MVP; jika diinginkan, ditambahkan lewat prosedur perubahan.

## 10. Version Control
Git dengan `.gitignore` standar Unity (`Library/`, `Temp/`, `Logs/`, `obj/`, `Build/`). Satu commit per task TODO dengan ID task di pesan, misalnya `T-032: coin pickup`.

## 11. Pemenuhan Non-Functional Requirements
| NFR | Cara dipenuhi | Verifikasi |
|---|---|---|
| NFR-01 (PC Windows) | Build target Windows 64-bit, tanpa API platform lain. | T-054 |
| NFR-02 (ringan, 60 FPS) | Aset low-poly, jumlah objek aktif kecil, tanpa `Find*` di `Update`. | T-053 |
| NFR-03 (nilai di Inspector) | `[SerializeField]` sesuai §8. | T-011, T-012, T-033 |
| NFR-04 (siap AI) | Lihat §12. | T-033, T-040, T-055 |
| NFR-05 (tanpa bug penghalang) | Aturan §8 dan Definition of Done di `AI_RULES.md` §5. | T-053 |

## 12. Kesiapan NPC dan Behavior Tree (NFR-04)
Tidak ada kode AI pada MVP. Agar Tahap 7 dapat ditambahkan tanpa mengubah mekanisme inti:
- PlayerController, Coin, CoinCounter, dan Gate tidak boleh mereferensikan kelas NPC.
- Posisi pemain dan posisi koin dapat dibaca komponen lain secara read-only (lewat Transform atau properti publik).
- Event koin/gerbang tetap bersifat publik sehingga NPC dapat mengamati tanpa mengubah logika yang ada.
- Layer `Player`, `Coin`, `Ground`, `Obstacle` disiapkan agar deteksi (misalnya line of sight) dan NavMesh dapat memakainya nanti.
- Pemilihan library Behavior Tree dan NavMesh diputuskan pada Tahap 7, bukan sekarang.
