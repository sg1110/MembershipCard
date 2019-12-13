var solution = "../MembershipCardSystem.sln";
var buildConfiguration = "Release";

Task("Build")
    .Does(() =>
{
    var buildSettings = new DotNetCoreBuildSettings{
        MSBuildSettings = new DotNetCoreMSBuildSettings()
                                .SetConfiguration(buildConfiguration)
                                .WithTarget("Rebuild")
    };

    DotNetCoreBuild(solution, buildSettings);
});

Task("Unit-Test")
    .Does(() =>
{
    DotNetCoreTest("../tests/MembershipCardSystem.UnitTests/MembershipCardSystem.UnitTests.csproj");
});

Task("Integration-Test")
    .Does(() =>
{
    DotNetCoreTest("../tests/MembershipCardSystem.IntegrationTests/MembershipCardSystem.IntegrationTests.csproj");
});

Task("default")
    .IsDependentOn("Build")
    .IsDependentOn("Unit-Test")
    .IsDependentOn("Integration-Test");

RunTarget("default");