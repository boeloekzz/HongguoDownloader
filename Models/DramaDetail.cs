namespace HongguoStarter.Models;

public sealed class DramaDetail
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Intro { get; set; } = "";
    public string Cover { get; set; } = "";
    public int EpisodeCount { get; set; }
    public bool Complete { get; set; }
    public string EpisodeText { get; set; } = "";
    public long HotScore { get; set; }
    public long Followers { get; set; }
    public long PlayCount { get; set; }
    public long Likes { get; set; }
    public long DurationSec { get; set; }
    public List<string> Tags { get; set; } = [];
    public string Badge { get; set; } = "";
    public bool MultiSeason { get; set; }
    public List<DramaEpisode> Episodes { get; set; } = [];
}
