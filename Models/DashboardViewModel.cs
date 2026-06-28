namespace BookShelf_MVC.Models;

public class DashboardViewModel
{
    public int TotalBooks    { get; set; }
    public int FinishedCount { get; set; }
    public int ReadingCount  { get; set; }
    public int WantToReadCount { get; set; }
    public int PagesRead     { get; set; }
    public double AverageRating { get; set; }

    public List<Book> RecentBooks { get; set; } = new();
}
