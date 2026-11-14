# AI_RULES — Collect the Coins

| Atribut | Nilai |
|---|---|
| Versi dokumen | 2.0 |
| Otoritas | **Menentukan apa yang BOLEH dan DILARANG dilakukan AI** |

## 1. Hierarki Dokumen
| Dokumen | Kewenangan |
|---|---|
| `PRD.md` | Menentukan apa yang harus dibuat |
| `ARCHITECTURE.md` | Menentukan bagaimana sistem dibuat |
| `TODO.md` | Menentukan pekerjaan yang boleh dilakukan |
| `AI_RULES.md` | Menentukan apa yang boleh dan dilarang dilakukan AI |
| `CHANGELOG.md` | Mencatat perubahan yang disetujui |

**Resolusi konflik:** jika dua dokumen bertentangan, AI berhenti, melaporkan konflik, dan menunggu keputusan pemilik. AI tidak memilih sendiri. Urutan acuan umum: PRD, ARCHITECTURE, TODO.

## 2. Aturan Utama
1. AI hanya mengerjakan **satu task TODO** dalam satu waktu, dan hanya task yang diminta pemilik.
2. AI tidak melompat ke tahap berikutnya sebelum tahap sebelumnya berhasil dan disetujui pemilik.
3. AI tidak menambah fitur, sistem, dependensi, package, atau aset di luar PRD, ARCHITECTURE, dan TODO.
4. AI tidak mengimplementasikan NPC, Behavior Tree, atau NavMesh (Tahap 7) sebelum perubahan requirement disetujui.
5. AI tidak mengubah dokumentasi tanpa persetujuan eksplisit pemilik.
6. AI tidak melakukan refactor, rename, atau penghapusan file di luar cakupan task.
7. Jika ada ambiguitas, AI bertanya satu pertanyaan jelas sebelum bekerja, bukan menebak.
8. Semua kode mengikuti `ARCHITECTURE.md` §8 (penamaan, larangan, nilai di Inspector).
9. Setiap task selesai wajib mencantumkan: file yang diubah, cara memverifikasi di Unity, dan FR/NFR yang dipenuhi.

## 3. Penanganan Permintaan di Luar Lingkup
Jika pemilik meminta sesuatu yang tidak ada di PRD (contoh: "Tambahkan sistem inventory dan crafting"), AI wajib menjawab:

> Fitur inventory dan crafting belum termasuk dalam ruang lingkup PRD saat ini. Saya tidak akan mengimplementasikannya sebelum ada persetujuan perubahan requirement dan pembaruan dokumentasi.

AI lalu menawarkan prosedur perubahan pada §4.

## 4. Prosedur Perubahan Requirement
1. Pemilik menyetujui perubahan secara eksplisit.
2. PRD diperbarui terlebih dahulu (FR/NFR baru atau diubah).
3. ARCHITECTURE diperbarui bila desain terdampak.
4. TODO diperbarui dengan task baru.
5. CHANGELOG mencatat perubahan.
6. Baru setelah itu implementasi boleh dimulai.

## 5. Definition of Done (per task)
- Perilaku sesuai kriteria penerimaan FR terkait.
- Tidak ada error baru di Console Unity.
- Tidak ada perubahan di luar cakupan task.
- Status task di TODO diperbarui menjadi `[x]` dan entri CHANGELOG ditambahkan (setelah disetujui pemilik).

Definition of Done untuk Level 1 secara keseluruhan ada di `PRD.md` §9.

## 6. Hal yang Dilarang
- Mengimplementasikan item di `PRD.md` §8 (Out of Scope).
- Mengerjakan beberapa task sekaligus atau melompati tahap.
- Memasang package atau library tanpa persetujuan.
- Mengklaim sesuatu sudah berfungsi tanpa langkah verifikasi.
- Menulis angka gameplay (kecepatan, tinggi lompat, jumlah koin) langsung di logika kode.
- Membuat sistem inti yang mereferensikan NPC/AI (`ARCHITECTURE.md` §12).

## 7. Implementation Control Prompt
Gunakan prompt ini setiap kali meminta AI mengerjakan task. Ganti `<ID_TASK>`.

```
Baca Documentation/PRD.md, ARCHITECTURE.md, TODO.md, dan AI_RULES.md terlebih dahulu.

Kerjakan hanya task <ID_TASK> dari TODO.md.

Sebelum menulis kode:
1. Sebutkan FR/NFR yang dipenuhi task ini.
2. Pastikan seluruh task tahap sebelumnya sudah berstatus selesai.
3. Daftar file yang akan dibuat atau diubah.
4. Laporkan jika ada konflik antar dokumen atau ambiguitas, lalu berhenti dan tanyakan.

Saat bekerja:
- Jangan menambah fitur, package, atau refactor di luar task.
- Ikuti ARCHITECTURE.md (struktur folder, penamaan, event, nilai di Inspector).

Setelah selesai:
- Ringkas perubahan per file.
- Berikan langkah verifikasi (apa yang dicek di Unity).
- Usulkan pembaruan TODO.md dan CHANGELOG.md, jangan terapkan sebelum saya setuju.
- Berhenti. Jangan lanjut ke task berikutnya.
```
