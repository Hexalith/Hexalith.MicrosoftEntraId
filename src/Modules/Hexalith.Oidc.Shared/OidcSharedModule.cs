namespace Hexalith.Oidc.Shared;

using System.Collections.Generic;
using System.Reflection;

using Hexalith.Application.Modules.Modules;
using Hexalith.Extensions.Configuration;
using Hexalith.Oidc.Shared.Configurations;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Microsoft Entra ID shared module.
/// </summary>
public class OidcSharedModule : ISharedApplicationModule
{
    /// <inheritdoc/>
    public IEnumerable<string> Dependencies => [];

    /// <inheritdoc/>
    public string Description => "Microsoft Entra ID shared module";

    /// <inheritdoc/>
    public string Id => "Hexalith.Oidc.Shared";

    /// <inheritdoc/>
    public string Name => "Microsoft Entra ID shared";

    /// <inheritdoc/>
    public int OrderWeight => 0;

    /// <inheritdoc/>
    public string Path => "hexalith/microsoftentraid";

    /// <inheritdoc/>
    public IEnumerable<Assembly> PresentationAssemblies => [GetType().Assembly];

    /// <inheritdoc/>
    public string Version => "1.0";

    /// <summary>
    /// Adds services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    public static void AddSharedModulesServices(IServiceCollection services, IConfiguration configuration)
    {
        _ = services
            .AddAuthorizationCore()
            .AddCascadingAuthenticationState()
            .ConfigureSettings<OidcSettings>(configuration);
    }
}