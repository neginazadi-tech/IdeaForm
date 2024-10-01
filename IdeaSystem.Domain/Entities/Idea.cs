using System.ComponentModel.DataAnnotations;

namespace IdeaSystem.Domain.Entities;

public class Idea
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(500)]
    public string Message { get; set; }

    [Required]
    public string Category { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime SubmissionDate { get; set; }
}
