using System.ComponentModel.DataAnnotations;

namespace BookShelf_MVC.Models;

public enum ReadingStatus
{
    [Display(Name = "Want to Read")] WantToRead,
    [Display(Name = "Reading")]      Reading,
    [Display(Name = "Finished")]     Finished
}

public class Book
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(120)]
    public string Author { get; set; } = string.Empty;

    [StringLength(60)]
    public string Genre { get; set; } = string.Empty;

    [Range(1, 10000, ErrorMessage = "Pages must be between 1 and 10,000.")]
    public int Pages { get; set; }

    [Display(Name = "Status")]
    public ReadingStatus Status { get; set; } = ReadingStatus.WantToRead;

    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
    public int Rating { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Date Added")]
    public DateTime DateAdded { get; set; } = DateTime.Today;

    [StringLength(1000)]
    public string? Notes { get; set; }
}
