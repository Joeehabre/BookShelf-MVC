using System.Diagnostics;
using BookShelf_MVC.Data;
using BookShelf_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShelf_MVC.Controllers;

public class HomeController : Controller
{
    private readonly BookShelfContext _db;

    public HomeController(BookShelfContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var books = await _db.Books.ToListAsync();
        var rated = books.Where(b => b.Rating > 0).ToList();

        var model = new DashboardViewModel
        {
            TotalBooks      = books.Count,
            FinishedCount   = books.Count(b => b.Status == ReadingStatus.Finished),
            ReadingCount    = books.Count(b => b.Status == ReadingStatus.Reading),
            WantToReadCount = books.Count(b => b.Status == ReadingStatus.WantToRead),
            PagesRead       = books.Where(b => b.Status == ReadingStatus.Finished).Sum(b => b.Pages),
            AverageRating   = rated.Count > 0 ? Math.Round(rated.Average(b => b.Rating), 1) : 0,
            RecentBooks     = books.OrderByDescending(b => b.DateAdded).Take(5).ToList()
        };

        return View(model);
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
