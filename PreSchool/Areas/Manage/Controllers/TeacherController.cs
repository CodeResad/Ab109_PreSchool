using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PreSchool.DAL;
using PreSchool.Models;
using PreSchool.Utils.Extentions;

namespace PreSchool.Areas.Manage.Controllers;

[Area("Manage")]
public class TeacherController : Controller
{
    AppDbContext _context;
    IWebHostEnvironment _environment;

    public TeacherController(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }
    
    public async Task<IActionResult> Index()
    {
        List<Teacher> teachers = await _context.Teachers.ToListAsync();
        return View(teachers);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Teacher teacher)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        if (!teacher.File.ContentType.Contains("image"))
        {
            ModelState.AddModelError("File", "Duzgun format daxil edin");
            return View();
        }

        if (teacher.File.Length > 2097152)
        {
            ModelState.AddModelError("File", "File is too long");
            return View();
        }

        teacher.ImgUrl =  teacher.File.CreateFile(_environment.WebRootPath, "/Upload/Teacher");
        
        await _context.Teachers.AddAsync(teacher);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        Teacher teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == id);

        if (teacher == null)
        {
            return NotFound();
        }
        
        teacher.ImgUrl.RemoveFile(_environment.WebRootPath, "/Upload/Teacher");
        
        _context.Teachers.Remove(teacher);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }
        Teacher dbTeacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == id);
        if (dbTeacher == null)
        {
            return NotFound();
        }
        return View(dbTeacher);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, Teacher teacher)
    {
        if (id == null)
        {
            return BadRequest();
        }
        Teacher dbTeacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == id);
        if (dbTeacher == null)
        {
            return NotFound();
        }
        
        if (!ModelState.IsValid)
        {
            return View();
        }

        if (teacher.File != null)
        {
            if (!teacher.File.ContentType.Contains("image"))
            {
                ModelState.AddModelError("File", "Duzgun format daxil edin");
                return View();
            }

            if (teacher.File.Length > 2097152)
            {
                ModelState.AddModelError("File", "File is too long");
                return View();
            }
            
            dbTeacher.ImgUrl.RemoveFile(_environment.WebRootPath, "/Upload/Teacher");
            dbTeacher.ImgUrl = teacher.File.CreateFile(_environment.WebRootPath, "Upload/Teacher");

        }
        
        dbTeacher.FullName = teacher.FullName;
        dbTeacher.Designation = teacher.Designation;
        
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}