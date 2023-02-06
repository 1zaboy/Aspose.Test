using Aspose.Core.Services;
using Aspose.Core.Storages;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddAsposeServices(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddSingleton<IImageService, ImageService>();
        services.AddSingleton<IImageStorage, ImageStorage>();

        return services;
    }
    
}