# Hongguo C# Starter (Windows)

Kerangka aplikasi Windows Forms untuk mengelola metadata drama, pilihan episode, antrean tugas, dan riwayat lokal.

## Persyaratan
- Windows 10/11 64-bit
- .NET 8 SDK
- Visual Studio 2022 (opsional), workload ".NET desktop development"

## Menjalankan
1. Ekstrak ZIP.
2. Buka `HongguoStarter.sln` dengan Visual Studio 2022, atau buka terminal pada folder ini.
3. Jalankan:
   ```powershell
   dotnet restore
   dotnet run --project HongguoStarter.csproj
   ```

## Yang sudah tersedia
- Antarmuka Windows Forms.
- Kolom URL/ID drama.
- Contoh data demo untuk menguji tampilan episode.
- Pilih semua / hapus pilihan episode.
- Antrean tugas lokal dengan status Waiting.
- Riwayat lokal disimpan ke `%LocalAppData%\\HongguoStarter\\history.json`.

## Batasan penting
Ini adalah kerangka aplikasi dan **belum terhubung ke server Hongguo**. Tombol "Analisis Drama" memuat data demo agar UI dapat diuji. Tidak ada pengambilan URL media, pengunduhan video, penyalinan mekanisme signing, atau bypass lisensi/DRM/paywall. Integrasi berikutnya harus memakai API/sumber yang memang diizinkan dan terdokumentasi.
