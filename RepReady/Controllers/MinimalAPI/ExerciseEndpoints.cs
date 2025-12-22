using Microsoft.AspNetCore.Mvc;
using RepReady.Models;
using RepReady.DTOs;
using RepReady.Services;

namespace RepReady.Controllers.MinimalAPI;

/**
 * API's for the Exercise table in Supabase.
 */
public static class ExerciseEndpoints
{
    public static void MapExerciseEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("v1/exercises");
        endpoints.MapGet("/", GetAll);
        endpoints.MapGet("/{id}", Get);
        endpoints.MapGet("/getExerciseByBodyRegion/{bodyRegion}", GetExerciseByBodyRegion);
        endpoints.MapGet("getExerciseByCategory/{category}", GetExerciseByCategory);
        endpoints.MapPost("/", Create);
        endpoints.MapPut("/{id}", Update);
        endpoints.MapDelete("/{id}", Delete);
    }

    private static async Task<IEnumerable<ExerciseDto>> GetAll(SupabaseService db)
    {
        var response = await db.Client.From<Exercise>().Get();
        return response.Models.ToDto();
    }

    private static async Task<ExerciseDto?> Get(SupabaseService db, int id)
    {
        var response = await db.Client.From<Exercise>().Where(e => e.Id == id).Get();
        return response.Model?.ToDto();
    }

    private static async Task<IEnumerable<ExerciseDto>> GetExerciseByBodyRegion(SupabaseService db, string bodyRegion)
    {
        bodyRegion = bodyRegion.ToLower();
        var response = await db.Client.From<Exercise>().Where(e => e.BodyRegion == bodyRegion).Get();
        return response.Models.ToDto();
    }

    private static async Task<IEnumerable<ExerciseDto>> GetExerciseByCategory(SupabaseService db, string category)
    {
        category = category.ToLower();
        var response = await db.Client.From<Exercise>().Where(e => e.Category == category).Get();
        return response.Models.ToDto();
    }
    
    private static async Task<IResult> Create(SupabaseService db, [FromBody] Exercise exercise)
    {
        try
        {
            await db.Client.From<Exercise>().Insert(exercise);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    private static async Task<bool> Update(SupabaseService db, int id, [FromBody] Exercise exercise)
    {
        try
        {
            exercise.Id = id;
            await db.Client.From<Exercise>().Update(exercise);
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
            var response = await db.Client.From<Exercise>().Where(e => e.Id == id).Get();
            if (response.Model != null)
            {
                await db.Client.From<Exercise>().Delete(response.Model);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}