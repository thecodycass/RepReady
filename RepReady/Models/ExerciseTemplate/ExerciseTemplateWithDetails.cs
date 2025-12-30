using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace RepReady.Models.ExerciseTemplate;

/// <summary>
/// Read-model for ExerciseTemplate that includes embedded Exercise + WorkoutTemplate objects.
/// This is used for PostgREST resource embedding results (Select with aliases).
/// </summary>
[Supabase.Postgrest.Attributes.Table("exercise_templates")]
public class ExerciseTemplateWithDetails : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("sort_order")]
    public int SortOrder { get; set; }

    [Column("default_sets")]
    public int DefaultSets { get; set; }

    [Column("default_reps")]
    public int DefaultReps { get; set; }
    
    [Column("weight")]
    public float Weight { get; set; }
    
    [Column("set_type")]
    public string SetType { get; set; }

    [Column("workout_template_id")]
    public int WorkoutTemplateId { get; set; }

    [Column("exercise_id")]
    public int ExerciseId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    // Embedded resources (must match aliases in .Select(...))
    [Column("exercise")]
    public Exercise.Exercise? Exercise { get; set; }

    [Column("workout_template")]
    public WorkoutTemplate.WorkoutTemplate? WorkoutTemplate { get; set; }
}
