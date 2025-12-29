using RepReady.Models.WorkoutTemplate;
namespace RepReady.DTOs;
/**
 * Data Transfer Object for WorkoutTemplate entity
 * Used for API responses to avoid serialization issues with Supabase attributes
 */
public class WorkoutTemplateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Related Objects will be populated automatically
    public UserDto? User { get; set; }
}

// Extension methods for WorkoutTemplate model mapping
public static class WorkoutTemplateExtensions
{
    // Converts a WorkoutTemplate entity to a WorkoutTemplateDto
    public static WorkoutTemplateDto ToDto(this WorkoutTemplate template)
    {
        return new WorkoutTemplateDto
        {
            Id = template.Id,
            Name = template.Name,
            Description = template.Description,
            UserId = template.UserId,
            CreatedAt = template.CreatedAt,
            UpdatedAt = template.UpdatedAt
        };
    }
    
    // Converts a list of WorkoutTemplate entities to a list of WorkoutTemplateDto objects
    public static IEnumerable<WorkoutTemplateDto> ToDto(this IEnumerable<WorkoutTemplate> templates)
    {
        return templates.Select(t => t.ToDto()).ToList();
    }
}