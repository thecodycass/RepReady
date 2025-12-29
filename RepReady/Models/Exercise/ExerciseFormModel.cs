using System.ComponentModel.DataAnnotations;

namespace RepReady.Models.Exercise;

public class ExerciseFormModel
{
    [Required(ErrorMessage = "'Name' is required")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "'Category' is required")]
    public string Category { get; set; } = string.Empty;
    [Required(ErrorMessage = "'Body Region' is required")]
    public string BodyRegion { get; set; } = string.Empty;
    [Required(ErrorMessage = "'Created at' is required")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Required(ErrorMessage = "'Updated at' is required")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}