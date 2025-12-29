using System.ComponentModel.DataAnnotations.Schema;

namespace RepReady.Models.WorkoutTemplate;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("workout_templates")]
public class WorkoutTemplate : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    [Column("description")]
    public string Description { get; set; } = string.Empty;
    [Column("user_id")]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}