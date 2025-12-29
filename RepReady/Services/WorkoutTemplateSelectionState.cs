using RepReady.DTOs;

namespace RepReady.Services;

/// <summary>
/// Simple in-memory state container used to carry the selected WorkoutTemplateDto
/// across navigation (e.g., list -> show) without another API call.
/// 
/// Note: because this is in-memory, it will be lost on refresh/deep-link.
/// </summary>
public class WorkoutTemplateSelectionState
{
    public WorkoutTemplateDto? Selected { get; private set; }

    public void Set(WorkoutTemplateDto template) => Selected = template;

    public void Clear() => Selected = null;
}
