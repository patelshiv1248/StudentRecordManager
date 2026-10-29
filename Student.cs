using System.ComponentModel.DataAnnotations;

namespace StudentRecordMVC.Models;

public class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter the student's full name.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 80 characters.")]
    [Display(Name = "Full name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(16, 60, ErrorMessage = "Age must be between 16 and 60.")]
    public int Age { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [StringLength(120)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Course must be between 2 and 50 characters.")]
    public string Course { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    [Display(Name = "Enrollment number")]
    public string EnrollmentNumber { get; set; } = string.Empty;
}
