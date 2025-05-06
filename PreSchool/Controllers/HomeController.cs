using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PreSchool.DAL;
using PreSchool.Models;
using PreSchool.ViewModels.Home;

namespace PreSchool.Controllers;

public class HomeController : Controller
{
    AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        List<Teacher> teachers = await _context.Teachers.Take(3).ToListAsync();
        
        HomeVm model = new HomeVm()
        {
            Teachers = teachers,
        };
        
        return View(model);
    }
}