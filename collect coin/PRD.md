# PRD — Collect the Coins (MVP Level 1)

| Atribut | Nilai |
|---|---|
| Versi dokumen | 2.0 (Draft untuk review, diselaraskan dengan Game Design Document) |
| Engine | Unity (C#) |
| Genre | 3D Casual Adventure, Single Player |
| Platform | PC Windows |
| Otoritas | **Menentukan APA yang harus dibuat** |

## 1. Ringkasan
Collect the Coins adalah game petualangan 3D. Pemain menjelajahi Coin Island, menghindari rintangan, dan mengumpulkan 10 koin emas untuk membuka gerbang keluar. Target MVP adalah **Level 1 yang benar-benar dapat dimainkan dari Main Menu sampai Level Completed**, bukan sekadar demo visual.

Cerita singkat: pemain berada di pulau kecil bernama Coin Island. Gerbang keluar terbuka setelah 10 koin emas terkumpul.

## 2. Tujuan
- Menyelesaikan Level 1 yang dapat dimainkan penuh.
- Menyiapkan struktur sistem agar NPC dengan Behavior Tree dapat ditambahkan nanti tanpa mengubah mekanisme inti (NFR-04).

## 3. Sumber dan Asumsi
Sumber utama: Game Design Document "Collect the Coins – MVP Level 1". Hal yang tidak disebut GDD dan saya asumsikan:

| ID | Asumsi | Status |
|---|---|---|
| A-01 | Kamera third-person yang mengikuti pemain (tersirat dari "kamera mengikuti pemain" dan "rotasi karakter mengikuti arah gerakan"). | Konfirmasi |
| A-02 | Tidak ada timer, nyawa, atau kondisi kalah di MVP; rintangan hanya menghalangi jalur dan harus dilewati dengan lompat. | Konfirmasi (OQ-01) |
| A-03 | Layar Level Completed berisi tombol Restart dan Kembali ke Main Menu. | Konfirmasi |
| A-04 | Menu Pause berisi tombol Resume dan Kembali ke Main Menu. | Konfirmasi |
| A-05 | Main Menu berisi tombol Start dan Quit. | Konfirmasi |
| A-06 | Hanya efek suara koin yang wajib; musik latar tidak termasuk MVP. | Konfirmasi |

## 4. Mekanik dan Kontrol (dari GDD)
| Mekanik | Fungsi |
|---|---|
| Move | Menggerakkan karakter |
| Jump | Melompati rintangan |
| Collect | Mengambil koin |
| Counter | Menghitung koin |
| Gate | Gerbang keluar |
| Win Condition | Menyelesaikan level |

| Tombol | Aksi |
|---|---|
| W / S / A / D | Maju / mundur / kiri / kanan |
| Space | Melompat |
| Shift | Berlari |
| Esc | Pause |

## 5. Functional Requirements
| ID | Requirement | Kriteria penerimaan |
|---|---|---|
| FR-01 | Main Menu dapat memulai game ke Level 1. | Tombol Start memuat scene Level1; Quit menutup aplikasi pada build. |
| FR-02 | Pemain bergerak dengan W/A/S/D dan berlari dengan Shift. | Kecepatan lari lebih tinggi dari jalan; karakter bergerak sesuai input. |
| FR-03 | Rotasi karakter mengikuti arah gerakan. | Karakter menghadap arah ia berjalan. |
| FR-04 | Pemain melompat dengan Space dengan gravity. | Lompat hanya dari tanah; karakter jatuh kembali secara wajar. |
| FR-05 | Karakter berinteraksi dengan lingkungan lewat collision. | Karakter tidak menembus tanah, rintangan, pohon, batu, atau dinding. |
| FR-06 | Kamera mengikuti pemain. | Pemain selalu terlihat saat bergerak dan melompat. |
| FR-07 | Level 1 berupa satu arena kecil di Coin Island dengan Area A (Starting Point), B (Coin Field), C (Obstacle Zone), D (Final Area), dan gerbang keluar. | Keempat area dapat dijelajahi dan dilalui berurutan. |
| FR-08 | Terdapat 10 koin dengan distribusi: Starting Point 0, Coin Field 4, Obstacle Zone 3, Final Area 3. | Jumlah dan lokasi koin sesuai distribusi. |
| FR-09 | Koin berupa model 3D emas, berputar, ber-collider trigger, bersuara saat diambil, dan hilang setelah dikumpulkan. | Satu koin hanya dihitung sekali. |
| FR-10 | UI menampilkan penghitung koin ("n / 10") dan teks Objective. | Angka selalu sama dengan jumlah koin yang telah diambil. |
| FR-11 | Gerbang terkunci sampai 10 koin terkumpul, lalu terbuka. | Gerbang tidak dapat dilewati sebelum koin lengkap. |
| FR-12 | Memasuki area gerbang yang terbuka memunculkan layar Level Completed. | Layar muncul sekali; tersedia aksi sesuai A-03. |
| FR-13 | Esc membuka menu Pause dan menghentikan permainan. | Resume melanjutkan tanpa kehilangan kemajuan; aksi sesuai A-04. |

## 6. Non-Functional Requirements
| ID | Requirement |
|---|---|
| NFR-01 | Target platform PC Windows (build standalone 64-bit). |
| NFR-02 | Aset low-poly yang ringan; permainan berjalan lancar (target 60 FPS) pada PC spesifikasi menengah. |
| NFR-03 | Nilai gameplay (kecepatan jalan/lari, tinggi lompat, jumlah koin target) dapat diatur dari Inspector tanpa mengubah logika kode. |
| NFR-04 | Struktur sistem memungkinkan penambahan NPC/AI (Behavior Tree) tanpa mengubah mekanisme inti. |
| NFR-05 | Tidak ada bug yang menghalangi permainan dan tidak ada error di Console saat bermain. |

## 7. Level 1
| Area | Fungsi | Koin |
|---|---|---|
| A — Starting Point | Spawn dan belajar kontrol | 0 |
| B — Coin Field | Mengumpulkan koin pertama | 4 |
| C — Obstacle Zone | Koin dengan melewati rintangan | 3 |
| D — Final Area | Koin terakhir dan menuju gerbang | 3 |
| **Total** | | **10** |

Tata letak final dibangun langsung di Unity; gambar pada GDD hanya referensi visual.

## 8. Di Luar Ruang Lingkup (Out of Scope) MVP
NPC penjaga, Behavior Tree, NavMesh, dan seluruh pekerjaan Tahap 7 (AI Development) — ditunda sampai MVP selesai dan disetujui lewat prosedur perubahan. Juga: timer, nyawa/kondisi kalah, high score, level lain, inventory, crafting, multiplayer, build non-Windows, gamepad, monetisasi.

> Fitur di luar FR/NFR di atas **tidak boleh** diimplementasikan sebelum PRD diperbarui dan disetujui (lihat `AI_RULES.md`).

## 9. Definition of Done — Level 1
| Checklist GDD | Requirement |
|---|---|
| Game bisa dimulai dari Main Menu | FR-01 |
| Pemain dapat bergerak dan melompat | FR-02, FR-03, FR-04, FR-05 |
| Kamera mengikuti pemain | FR-06 |
| Map Level 1 dapat dijelajahi | FR-07 |
| 10 koin dapat dikumpulkan | FR-08, FR-09 |
| Coin Counter berfungsi | FR-10 |
| Gerbang terbuka setelah semua koin terkumpul | FR-11 |
| Pemain bisa menyelesaikan level | FR-12 |
| Tidak ada bug yang menghalangi permainan | NFR-05 |

Pause (FR-13) adalah bagian sistem UI GDD dan diuji bersama checklist.

## 10. Pertanyaan Terbuka
| ID | Pertanyaan |
|---|---|
| OQ-01 | Apakah ada konsekuensi jatuh dari pulau/arena (misalnya respawn)? Asumsi saat ini: tidak ada kondisi kalah, dan arena dibatasi agar pemain tidak jatuh. |
| OQ-02 | Versi Unity/LTS yang dipakai? (asumsi Unity 6 LTS) |
| OQ-03 | Input System package atau Input Manager lama? |
| OQ-04 | Karakter dan model apa yang dipilih dari Kenney / Poly Pizza / Mixamo / Asset Store? |
| OQ-05 | Apakah A-01 s.d. A-06 sudah sesuai? |
