namespace HongguoStarter.Models;

public sealed class DownloadTask
{
    public string SeriesId { get; set; } = "";
    public string Title { get; set; } = "";
    public string Quality { get; set; } = "Sumber resmi";
    public List<int> Episodes { get; set; } = [];
    public string Status { get; set; } = "waiting";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
