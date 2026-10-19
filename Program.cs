using wt_lab4_4mvc_popko.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Render передаёт приложению номер порта через PORT. Локально используется 5108.
var port = Environment.GetEnvironmentVariable("PORT") ?? "5108";
app.Run($"http://0.0.0.0:{port}");
