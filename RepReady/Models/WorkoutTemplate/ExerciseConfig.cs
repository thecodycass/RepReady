namespace RepReady.Models.WorkoutTemplate;

public class ExerciseConfig
{
    public int DefaultSets { get; set; } = Globals.BaseDefaultSets;
    public int DefaultReps { get; set; } = Globals.BaseDefaultReps;
    public int SortOrder { get; set; }
    public float Weight { get; set; } = Globals.BaseDefaultWeight;
    public string SetType { get; set; } = "regular";
}
