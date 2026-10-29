using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRecordMVC.Data;
using StudentRecordMVC.Models;

namespace StudentRecordMVC.Controllers;

public class StudentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public StudentsController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index(string? search)
    {
        var query = _context.Students.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(s =>
                s.Name.Contains(term) ||
                s.Email.Contains(term) ||
                s.Course.Contains(term) ||
                s.EnrollmentNumber.Contains(term));
            ViewData["Search"] = term;
        }

        ViewData["TotalStudents"] = await _context.Students.CountAsync();
        return View(await query.OrderBy(s => s.Name).ToListAsync());
    }

    public IActionResult Create() => View(new Student());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student)
    {
        if (await _context.Students.AnyAsync(s => s.EnrollmentNumber == student.EnrollmentNumber))
            ModelState.AddModelError(nameof(student.EnrollmentNumber), "This enrollment number already exists.");

        if (!ModelState.IsValid) return View(student);

        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Student added successfully.";
        HttpContext.Session.SetString("LastAction", "Created a student record");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var student = await _context.Students.FindAsync(id);
        return student is null ? NotFound() : View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        if (id != student.Id) return BadRequest();

        if (await _context.Students.AnyAsync(s =>
            s.EnrollmentNumber == student.EnrollmentNumber && s.Id != student.Id))
            ModelState.AddModelError(nameof(student.EnrollmentNumber), "This enrollment number already exists.");

        if (!ModelState.IsValid) return View(student);

        try
        {
            _context.Update(student);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Students.AnyAsync(s => s.Id == id)) return NotFound();
            throw;
        }

        TempData["SuccessMessage"] = "Student record updated successfully.";
        HttpContext.Session.SetString("LastAction", "Updated a student record");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        return student is null ? NotFound() : View(student);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        return student is null ? NotFound() : View(student);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student is not null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Student record deleted successfully.";
        }
        return RedirectToAction(nameof(Index));
    }
}
