namespace HongguoStarter.Models;

public sealed record DramaEpisode(
    string Vid,
    int Index,
    string Title,
    int DurationSec,
    long Comments,
    long Likes,
    string Cover,
    bool Locked
);
