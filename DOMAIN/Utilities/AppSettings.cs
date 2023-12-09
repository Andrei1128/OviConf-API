namespace DOMAIN.Utilities;

public class AppSettings
{
    public JwtSettings JwtSettings { get; set; }
    public DBConnections DBConnections { get; set; }
}

public class JwtSettings
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public int ExpiresInHours { get; set; }
}

public class DBConnections
{
    public string SqlServer { get; set; } = string.Empty;
}