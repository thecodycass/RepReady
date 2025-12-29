using RepReady.Models.Exercise;
namespace RepReady.DTOs;
/**
 * Data Transfer Object for Exercise entity
 * Used for API responses to avoid serialization issues with Supabase attributes
 */
public class ExerciseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string BodyRegion { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

// Extension methods for Exercise model mapping
public static class ExerciseExtensions
{
    // Converts a Exercise entity to a ExerciseDto
    public static ExerciseDto ToDto(this Exercise exercise)
    {
        return new ExerciseDto
        {
            Id = exercise.Id,
            Name = exercise.Name,
            Category = exercise.Category,
            BodyRegion = exercise.BodyRegion,
            CreatedAt = exercise.CreatedAt,
            UpdatedAt = exercise.UpdatedAt
        };
    }
    
    // Converts a list of Exercise entities to a list of ExerciseDto objects
    public static IEnumerable<ExerciseDto> ToDto(this IEnumerable<Exercise> exercises)
    {
        return exercises.Select(e => e.ToDto()).ToList();
    }
}