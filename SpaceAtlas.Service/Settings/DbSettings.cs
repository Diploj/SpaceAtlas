namespace SpaceAtlas.Settings;

public class DbSettings
{
    public string? ConnectionString  { get; set; } 
    
    public DbSettings(IConfiguration configuration,string key)
    {
        ConnectionString = configuration.GetValue<string>(key);
    }
}