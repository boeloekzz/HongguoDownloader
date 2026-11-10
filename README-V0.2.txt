HONGGUO DRAMA CATALOG v0.2

FITUR
- Memuat katalog lokal contoh untuk drama pada screenshot (ID 7705801403662355518).
- Menampilkan daftar episode 1-40.
- Pilih semua / hapus pilihan.
- Menyimpan episode terpilih ke antrean lokal.
- Ekspor daftar episode ke CSV.
- Membuka halaman drama resmi di browser.

CATATAN PENTING
Versi ini adalah katalog dan pengelola daftar episode, bukan pengunduh video aktif.
Metadata contoh dimasukkan berdasarkan screenshot yang diberikan. Aplikasi tidak
mengakses endpoint internal, token, tanda tangan API, URL media, DRM, atau proteksi
akses. Episode yang ditampilkan adalah nomor episode, bukan data stream.

MEMBANGUN EXE
1. Unggah isi folder proyek ini ke repository GitHub yang sama, timpa file lama.
2. Pastikan file .github/workflows/build-windows.yml tetap ada di repository.
3. Buka tab Actions -> Build Windows EXE -> Run workflow (atau push perubahan).
4. Tunggu tanda hijau (Success).
5. Buka hasil workflow dan unduh artifact HongguoDownloader-Windows-x64.
6. Ekstrak ZIP artifact, lalu jalankan HongguoDownloader.exe.

Build menggunakan GitHub Actions Windows + .NET 8 self-contained, sehingga PC
pengguna tidak perlu memasang .NET SDK.
