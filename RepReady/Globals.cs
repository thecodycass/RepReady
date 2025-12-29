namespace RepReady;

public static class Globals
{
    // Keep this Sorted ASC at all times.
    public static readonly List<string> BodyRegions =
        [
            "abs",
            "back",
            "biceps",
            "calves",
            "chest",
            "forearms",
            "glutes",
            "hamstrings",
            "neck",
            "obliques",
            "quads",
            "shoulders",
            "traps",
            "triceps",
        ];

    public static readonly List<string> ExerciseCategories =
    [
        "push",
        "pull",
        "legs",
        "cardio",
    ];

    public static readonly int BaseDefaultSets = 3;
    public static readonly int BaseDefaultReps = 10;
    public static readonly int BaseDefaultWeight = 0;

    public static readonly List<string> SetTypes =
    [
        "Regular",
        "Myorep Match",
        "Pyramid Up",
        "Pyramid Down",
        "Super Set"
    ];
}
