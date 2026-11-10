using System.Text.Json;
using HongguoStarter.Models;

namespace HongguoStarter.Services;

public sealed class HistoryStore
{
    private readonly string _file = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "HongguoStarter", "history.json");

    public List<DownloadTask> Load()
    {
        try
        {
            if (!File.Exists(_file)) return [];
            return JsonSerializer.Deserialize<List<DownloadTask>>(File.ReadAllText(_file)) ?? [];
        }
        catch { return []; }
    }

    public void Save(List<DownloadTask> tasks)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_file)!);
        File.WriteAllText(_file, JsonSerializer.Serialize(tasks,
            new JsonSerializerOptions { WriteIndented = true }));
    }
}
