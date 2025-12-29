using System.ComponentModel.DataAnnotations;
namespace RepReady.Models.WorkoutTemplate;

public class WorkoutTemplateFormModel
{
    [Required(ErrorMessage = "'Name' is required")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "'Description' is required")]
    public string Description { get; set; } = string.Empty;
    [Required(ErrorMessage = "'User Id' is required")]
    public int UserId { get; set; }
    [Required(ErrorMessage = "'Created at' is required")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Required(ErrorMessage = "'Updated at' is required")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}