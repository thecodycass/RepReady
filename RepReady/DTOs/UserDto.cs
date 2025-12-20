using RepReady.Models;
namespace RepReady.DTOs;
/**
 * Data Transfer Object for User entity
 * Used for API responses to avoid serialization issues with Supabase attributes
 */
public class UserDto
{
    public int Id { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public string Role { get; set; } = "user";
}

// Extension methods for User model mapping
public static class UserExtensions
{
    // Converts a User entity to a UserDto
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            Role = user.Role ?? "user"
        };
    }
    
    // Converts a list of User entities to a list of UserDto objects
    public static List<UserDto> ToDto(this List<User> users)
    {
        return users.Select(u => u.ToDto()).ToList();
    }
}
