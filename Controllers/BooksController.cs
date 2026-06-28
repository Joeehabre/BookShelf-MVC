using BookShelf_MVC.Data;
using BookShelf_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShelf_MVC.Controllers;

public class BooksController : Controller
{
    private readonly BookShelfContext _db;

    public BooksController(BookShelfContext db) => _db = db;

    // GET: /Books?search=...&status=...
    public async Task<IActionResult> Index(string? search, ReadingStatus? status)
    {
        var query = _db.Books.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(b =>
                b.Title.ToLower().Contains(term) ||
                b.Author.ToLower().Contains(term) ||
                b.Genre.ToLower().Contains(term));
        }

        if (status is not null)
            query = query.Where(b => b.Status == status);

        ViewData["Search"] = search;
        ViewData["Status"] = status;

        var books = await query.OrderByDescending(b => b.DateAdded).ToListAsync();
        return View(books);
    }

    // GET: /Books/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var book = await _db.Books.FindAsync(id);
        return book is null ? NotFound() : View(book);
    }

    // GET: /Books/Create
    public IActionResult Create() => View();

    // POST: /Books/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Author,Genre,Pages,Status,Rating,DateAdded,Notes")] Book book)
    {
        if (!ModelState.IsValid) return View(book);
        _db.Add(book);
        await _db.SaveChangesAsync();
        TempData["Message"] = $"Added \"{book.Title}\".";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Books/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var book = await _db.Books.FindAsync(id);
        return book is null ? NotFound() : View(book);
    }

    // POST: /Books/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Genre,Pages,Status,Rating,DateAdded,Notes")] Book book)
    {
        if (id != book.Id) return NotFound();
        if (!ModelState.IsValid) return View(book);

        try
        {
            _db.Update(book);
            await _db.SaveChangesAsync();
            TempData["Message"] = $"Updated \"{book.Title}\".";
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _db.Books.AnyAsync(b => b.Id == id)) return NotFound();
            throw;
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: /Books/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var book = await _db.Books.FindAsync(id);
        return book is null ? NotFound() : View(book);
    }

    // POST: /Books/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var book = await _db.Books.FindAsync(id);
        if (book is not null)
        {
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            TempData["Message"] = $"Deleted \"{book.Title}\".";
        }
        return RedirectToAction(nameof(Index));
    }
}
