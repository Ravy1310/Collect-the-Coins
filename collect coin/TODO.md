# TODO — Collect the Coins (MVP Level 1)

| Atribut | Nilai |
|---|---|
| Versi dokumen | 2.0 |
| Otoritas | **Menentukan pekerjaan yang BOLEH dilakukan** |
| Aturan | Satu task dalam satu waktu. Jangan melompat ke tahap berikutnya sebelum tahap sebelumnya berhasil (sesuai roadmap GDD). Status: `[ ]` belum, `[~]` dikerjakan, `[x]` selesai. |

Hanya task di dokumen ini yang boleh dikerjakan. Task baru hanya boleh ditambahkan setelah persetujuan pemilik (lihat `AI_RULES.md`).

## Tahap 1 — Setup Project
- [x] **T-001** Buat project Unity 3D dan struktur folder sesuai `ARCHITECTURE.md` §3. *Refs: ARCH §3, NFR-01*
- [x] **T-002** Inisialisasi Git dan `.gitignore` Unity. *Dep: T-001. Refs: ARCH §10*
- [x] **T-003** Buat scene MainMenu dan Level1, daftarkan di Build Settings. *Dep: T-001. Refs: FR-01*

## Tahap 2 — Membuat Player
- [x] **T-010** Import karakter (low-poly) ke `Models/Characters` dan buat prefab Player. *Dep: T-003. Refs: FR-02*
- [x] **T-011** PlayerController: gerak WASD, lari Shift, rotasi mengikuti arah gerakan. *Dep: T-010. Refs: FR-02, FR-03, NFR-03*
- [x] **T-012** Lompat (Space) dan gravity. *Dep: T-011. Refs: FR-04, NFR-03*
- [x] **T-013** Kamera mengikuti pemain. *Dep: T-011. Refs: FR-06*
- [x] **T-014** Collision dengan lingkungan (uji pada ground sementara). *Dep: T-012. Refs: FR-05*

## Tahap 3 — Membuat Map
- [x] **T-020** Ground/terrain Coin Island dan pembagian Area A–D. *Dep: T-014. Refs: FR-07*
- [x] **T-021** Pohon dan batu sebagai dekorasi/penghalang. *Dep: T-020. Refs: FR-05, FR-07*
- [x] **T-022** Obstacle Zone (rintangan yang dilewati dengan lompat). *Dep: T-020. Refs: FR-07*
- [x] **T-023** Model dan penempatan gerbang di Final Area. *Dep: T-020. Refs: FR-07, FR-11*

## Tahap 4 — Coin System
- [x] **T-030** Import model koin; buat prefab dengan animasi berputar dan collider trigger. *Dep: T-020. Refs: FR-09*
- [ ] **T-031** Tempatkan 10 koin: Coin Field 4, Obstacle Zone 3, Final Area 3. *Dep: T-022, T-030. Refs: FR-08*
- [x] **T-032** Sistem pengambilan koin: SFX dan menghilang setelah diambil. *Dep: T-030. Refs: FR-09*
- [x] **T-033** CoinCounter dengan event dan HUD "n / 10" plus Objective. *Dep: T-032. Refs: FR-10, NFR-03, NFR-04*

## Tahap 5 — Game Logic
- [ ] **T-040** Gate: terkunci sampai 10 koin, lalu terbuka (berbasis event). *Dep: T-023, T-033. Refs: FR-11, NFR-04*
- [ ] **T-041** Trigger gerbang dan layar Level Completed. *Dep: T-040. Refs: FR-12*
- [ ] **T-042** Main Menu: Start dan Quit. *Dep: T-003. Refs: FR-01*
- [ ] **T-043** Menu Pause dengan Esc (Resume, Kembali ke Menu). *Dep: T-041. Refs: FR-13*

## Tahap 6 — Testing Level 1
- [ ] **T-050** Uji gerakan, lompat, kamera, collision. *Dep: T-043. Refs: FR-02, FR-03, FR-04, FR-05, FR-06*
- [ ] **T-051** Uji seluruh 10 koin dapat dijangkau dan counter akurat. *Dep: T-050. Refs: FR-08, FR-09, FR-10*
- [ ] **T-052** Uji gerbang, Level Completed, Pause, dan alur Main Menu. *Dep: T-051. Refs: FR-01, FR-11, FR-12, FR-13*
- [ ] **T-053** Jalankan seluruh checklist Definition of Done (`PRD.md` §9), cek performa dan Console. *Dep: T-052. Refs: NFR-02, NFR-05*
- [ ] **T-054** Build Windows standalone dan uji jalan. *Dep: T-053. Refs: NFR-01*
- [ ] **T-055** Review akhir kesiapan NPC (`ARCHITECTURE.md` §12) dan pembaruan dokumentasi/CHANGELOG. *Dep: T-054. Refs: NFR-04*

## Backlog — belum boleh dikerjakan
- **Tahap 7 — AI Development** (NPC, NavMesh, Behavior Tree, pengujian perilaku NPC). Dimulai hanya setelah MVP selesai dan perubahan requirement disetujui; tidak punya task aktif di dokumen ini.
