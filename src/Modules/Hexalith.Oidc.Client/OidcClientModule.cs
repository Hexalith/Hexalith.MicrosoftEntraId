﻿namespace Hexalith.Oidc.Client;

using System.Collections.Generic;
using System.Reflection;

using Hexalith.Application.Modules.Modules;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Microsoft Entra ID client module.
/// </summary>
public class OidcClientModule : IClientApplicationModule
{
    /// <inheritdoc/>
    public IEnumerable<string> Dependencies => [];

    /// <inheritdoc/>
    public string Description => "Microsoft Entra ID client module";

    /// <inheritdoc/>
    public string Id => "Hexalith.Oidc.Client";

    /// <inheritdoc/>
    public string Name => "Microsoft Entra ID client";

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
    public static void AddClientModulesServices(IServiceCollection services, IConfiguration configuration)
        => _ = services.AddSingleton<AuthenticationStateProvider, ClientPersistentAuthenticationStateProvider>();
}