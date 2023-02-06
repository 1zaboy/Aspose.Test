namespace Microsoft.Extensions.DependencyInjection;

public class CorsSettings
{
    public List<string> AllowedCorsOrigins { get; } = new List<string>();
}