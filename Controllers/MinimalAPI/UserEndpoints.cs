using RepReady.Models;
using RepReady.Services;

namespace RepReady.Controllers.MinimalAPI;

/*
 * Users PostgreSQL table in Supabase
 */
public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/v1/users");
        endpoints.MapGet("/", GetAll);
        endpoints.MapGet("/{id}", Get);
        endpoints.MapPost("/", Create);
        endpoints.MapPut("/{id}", Update);
        endpoints.MapDelete("/{id}", Delete);
    }
    
    private static async Task<List<UserDto>> GetAll(SupabaseService db, ILogger<UserEndpoints> logger)
    {
        logger.LogInformation("Retrieving all users");
        try
        {
            var response = await db.Client.From<User>().Get();
            logger.LogInformation("Successfully retrieved {UserCount} users", response.Models.Count);
            return response.Models.ToDto();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving all users");
            throw;
        }
    }

    private static async Task<UserDto?> Get(SupabaseService db, ILogger<UserEndpoints> logger, int id)
    {
        logger.LogInformation("Retrieving user with ID: {UserId}", id);
        try
        {
            var response = await db.Client.From<User>().Where(x => x.Id == id).Get();
            
            if (response.Model == null)
            {
                logger.LogWarning("User with ID {UserId} not found", id);
            }
            else
            {
                logger.LogInformation("Successfully retrieved user with ID: {UserId}", id);
            }
            
            return response.Model?.ToDto();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving user with ID: {UserId}", id);
            throw;
        }
    }

    private static async Task<bool> Create(SupabaseService db, ILogger<UserEndpoints> logger, User user)
    {
        logger.LogInformation("Creating new user");
        try
        {
            await db.Client.From<User>().Insert(user);
            logger.LogInformation("Successfully created user");
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating user");
            return false;
        }
    }
    
    private static async Task<bool> Update(SupabaseService db, ILogger<UserEndpoints> logger, User user)
    {
        logger.LogInformation("Updating user");
        try
        {
            await db.Client.From<User>().Update(user);
            logger.LogInformation("Successfully updated user");
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating user");
            return false;
        }
    }

    private static async Task<bool> Delete(SupabaseService db, ILogger<UserEndpoints> logger, User user)
    {
        logger.LogInformation("Deleting user");
        try
        {
            await db.Client.From<User>().Delete(user);
            logger.LogInformation("Successfully deleted user");
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting user");
            return false;
        }
    }
}
