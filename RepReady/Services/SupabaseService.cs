using Microsoft.Extensions.Options;
using RepReady.Configuration;
using Supabase;

namespace RepReady.Services;

// Service for managing Supabase client connection
public class SupabaseService
{
    private readonly SupabaseSettings _settings;
    private Client? _client;

    public SupabaseService(IOptions<SupabaseSettings> settings)
    {
        _settings = settings.Value;
    }
    
    // Gets the Supabase client instance
    public Client Client
    {
        get
        {
            if (_client == null)
            {
                throw new InvalidOperationException(
                    "Supabase client has not been initialized. Call InitializeAsync first.");
            }
            return _client;
        }
    }
    
    // Initializes the Supabase client connection
    public async Task InitializeAsync()
    {
        if (_client != null)
        {
            return; // Already initialized
        }

        var options = new SupabaseOptions
        {
            AutoRefreshToken = true,
            AutoConnectRealtime = true
        };

        _client = new Client(_settings.Url, _settings.Key, options);
        await _client.InitializeAsync();
    }
    
    // Checks if the client is initialized
    public bool IsInitialized => _client != null;
}
