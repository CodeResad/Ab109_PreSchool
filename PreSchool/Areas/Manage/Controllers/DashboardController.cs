using Microsoft.AspNetCore.Mvc;

namespace PreSchool.Areas.Manage.Controllers;

[Area("Manage")]
public class DashboardController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}