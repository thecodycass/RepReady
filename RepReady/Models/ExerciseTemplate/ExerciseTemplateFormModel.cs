using System.ComponentModel.DataAnnotations;
namespace RepReady.Models.ExerciseTemplate;

public class ExerciseTemplateFormModel
{
    [Required(ErrorMessage = "'Sort Order' is required")]
    public int SortOrder { get; set; }
    
    [Required(ErrorMessage = "'Default Sets' is required")]
    public int DefaultSets { get; set; }
    
    [Required(ErrorMessage = "'Default Reps' is required")]
    public int DefaultReps { get; set; }
    
    public float Weight { get; set; }
    
    [Required(ErrorMessage = "'Exercise Id' is required")]
    public int ExerciseId { get; set; }
    
    [Required(ErrorMessage = "'Workout Template Name' is required")]
    public int WorkoutTemplateId { get; set; }
    
    [Required(ErrorMessage = "'Created at' is required")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Required(ErrorMessage = "'Updated at' is required")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}