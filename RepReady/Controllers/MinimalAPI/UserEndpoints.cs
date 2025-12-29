using Microsoft.AspNetCore.Mvc;
using RepReady.Models.User;
using RepReady.DTOs;
using RepReady.Services;

namespace RepReady.Controllers.MinimalAPI;

/*
 * API's for the Users table in Supabase.
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
    
    private static async Task<IEnumerable<UserDto>> GetAll(SupabaseService db)
    {
        var response = await db.Client.From<User>().Get();
        return response.Models.ToDto();
    }

    private static async Task<UserDto?> Get(SupabaseService db, int id)
    {
        var response = await db.Client.From<User>().Where(x => x.Id == id).Get();
        return response.Model?.ToDto();
    }

    private static async Task<IResult> Create(SupabaseService db, [FromBody] User user)
    {
        try
        {
            await db.Client.From<User>().Insert(user);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    
    private static async Task<bool> Update(SupabaseService db, int id, [FromBody] User user)
    {
        try
        {
            user.Id = id;
            await db.Client.From<User>().Update(user);
            return true;
        } catch (Exception)
        {
            return false;
        }
    }

    private static async Task<bool> Delete(SupabaseService db, int id)
    {
        try
        {
            var response = await db.Client.From<User>().Where(x => x.Id == id).Get();
            if (response.Model != null)
            {
                await db.Client.From<User>().Delete(response.Model);
            }
            return true;
        } catch (Exception)
        {
            return false;
        }
    }
}
