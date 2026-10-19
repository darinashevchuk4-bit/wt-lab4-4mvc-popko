# Архитектура MeloWave

```text
Браузер
   │ HTTP GET/POST
   ▼
Routing ASP.NET Core
   │
   ▼
TrackController / HomeController
   │
   ├── Track (Model) — поля трека и правила проверки
   ├── List<Track> — демонстрационные данные в памяти
   └── TrackIndexViewModel — данные каталога и фильтров
   │
   ▼
Razor Views (.cshtml) + Bootstrap
   │ HTML
   ▼
Браузер
```

Для Render приложение запускается внутри Docker-контейнера и слушает `0.0.0.0:$PORT`. База данных не используется.
