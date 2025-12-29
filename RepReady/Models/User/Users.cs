namespace RepReady.Models.User;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("users")]
public class User : BaseModel
{
    [PrimaryKey("id")] 
    public int Id { get; set; }
    
    [Column("first_name")] 
    public string FirstName { get; set; } = string.Empty;
    
    [Column("last_name")] 
    public string LastName { get; set; } = string.Empty;
    
    [Column("email")] 
    public string Email { get; set; } = string.Empty;
    
    [Column("created_at")] 
    public DateTime CreatedAt { get; set; }
    
    [Column("updated_at")] 
    public DateTime UpdatedAt { get; set; }
    
    [Column("role")] 
    public string Role { get; set; } = "user";
    
    [Column("body_weight")] 
    public float BodyWeight { get; set; } = 0.0F;
    
}