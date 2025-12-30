using RepReady.Models.ExerciseTemplate;

namespace RepReady.DTOs;
/**
 * Data Transfer Object for ExerciseTemplate entity
 * Used for API responses to avoid serialization issues with Supabase attributes
 */
public class ExerciseTemplateDto
{
    public int Id { get; set; }
    
    public int SortOrder { get; set; }
    
    public int DefaultSets { get; set; }
    
    public int DefaultReps { get; set; }
    
    public float Weight { get; set; }
    
    public string SetType {  get; set; }
    
    public int WorkoutTemplateId { get; set; }
    
    public int ExerciseId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    // Related Objects will be populated automatically
    public ExerciseDto? Exercise { get; set; }
    
    public WorkoutTemplateDto? WorkoutTemplate { get; set; }
}

// Extension methods for ExerciseTemplate model mapping
public static class ExerciseTemplateExtensions
{
    // Converts a ExerciseTemplate entity to a ExerciseTemplateDto
    public static ExerciseTemplateDto ToDto(this ExerciseTemplate template)
    {
        return new ExerciseTemplateDto
        {
            Id = template.Id,
            SortOrder = template.SortOrder,
            DefaultReps = template.DefaultReps,
            DefaultSets = template.DefaultSets,
            Weight = template.Weight,
            SetType = template.SetType,
            WorkoutTemplateId = template.WorkoutTemplateId,
            ExerciseId = template.ExerciseId,
            CreatedAt = template.CreatedAt,
            UpdatedAt = template.UpdatedAt,
        };
    }

    // Converts a ExerciseTemplateWithDetails entity (includes embedded resources) to a ExerciseTemplateDto
    public static ExerciseTemplateDto ToDto(this ExerciseTemplateWithDetails template)
    {
        return new ExerciseTemplateDto
        {
            Id = template.Id,
            SortOrder = template.SortOrder,
            DefaultReps = template.DefaultReps,
            DefaultSets = template.DefaultSets,
            Weight = template.Weight,
            SetType = template.SetType,
            WorkoutTemplateId = template.WorkoutTemplateId,
            ExerciseId = template.ExerciseId,
            CreatedAt = template.CreatedAt,
            UpdatedAt = template.UpdatedAt,
            Exercise = template.Exercise?.ToDto(),
            WorkoutTemplate = template.WorkoutTemplate?.ToDto(),
        };
    }
    
    // Converts a list of ExerciseTemplate entities to a list of ExerciseTemplateDto objects
    public static IEnumerable<ExerciseTemplateDto> ToDto(this IEnumerable<ExerciseTemplate> templates)
    {
        return templates.Select(t => t.ToDto()).ToList();
    }

    // Converts a list of ExerciseTemplateWithDetails entities to a list of ExerciseTemplateDto objects
    public static IEnumerable<ExerciseTemplateDto> ToDto(this IEnumerable<ExerciseTemplateWithDetails> templates)
    {
        return templates.Select(t => t.ToDto()).ToList();
    }
}
