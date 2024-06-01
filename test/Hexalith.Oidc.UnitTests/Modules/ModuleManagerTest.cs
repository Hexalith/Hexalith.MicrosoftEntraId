// <copyright file="ModuleManagerTest.cs" company="Fiveforty SAS Paris France">
//     Copyright (c) Fiveforty SAS Paris France. All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Oidc.UnitTests.Modules;

using FluentAssertions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

public class ModuleManagerTest
{
    [Fact]
    public void DummyServicesFromModulesShouldBeAdded()
    {
        ServiceCollection services = [];
        Mock<IConfiguration> configurationMock = new();

        ModuleManager.AddSharedModulesServices(services, configurationMock.Object);

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
            .Contain(typeof(HexalithClientModule).Assembly);
        _ = manager
            .ClientPresentationAssemblies
            .Should()
            .Contain(typeof(HexalithSharedModule).Assembly);
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
            .OfType<HexalithClientModule>()
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
            .Contain(typeof(HexalithServerModule).Assembly);
        _ = manager
            .ServerPresentationAssemblies
            .Should()
            .Contain(typeof(HexalithSharedModule).Assembly);
        _ = manager
            .ServerPresentationAssemblies
            .Should()
            .Contain(typeof(HexalithClientModule).Assembly);
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
            .OfType<HexalithServerModule>()
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
            .OfType<HexalithSharedModule>()
            .Should()
            .HaveCount(1);
    }
}