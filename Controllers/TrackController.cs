using Microsoft.AspNetCore.Mvc;
using wt_lab4_4mvc_popko.Models;
using wt_lab4_4mvc_popko.ViewModels;

namespace wt_lab4_4mvc_popko.Controllers;

/// <summary>
/// Обрабатывает HTTP-запросы к музыкальной медиатеке.
/// Данные хранятся в памяти процесса; подключение к базе данных не используется.
/// </summary>
public class TrackController : Controller
{
    private static readonly List<Track> Tracks = new()
    {
        new Track { Id = 1, Title = "Bohemian Rhapsody", Artist = "Queen", Album = "A Night at the Opera", Genre = "Rock", Year = 1975, DurationSeconds = 355, PlayCount = 2_500_000_000, IsAvailable = true },
        new Track { Id = 2, Title = "Blinding Lights", Artist = "The Weeknd", Album = "After Hours", Genre = "Synth-pop", Year = 2019, DurationSeconds = 200, PlayCount = 4_700_000_000, IsAvailable = true },
        new Track { Id = 3, Title = "Believer", Artist = "Imagine Dragons", Album = "Evolve", Genre = "Pop-rock", Year = 2017, DurationSeconds = 204, PlayCount = 2_900_000_000, IsAvailable = true },
        new Track { Id = 4, Title = "Bad Guy", Artist = "Billie Eilish", Album = "When We All Fall Asleep, Where Do We Go?", Genre = "Pop", Year = 2019, DurationSeconds = 194, PlayCount = 2_800_000_000, IsAvailable = false },
        new Track { Id = 5, Title = "Shape of You", Artist = "Ed Sheeran", Album = "÷ (Divide)", Genre = "Pop", Year = 2017, DurationSeconds = 233, PlayCount = 4_000_000_000, IsAvailable = true },
        new Track { Id = 6, Title = "Viva La Vida", Artist = "Coldplay", Album = "Viva la Vida", Genre = "Alternative", Year = 2008, DurationSeconds = 242, PlayCount = 2_000_000_000, IsAvailable = true },
        new Track { Id = 7, Title = "Get Lucky", Artist = "Daft Punk", Album = "Random Access Memories", Genre = "Electronic", Year = 2013, DurationSeconds = 369, PlayCount = 1_500_000_000, IsAvailable = false }
    };

    // GET /Track/Index — каталог с поиском, фильтрацией и сортировкой.
    public IActionResult Index(string? search, string? genre, string? sort = "title")
    {
        IEnumerable<Track> result = Tracks;

        if (!string.IsNullOrWhiteSpace(search))
        {
            result = result.Where(track =>
                track.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                track.Artist.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                track.Album.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(genre))
        {
            result = result.Where(track => track.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
        }

        result = (sort ?? "title").ToLowerInvariant() switch
        {
            "artist" => result.OrderBy(track => track.Artist),
            "popular" => result.OrderByDescending(track => track.PlayCount),
            "year" => result.OrderByDescending(track => track.Year),
            _ => result.OrderBy(track => track.Title)
        };

        var model = new TrackIndexViewModel
        {
            Tracks = result.ToList(),
            SearchText = search ?? string.Empty,
            SelectedGenre = genre ?? string.Empty,
            Sort = sort ?? "title",
            Genres = Tracks.Select(track => track.Genre)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(value => value)
                .ToList()
        };

        return View(model);
    }

    // GET /Track/Details/1 — карточка одного трека.
    public IActionResult Details(int id)
    {
        var track = Tracks.FirstOrDefault(item => item.Id == id);
        return track is null ? NotFound() : View(track);
    }

    // GET /Track/Create — пустая форма добавления.
    [HttpGet]
    public IActionResult Create()
    {
        return View(new Track
        {
            Year = DateTime.Now.Year,
            DurationSeconds = 180,
            IsAvailable = true
        });
    }

    // POST /Track/Create — валидация и добавление в коллекцию в памяти.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Track track)
    {
        if (!ModelState.IsValid)
        {
            return View(track);
        }

        track.Id = Tracks.Count == 0 ? 1 : Tracks.Max(item => item.Id) + 1;
        Tracks.Add(track);
        TempData["SuccessMessage"] = $"Трек «{track.Title}» — {track.Artist} добавлен в медиатеку.";
        return RedirectToAction(nameof(Index));
    }

    // GET /Track/Edit/1 — форма редактирования существующего трека.
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var track = Tracks.FirstOrDefault(item => item.Id == id);
        return track is null ? NotFound() : View(track);
    }

    // POST /Track/Edit/1 — сохранение изменений в коллекции в памяти.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Track updatedTrack)
    {
        var existingTrack = Tracks.FirstOrDefault(item => item.Id == id);
        if (existingTrack is null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            updatedTrack.Id = id;
            return View(updatedTrack);
        }

        existingTrack.Title = updatedTrack.Title;
        existingTrack.Artist = updatedTrack.Artist;
        existingTrack.Album = updatedTrack.Album;
        existingTrack.Genre = updatedTrack.Genre;
        existingTrack.Year = updatedTrack.Year;
        existingTrack.DurationSeconds = updatedTrack.DurationSeconds;
        existingTrack.PlayCount = updatedTrack.PlayCount;
        existingTrack.IsAvailable = updatedTrack.IsAvailable;

        TempData["SuccessMessage"] = $"Изменения трека «{existingTrack.Title}» сохранены.";
        return RedirectToAction(nameof(Index));
    }
}
