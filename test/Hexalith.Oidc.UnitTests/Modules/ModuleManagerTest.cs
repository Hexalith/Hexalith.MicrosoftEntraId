// <copyright file="ModuleManagerTest.cs" company="Fiveforty SAS Paris France">
//     Copyright (c) Fiveforty SAS Paris France. All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Oidc.UnitTests.Modules;

using FluentAssertions;

using Hexalith.Application.Modules;
using Hexalith.Application.Modules.Configurations;
using Hexalith.Oidc.Client;
using Hexalith.Oidc.Server;
using Hexalith.Oidc.Shared;
using Hexalith.Oidc.UnitTests.Configurations;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

public class ModuleManagerTest
{
    [Fact]
    public void ClientServicesFromModulesShouldBeAdded()
    {
        ServiceCollection services = [];
        Mock<IConfiguration> configurationMock = new();

        ModuleManager.AddClientModulesServices(services, configurationMock.Object);
        _ = ModuleManager.ClientModuleTypes.Should().HaveCount(1);

        // Check that the services have been added
        _ = services
            .Should()
            .HaveCount(1);
    }

    [Fact]
    public void ModuleManagerShouldReturnClientAssemby()
    {
        ILogger<ModuleManager> logger = Mock.Of<ILogger<ModuleManager>>();
        IOptions<ModuleSettings> options = Options.Create(new ModuleSettings());

        ModuleManager manager = new([], options, logger);
        _ = ModuleManager.ClientModuleTypes.Should().HaveCount(1);
        _ = manager
            .ClientPresentationAssemblies
            .Should()
            .HaveCount(2);
        _ = manager
            .ClientPresentationAssemblies
            .Should()
            .Contain(typeof(OidcClientModule).Assembly);
        _ = manager
            .ClientPresentationAssemblies
            .Should()
            .Contain(typeof(OidcSharedModule).Assembly);
    }

    [Fact]
    public void ModuleManagerShouldReturnClientModule()
    {
        ILogger<ModuleManager> logger = Mock.Of<ILogger<ModuleManager>>();
        IOptions<ModuleSettings> options = Options.Create(new ModuleSettings());

        ModuleManager manager = new([], options, logger);
        _ = ModuleManager.ClientModuleTypes.Should().HaveCount(1);
        _ = manager
            .ClientModules
            .Select(p => p.Value)
            .OfType<OidcClientModule>()
            .Should()
            .HaveCount(1);
    }

    [Fact]
    public void ModuleManagerShouldReturnServerAssemby()
    {
        ILogger<ModuleManager> logger = Mock.Of<ILogger<ModuleManager>>();
        IOptions<ModuleSettings> options = Options.Create(new ModuleSettings());

        ModuleManager manager = new([], options, logger);
        _ = ModuleManager.ServerModuleTypes.Should().HaveCount(1);
        _ = manager
            .ServerPresentationAssemblies
            .Should()
            .HaveCount(3);
        _ = manager
            .ServerPresentationAssemblies
            .Should()
            .Contain(typeof(OidcServerModule).Assembly);
        _ = manager
            .ServerPresentationAssemblies
            .Should()
            .Contain(typeof(OidcSharedModule).Assembly);
        _ = manager
            .ServerPresentationAssemblies
            .Should()
            .Contain(typeof(OidcClientModule).Assembly);
    }

    [Fact]
    public void ModuleManagerShouldReturnServerModule()
    {
        ILogger<ModuleManager> logger = Mock.Of<ILogger<ModuleManager>>();
        IOptions<ModuleSettings> options = Options.Create(new ModuleSettings());

        ModuleManager manager = new([], options, logger);
        _ = ModuleManager.ServerModuleTypes.Should().HaveCount(1);
        _ = manager
            .ServerModules
            .Select(p => p.Value)
            .OfType<OidcServerModule>()
            .Should()
            .HaveCount(1);
    }

    [Fact]
    public void ModuleManagerShouldReturnSharedModule()
    {
        ILogger<ModuleManager> logger = Mock.Of<ILogger<ModuleManager>>();
        IOptions<ModuleSettings> options = Options.Create(new ModuleSettings());

        ModuleManager manager = new([], options, logger);
        _ = ModuleManager.SharedModuleTypes.Should().HaveCount(1);
        _ = manager
            .SharedModules
            .Select(p => p.Value)
            .OfType<OidcSharedModule>()
            .Should()
            .HaveCount(1);
    }

    [Fact]
    public void ServerServicesFromModulesShouldBeAdded()
    {
        ServiceCollection services = [];
        Mock<IConfiguration> configurationMock = new();
        _ = ModuleManager
            .ServerModuleTypes
            .Should()
            .Contain(typeof(OidcServerModule));

        ModuleManager.AddServerModulesServices(services, configurationMock.Object);

        // Check that the services have been added
        _ = services
            .Should()
            .NotBeEmpty();
    }

    [Fact]
    public void SharedServicesFromModulesShouldBeAdded()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(OidcSettingsTest.TestSettings)
            .Build();
        ServiceCollection services = [];
        _ = ModuleManager.SharedModuleTypes.Should().HaveCount(1);

        _ = ModuleManager
            .SharedModuleTypes
            .Should()
            .Contain(typeof(OidcSharedModule));

        ModuleManager.AddSharedModulesServices(services, configuration);

        // Check that the services have been added
        _ = services
            .Should()
            .NotBeEmpty();
    }
}