using HongguoStarter.Models;

namespace HongguoStarter.Services;

// Provider demo: tidak melakukan request ke Hongguo atau mengambil URL media.
public sealed class DemoDramaProvider
{
    public DramaDetail LoadDemo(string input)
    {
        var id = input.Trim();
        if (id.Contains("series_id="))
        {
            var part = id.Split("series_id=").Last();
            id = part.Split('&', '/', '?').FirstOrDefault() ?? id;
        }

        var episodes = Enumerable.Range(1, 40)
            .Select(i => new DramaEpisode($"demo-{i}", i, $"Episode {i}",
                120, 0, 0, "", false)).ToList();

        return new DramaDetail
        {
            Id = string.IsNullOrWhiteSpace(id) ? "DEMO-001" : id,
            Title = "Contoh Drama — Data Demo",
            Intro = "Data contoh untuk menguji antarmuka. Belum diambil dari server Hongguo.",
            EpisodeCount = episodes.Count,
            Complete = true,
            EpisodeText = "40 Episode",
            Tags = ["Drama", "Demo"],
            Episodes = episodes
        };
    }
}
