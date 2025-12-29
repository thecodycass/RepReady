using Microsoft.AspNetCore.Mvc;
using RepReady.DTOs;
using RepReady.Models.ExerciseTemplate;
using RepReady.Services;
using Supabase.Postgrest;

namespace RepReady.Controllers.MinimalAPI;

public static class ExerciseTemplateEndpoints
{
    public static void MapExerciseTemplateEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("v1/exercise_templates");
        endpoints.MapGet("/{id}", Get);
        endpoints.MapGet("getExerciseTemplatesByWorkoutTemplateId/{id}",
            GetExerciseTemplatesByWorkoutTemplateId);
        endpoints.MapPost("/", Create);
        endpoints.MapPut("/{id}", Update);
        endpoints.MapDelete("/{id}", Delete);
    }

    private static async Task<ExerciseTemplateDto?> Get(SupabaseService db, int id)
    {
        var response = await db.Client.From<ExerciseTemplate>().Where(t => t.Id == id).Get();
        return response.Model?.ToDto();
    }

    private static async Task<IEnumerable<ExerciseTemplateDto>> GetExerciseTemplatesByWorkoutTemplateId(
        SupabaseService db, int id)
    {
        var response = await db.Client.From<ExerciseTemplateWithDetails>()
            // pull in related exercise + workout template data
            .Select("*, exercise:exercises(*), workout_template:workout_templates(*)")
            .Where(t => t.WorkoutTemplateId == id)
            .Order(t => t.SortOrder, Constants.Ordering.Ascending)
            .Get();
        return response.Models.ToDto();
    }
    
    private static async Task<IResult> Create(SupabaseService db, [FromBody] ExerciseTemplate template)
    {
        try
        {
            var response = await db.Client.From<ExerciseTemplate>().Insert(template);
            return Results.Ok(response.Model?.ToDto());
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    
    private static async Task<bool> Update(SupabaseService db, int id, [FromBody] ExerciseTemplate template)
    {
        try
        {
            template.Id = id;
            await db.Client.From<ExerciseTemplate>().Update(template);
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
            var response = await db.Client.From<ExerciseTemplate>().Where(t => t.Id == id).Get();
            if (response.Model != null)
            {
                await db.Client.From<ExerciseTemplate>().Delete(response.Model);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
