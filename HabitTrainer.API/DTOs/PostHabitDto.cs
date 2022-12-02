using System.ComponentModel.DataAnnotations;

namespace HabitTrainer.API.DTOs;

public class PostHabitDto
{
    [Required]
    public string Name { get; set; } = "This habit has no name.";

    [Required]
    public string Description { get; set; } = "This habit has no description.";
}
