using wt_lab4_4mvc_popko.Models;

namespace wt_lab4_4mvc_popko.ViewModels;

/// <summary>Данные для каталога: список треков и параметры фильтрации.</summary>
public class TrackIndexViewModel
{
    public IReadOnlyList<Track> Tracks { get; init; } = Array.Empty<Track>();
    public string SearchText { get; init; } = string.Empty;
    public string SelectedGenre { get; init; } = string.Empty;
    public string Sort { get; init; } = "title";
    public IReadOnlyList<string> Genres { get; init; } = Array.Empty<string>();
}
