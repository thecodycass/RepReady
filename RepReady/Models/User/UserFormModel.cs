using System.ComponentModel.DataAnnotations;

namespace RepReady.Models.User;

public class UserFormModel
{
    [Required(ErrorMessage = "'First name' is required")]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "'Last name' is required")]
    public string LastName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "'Email' is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "'Created at' is required")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Required(ErrorMessage = "'Updated at' is required")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [Required(ErrorMessage = "'Role' is required")]
    public string Role { get; set; } = "user";
    
    public float BodyWeight { get; set; } = 0.0F;
}