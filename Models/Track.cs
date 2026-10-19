using System.ComponentModel.DataAnnotations;

namespace wt_lab4_4mvc_popko.Models;

/// <summary>
/// Модель музыкального трека, перенесённая из лабораторной работы WT-3.
/// Содержит данные предметной области и правила их проверки.
/// </summary>
public class Track
{
    [Display(Name = "ID")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите название трека.")]
    [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов.")]
    [Display(Name = "Название трека")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите имя исполнителя.")]
    [StringLength(100, ErrorMessage = "Имя исполнителя не должно превышать 100 символов.")]
    [Display(Name = "Исполнитель")]
    public string Artist { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "Название альбома не должно превышать 100 символов.")]
    [Display(Name = "Альбом")]
    public string Album { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите жанр.")]
    [StringLength(50, ErrorMessage = "Жанр не должен превышать 50 символов.")]
    [Display(Name = "Жанр")]
    public string Genre { get; set; } = string.Empty;

    [Range(1900, 2100, ErrorMessage = "Год выпуска должен находиться в диапазоне 1900–2100.")]
    [Display(Name = "Год выпуска")]
    public int Year { get; set; }

    [Range(1, 3600, ErrorMessage = "Длительность должна быть от 1 до 3600 секунд.")]
    [Display(Name = "Длительность, секунд")]
    public int DurationSeconds { get; set; } = 180;

    [Range(typeof(long), "0", "9000000000000", ErrorMessage = "Количество прослушиваний не может быть отрицательным.")]
    [Display(Name = "Количество прослушиваний")]
    public long PlayCount { get; set; }

    [Display(Name = "Доступен для прослушивания")]
    public bool IsAvailable { get; set; } = true;

    [Display(Name = "Длительность")]
    public string DurationDisplay
    {
        get
        {
            var duration = TimeSpan.FromSeconds(DurationSeconds);
            return $"{(int)duration.TotalMinutes:00}:{duration.Seconds:00}";
        }
    }
}
