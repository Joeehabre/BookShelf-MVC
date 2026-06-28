using BookShelf_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShelf_MVC.Data;

public class BookShelfContext : DbContext
{
    public BookShelfContext(DbContextOptions<BookShelfContext> options) : base(options) { }

    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed a few books so the app is not empty on first run.
        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Clean Code",                 Author = "Robert C. Martin", Genre = "Programming", Pages = 464, Status = ReadingStatus.Finished,   Rating = 5, DateAdded = new DateTime(2026, 1, 12), Notes = "A must-read on writing maintainable code." },
            new Book { Id = 2, Title = "The Pragmatic Programmer",    Author = "Hunt & Thomas",    Genre = "Programming", Pages = 352, Status = ReadingStatus.Reading,    Rating = 4, DateAdded = new DateTime(2026, 3, 4) },
            new Book { Id = 3, Title = "Introduction to Algorithms",  Author = "CLRS",             Genre = "Computer Science", Pages = 1312, Status = ReadingStatus.WantToRead, Rating = 0, DateAdded = new DateTime(2026, 4, 21) },
            new Book { Id = 4, Title = "Sapiens",                     Author = "Yuval Noah Harari",Genre = "History",     Pages = 443, Status = ReadingStatus.Finished,   Rating = 5, DateAdded = new DateTime(2026, 2, 2),  Notes = "A sweeping look at human history." },
            new Book { Id = 5, Title = "Atomic Habits",               Author = "James Clear",      Genre = "Self-Help",   Pages = 320, Status = ReadingStatus.Reading,    Rating = 4, DateAdded = new DateTime(2026, 5, 9) }
        );
    }
}
