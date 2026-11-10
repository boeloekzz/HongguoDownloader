using HongguoStarter.Models;

namespace HongguoStarter.Services;

// Mode katalog manual: hanya menggunakan metadata yang terlihat pada halaman publik.
// Tidak mengambil stream, token, URL media, atau endpoint terlindungi.
public sealed class DemoDramaProvider
{
    public DramaDetail LoadDemo(string input)
    {
        var id = input.Trim();
        if (id.Contains("series_id=", StringComparison.OrdinalIgnoreCase))
        {
            var part = id.Split("series_id=", StringSplitOptions.None).Last();
            id = part.Split('&', '/', '?').FirstOrDefault() ?? id;
        }

        var isScreenshotDrama = id == "7705801403662355518";
        var episodes = Enumerable.Range(1, 40)
            .Select(i => new DramaEpisode($"manual-{i}", i, $"Episode {i}", 0, 0, 0, "", false)).ToList();

        return new DramaDetail
        {
            Id = string.IsNullOrWhiteSpace(id) ? "7705801403662355518" : id,
            Title = isScreenshotDrama ? "Cinta Sepuluh Tahun Sang CEO (judul sementara)" : "Katalog Drama (Data Manual)",
            Intro = isScreenshotDrama
                ? "Judul asli: 一念星沉，钟总暗恋我十年 | Pemeran yang terlihat: 段芷萱, 曹峻柯 | Kategori: romansa, romansa perkotaan, kisah cinta, CEO."
                : "Katalog lokal. Isi metadata berdasarkan informasi yang terlihat pada halaman drama.",
            EpisodeCount = episodes.Count,
            Complete = true,
            EpisodeText = "40 episode terdaftar pada halaman (sesuai screenshot)",
            Tags = ["Romansa", "Romansa perkotaan", "CEO"],
            Episodes = episodes
        };
    }
}
