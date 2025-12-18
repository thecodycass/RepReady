# Supabase Setup and Usage Guide

## Overview
This project is configured to use Supabase for database storage. The connection is set up and ready to use once you add your credentials.

## Setup Instructions

### 1. Add Your Supabase Credentials

Update the `appsettings.json` file with your Supabase project credentials:

```json
{
  "Supabase": {
    "Url": "https://your-project.supabase.co",
    "Key": "your-anon-public-key"
  }
}
```

**Where to find these:**
- Go to your [Supabase Dashboard](https://app.supabase.com)
- Select your project
- Go to Settings → API
- Copy the **Project URL** and **anon/public** key

### 2. Create Your Models

Create your data models in a `Models` folder. Example:

```csharp
namespace RepReady.Models;

using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("your_table_name")]
public class YourModel : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
```

## Usage in Blazor Components

### Basic Usage Example

```razor
@page "/example"
@using RepReady.Services
@using RepReady.Models
@inject SupabaseService SupabaseService

<h3>Supabase Example</h3>

@code {
    private List<YourModel> items = new();

    protected override async Task OnInitializedAsync()
    {
        // Fetch all records
        var response = await SupabaseService.Client
            .From<YourModel>()
            .Get();
        
        items = response.Models;
    }

    private async Task AddItem(string name)
    {
        var newItem = new YourModel { Name = name };
        
        await SupabaseService.Client
            .From<YourModel>()
            .Insert(newItem);
        
        // Refresh the list
        await OnInitializedAsync();
    }

    private async Task UpdateItem(YourModel item)
    {
        await SupabaseService.Client
            .From<YourModel>()
            .Update(item);
    }

    private async Task DeleteItem(int id)
    {
        await SupabaseService.Client
            .From<YourModel>()
            .Where(x => x.Id == id)
            .Delete();
    }
}
```

## Common Operations

### Select/Query Data

```csharp
// Get all records
var response = await SupabaseService.Client
    .From<YourModel>()
    .Get();

// Get with filter
var response = await SupabaseService.Client
    .From<YourModel>()
    .Where(x => x.Name == "Example")
    .Get();

// Get single record
var response = await SupabaseService.Client
    .From<YourModel>()
    .Where(x => x.Id == 1)
    .Single();

// Order by
var response = await SupabaseService.Client
    .From<YourModel>()
    .Order(x => x.CreatedAt, Supabase.Postgrest.Constants.Ordering.Descending)
    .Get();

// Limit results
var response = await SupabaseService.Client
    .From<YourModel>()
    .Limit(10)
    .Get();
```

### Insert Data

```csharp
var newItem = new YourModel 
{ 
    Name = "New Item",
    CreatedAt = DateTime.UtcNow
};

var response = await SupabaseService.Client
    .From<YourModel>()
    .Insert(newItem);
```

### Update Data

```csharp
item.Name = "Updated Name";

await SupabaseService.Client
    .From<YourModel>()
    .Update(item);
```

### Delete Data

```csharp
await SupabaseService.Client
    .From<YourModel>()
    .Where(x => x.Id == itemId)
    .Delete();
```

### Realtime Subscriptions

```csharp
// Subscribe to table changes
var channel = SupabaseService.Client.Realtime.Channel("public:your_table_name");

channel.OnInsert += (sender, change) =>
{
    // Handle new record
    Console.WriteLine($"New record: {change.Model}");
};

channel.OnUpdate += (sender, change) =>
{
    // Handle updated record
    Console.WriteLine($"Updated record: {change.Model}");
};

channel.OnDelete += (sender, change) =>
{
    // Handle deleted record
    Console.WriteLine($"Deleted record: {change.Model}");
};

await channel.Subscribe();
```

## Authentication (Optional)

If you need authentication:

```csharp
// Sign up
var session = await SupabaseService.Client.Auth.SignUp("email@example.com", "password");

// Sign in
var session = await SupabaseService.Client.Auth.SignIn("email@example.com", "password");

// Sign out
await SupabaseService.Client.Auth.SignOut();

// Get current user
var user = SupabaseService.Client.Auth.CurrentUser;
```

## Storage (Files)

```csharp
// Upload file
var bucket = SupabaseService.Client.Storage.From("bucket-name");
await bucket.Upload(fileBytes, "file-path.jpg");

// Download file
var fileBytes = await bucket.Download("file-path.jpg");

// Get public URL
var publicUrl = bucket.GetPublicUrl("file-path.jpg");
```

## Error Handling

Always wrap Supabase calls in try-catch blocks:

```csharp
try
{
    var response = await SupabaseService.Client
        .From<YourModel>()
        .Get();
    
    items = response.Models;
}
catch (Exception ex)
{
    // Handle error
    Console.WriteLine($"Error: {ex.Message}");
}
```

## Additional Resources

- [Supabase C# Documentation](https://supabase.com/docs/reference/csharp)
- [Postgrest C# Client](https://github.com/supabase-community/postgrest-csharp)
- [Supabase Dashboard](https://app.supabase.com)

## Notes

- The Supabase client is initialized as a singleton and is automatically connected on application startup
- Make sure your database tables exist in Supabase before querying them
- Use the Supabase Dashboard to create and manage your database tables
- The `anon` key is safe to use in client-side code - it's designed for this purpose
- For production, consider using Row Level Security (RLS) policies in Supabase
