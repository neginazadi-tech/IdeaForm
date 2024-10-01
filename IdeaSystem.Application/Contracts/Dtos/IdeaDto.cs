using System.ComponentModel.DataAnnotations;

namespace IdeaSystem.Application.Contracts.Dtos;

public class IdeaDto
{
    public int Id { get; set; }


    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; }


    [Required(ErrorMessage = "Email is required.")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }


    [Required(ErrorMessage = "Message is required.")]
    public string Message { get; set; }


    [Required(ErrorMessage = "Category is required.")]
    public string Category { get; set; }

    public DateTime SubmissionDate { get; set; } = DateTime.Now;
}
