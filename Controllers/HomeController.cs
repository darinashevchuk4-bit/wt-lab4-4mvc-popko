using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using wt_lab4_4mvc_popko.Models;

namespace wt_lab4_4mvc_popko.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();

    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}

public class ErrorViewModel
{
    public string? RequestId { get; init; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
