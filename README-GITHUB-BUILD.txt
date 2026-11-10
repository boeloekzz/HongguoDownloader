# Hongguo Downloader — Build EXE melalui GitHub Actions

Paket ini membuat **EXE Windows x64 self-contained** melalui GitHub Actions. Komputer Windows Anda tidak perlu memasang Visual Studio atau .NET SDK untuk menjalankan hasil EXE.

## Cara menghasilkan EXE

1. Buat akun atau masuk ke https://github.com
2. Buat repository baru, misalnya `HongguoDownloader`.
3. Upload seluruh isi folder paket ini ke repository (termasuk folder `.github/workflows`).
4. Pastikan branch bernama `main`.
5. Buka tab **Actions** di repository.
6. Pilih workflow **Build Windows EXE**.
7. Klik **Run workflow** jika workflow belum berjalan otomatis.
8. Tunggu sampai proses berstatus hijau / berhasil.
9. Buka hasil workflow, cari bagian **Artifacts**, lalu unduh `HongguoDownloader-Windows-x64`.
10. Ekstrak ZIP artifact dan jalankan `HongguoDownloader.exe`.

## Isi versi ini
- Antarmuka Windows Forms.
- Input URL/ID drama.
- Daftar episode contoh untuk pengujian tampilan.
- Pilih semua / hapus pilihan.
- Antrean tugas lokal.
- Riwayat disimpan di `%LocalAppData%\\HongguoStarter\\history.json`.

## Penting
- Hasil build adalah EXE mandiri (runtime .NET ikut dibundel); tidak perlu memasang .NET di PC untuk menjalankannya.
- Aplikasi saat ini **masih mode DEMO**. Tombol analisis menampilkan 40 episode contoh, bukan data langsung dari server Hongguo.
- Belum ada fungsi pengambilan atau pengunduhan video.
- Integrasi selanjutnya hanya dapat memakai sumber/API yang memang diizinkan. Paket ini tidak membypass lisensi, autentikasi, DRM, paywall, atau proteksi media.
- Nama EXE saat ini `HongguoDownloader.exe`. Bisa diubah menjadi `HongguoDownloader.exe` setelah build dengan mengganti `<AssemblyName>` pada file `.csproj` menjadi `HongguoDownloader`.

## Jika build gagal
Buka tab **Actions**, klik proses yang gagal, lalu kirim screenshot atau salin pesan error terakhir.
