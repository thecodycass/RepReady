namespace RepReady.Configuration;

// Configuration settings for Supabase connection
public class SupabaseSettings
{
    //The Supabase project URL
    public string Url { get; set; } = string.Empty;
    
    // The Supabase anonymous/public API key
    public string Key { get; set; } = string.Empty;
}
