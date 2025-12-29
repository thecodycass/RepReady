using Microsoft.AspNetCore.Mvc;
using RepReady.DTOs;
using RepReady.Models.WorkoutTemplate;
using RepReady.Services;
using Supabase.Postgrest;

namespace RepReady.Controllers.MinimalAPI;

/**
 * API's for the WorkoutTemplates table in Supabase.
 */
public static class WorkoutTemplateEndpoints
{
    public static void MapWorkoutTemplateEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("v1/workout_templates");
        endpoints.MapGet("/{id}", Get);
        endpoints.MapGet("getWorkoutTemplatesByUserId/{userId}", GetWorkoutTemplatesByUserId);
        endpoints.MapPost("/", Create);
        endpoints.MapPut("/{id}", Update);
        endpoints.MapDelete("/{id}", Delete);
    }

    private static async Task<WorkoutTemplateDto?> Get(SupabaseService db, int id)
    {
        var response = await db.Client.From<WorkoutTemplate>().Where(t => t.Id == id).Get();
        return response.Model?.ToDto();
    }

    private static async Task<IEnumerable<WorkoutTemplateDto>> GetWorkoutTemplatesByUserId(SupabaseService db, int userId)
    {
        var response = await db.Client.From<WorkoutTemplate>()
            .Where(t => t.UserId == userId)
            .Order(t => t.Id, Constants.Ordering.Ascending)
            .Get();
        return response.Models.ToDto();
    }
    
    private static async Task<IResult> Create(SupabaseService db, [FromBody] WorkoutTemplate template)
    {
        try
        {
            var response = await db.Client.From<WorkoutTemplate>().Insert(template);
            return Results.Ok(response.Model.ToDto());
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    
    private static async Task<bool> Update(SupabaseService db, int id, [FromBody] WorkoutTemplate template)
    {
        try
        {
            template.Id = id;
            await db.Client.From<WorkoutTemplate>().Update(template);
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
            var response = await db.Client.From<WorkoutTemplate>().Where(t => t.Id == id).Get();
            if (response.Model != null)
            {
                await db.Client.From<WorkoutTemplate>().Delete(response.Model);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
