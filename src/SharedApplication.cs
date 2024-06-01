namespace Hexalith.Oidc.Client;

using Hexalith.Application.Modules.Applications;

public class SharedApplication : IApplicationDefinition
{
    /// <inheritdoc/>
    public string? HomePath { get; }

    /// <inheritdoc/>
    public string? LoginPath { get; }

    /// <inheritdoc/>
    public string? LogoutPath { get; }

    /// <inheritdoc/>
    public IEnumerable<Type>? Modules { get; }

    /// <inheritdoc/>
    public IEnumerable<Type>? SharedModules { get; }
}