using HongguoStarter.Models;
using HongguoStarter.Services;
using System.Diagnostics;
using System.Text;

namespace HongguoStarter;

public sealed class MainForm : Form
{
    private readonly TextBox _url = new() { PlaceholderText = "Tempel URL detail drama atau masukkan ID drama", Dock = DockStyle.Fill };
    private readonly Label _title = new() { Text = "Belum ada drama dipilih", AutoSize = true, Font = new Font("Segoe UI", 13, FontStyle.Bold) };
    private readonly Label _info = new() { Text = "Masukkan URL/ID, lalu klik Analisis Drama. Saat ini mode demo.", AutoSize = true };
    private readonly DataGridView _episodes = new() { Dock = DockStyle.Fill, AllowUserToAddRows = false, AutoGenerateColumns = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
    private readonly DataGridView _queue = new() { Dock = DockStyle.Fill, AllowUserToAddRows = false, AutoGenerateColumns = true, ReadOnly = true };
    private readonly List<DownloadTask> _tasks;
    private readonly HistoryStore _history = new();
    private DramaDetail? _current;

    public MainForm()
    {
        Text = "Hongguo Drama Catalog — v0.2";
        Width = 1050; Height = 720; MinimumSize = new Size(850, 600);
        StartPosition = FormStartPosition.CenterScreen;
        _tasks = _history.Load();
        BuildUi();
        RefreshQueue();
    }

    private void BuildUi()
    {
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12), RowCount = 5, ColumnCount = 1 };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 76));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 58));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 42));
        Controls.Add(root);

        var header = new Label { Text = "HONGGUO DRAMA CATALOG  |  VERSI 0.2", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 16, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft };
        root.Controls.Add(header, 0, 0);

        var search = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3 };
        search.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        search.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
        search.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
        search.Controls.Add(_url, 0, 0);
        var analyze = new Button { Text = "Muat Katalog", Dock = DockStyle.Fill };
        analyze.Click += (_, _) => Analyze();
        search.Controls.Add(analyze, 1, 0);
        var openPage = new Button { Text = "Buka Halaman", Dock = DockStyle.Fill };
        openPage.Click += (_, _) => OpenDramaPage();
        search.Controls.Add(openPage, 2, 0);
        root.Controls.Add(search, 0, 1);

        var details = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown };
        details.Controls.Add(_title); details.Controls.Add(_info);
        root.Controls.Add(details, 0, 2);

        ConfigureEpisodeGrid();
        var episodePanel = new GroupBox { Text = "Daftar Episode", Dock = DockStyle.Fill, Padding = new Padding(8) };
        episodePanel.Controls.Add(_episodes);
        root.Controls.Add(episodePanel, 0, 3);

        var bottom = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
        bottom.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
        bottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill };
        var all = new Button { Text = "Pilih Semua" }; all.Click += (_, _) => SetAll(true);
        var none = new Button { Text = "Hapus Pilihan" }; none.Click += (_, _) => SetAll(false);
        var add = new Button { Text = "Masukkan ke Antrean" }; add.Click += (_, _) => AddToQueue();
        var export = new Button { Text = "Ekspor CSV" }; export.Click += (_, _) => ExportCsv();
        actions.Controls.AddRange([all, none, add, export]);
        bottom.Controls.Add(actions, 0, 0);
        var queueGroup = new GroupBox { Text = "Antrean / Riwayat Lokal", Dock = DockStyle.Fill, Padding = new Padding(8) };
        queueGroup.Controls.Add(_queue);
        bottom.Controls.Add(queueGroup, 0, 1);
        root.Controls.Add(bottom, 0, 4);
    }

    private void ConfigureEpisodeGrid()
    {
        _episodes.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Pilih", Name = "Selected", Width = 55 });
        _episodes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Episode", DataPropertyName = "Index", Width = 85 });
        _episodes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Judul", DataPropertyName = "Title", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        _episodes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Durasi (detik)", DataPropertyName = "DurationSec", Width = 110 });
        _episodes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Status", DataPropertyName = "Locked", Width = 90 });
    }

    private void Analyze()
    {
        if (string.IsNullOrWhiteSpace(_url.Text))
        {
            MessageBox.Show("Masukkan URL atau ID drama terlebih dahulu.");
            return;
        }
        _current = new DemoDramaProvider().LoadDemo(_url.Text);
        _title.Text = _current.Title;
        _info.Text = $"ID: {_current.Id} | {_current.EpisodeCount} episode | KATALOG LOKAL — metadata dari screenshot; tidak mengambil video";
        _episodes.Rows.Clear();
        foreach (var ep in _current.Episodes)
        {
            var row = _episodes.Rows.Add(false, ep.Index, ep.Title, ep.DurationSec, ep.Locked ? "Terbatas" : "Demo");
            _episodes.Rows[row].Tag = ep;
        }
    }

    private void SetAll(bool value)
    {
        foreach (DataGridViewRow row in _episodes.Rows) row.Cells["Selected"].Value = value;
    }

    private void AddToQueue()
    {
        if (_current is null) { MessageBox.Show("Muat katalog terlebih dahulu."); return; }
        var selected = _episodes.Rows.Cast<DataGridViewRow>()
            .Where(r => Convert.ToBoolean(r.Cells["Selected"].Value ?? false))
            .Select(r => (DramaEpisode)r.Tag!)
            .Select(e => e.Index).ToList();
        if (selected.Count == 0) { MessageBox.Show("Pilih minimal satu episode."); return; }

        _tasks.Add(new DownloadTask {
            SeriesId = _current.Id, Title = _current.Title,
            Episodes = selected, Status = "waiting"
        });
        _history.Save(_tasks);
        RefreshQueue();
        MessageBox.Show($"{selected.Count} episode dimasukkan ke antrean lokal. Antrean ini hanya mengelola daftar episode, belum mengunduh video.");
    }

    private void OpenDramaPage()
    {
        var input = _url.Text.Trim();
        if (string.IsNullOrWhiteSpace(input)) { MessageBox.Show("Masukkan URL halaman drama terlebih dahulu."); return; }
        var url = input.StartsWith("http", StringComparison.OrdinalIgnoreCase)
            ? input
            : $"https://hongguoduanju.com/detail?series_id={Uri.EscapeDataString(input)}";
        try { Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); }
        catch (Exception ex) { MessageBox.Show("Tidak dapat membuka browser: " + ex.Message); }
    }

    private void ExportCsv()
    {
        if (_current is null) { MessageBox.Show("Muat katalog terlebih dahulu."); return; }
        using var dialog = new SaveFileDialog { Filter = "CSV (*.csv)|*.csv", FileName = "daftar_episode.csv" };
        if (dialog.ShowDialog() != DialogResult.OK) return;
        static string Q(string value) => "\"" + value.Replace("\"", "\"\"") + "\"";
        var lines = new List<string> { "DramaId,JudulDrama,Episode,JudulEpisode" };
        lines.AddRange(_current.Episodes.Select(e => string.Join(",", Q(_current.Id), Q(_current.Title), e.Index, Q(e.Title))));
        File.WriteAllText(dialog.FileName, "\uFEFF" + string.Join(Environment.NewLine, lines), Encoding.UTF8);
        MessageBox.Show("Daftar episode berhasil diekspor ke CSV.");
    }

    private void RefreshQueue()
    {
        _queue.DataSource = null;
        _queue.DataSource = _tasks.Select(t => new {
            t.Title, Episode = string.Join(", ", t.Episodes),
            t.Quality, t.Status, Waktu = t.CreatedAt.ToString("yyyy-MM-dd HH:mm")
        }).ToList();
    }
}
