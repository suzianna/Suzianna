using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.AzurePipelines;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Tools.NuGet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.NuGet.NuGetTasks;
using static Nuke.Common.Tools.NuGet.NuGetPackSettingsExtensions;


[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.RunUnitTests);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;
    
    [Solution] readonly Solution Solution;
    [GitVersion] GitVersion GitVersion;
    [Parameter] string TestResultDirectory = RootDirectory + "/Artifacts/Test-Results/";
    [Parameter] string NugetOutputDirectory = RootDirectory + "/Artifacts/NugetPackages/";
    [Parameter] string NugetServerUrl;
    [Parameter] string NugetApiKey;

    Target Information => _ => _
        .Before(Preparation)
        .Executes(() =>
        {
            Logger.Info($"Configuration : {Configuration}");
            Logger.Info($"TestResultDirectory : {TestResultDirectory}");
            Logger.Info($"NugetOutputDirectory : {NugetOutputDirectory}");
            Logger.Info($"NugetOutputDirectory : {NugetServerUrl}");
            Logger.Info($"GitVersion.NuGetVersionV2 : {GitVersion.NuGetVersionV2}");
        });

    Target Preparation => _ => _
        .DependsOn(Information)
        .Executes(() =>
        {
            EnsureCleanDirectory(TestResultDirectory);
            EnsureCleanDirectory(NugetOutputDirectory);
        });

    Target Clean => _ => _
        .DependsOn(Preparation)
        .Executes(() =>
        {
            GlobDirectories(Solution.Directory, "**/bin", "**/obj").ForEach(DeleteDirectory);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(a => a.SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(a =>
                a.SetProjectFile(Solution)
                    .SetNoRestore(true)
                    .SetConfiguration(Configuration));
        });

    Target RunUnitTests => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            var testProjects = Solution.AllProjects.Where(s => s.Name.Contains("Tests.Unit"));

            DotNetTest(a => a
                .SetConfiguration(Configuration)
                .SetNoBuild(true)
                .SetNoRestore(true)
                .ResetVerbosity()
                .SetResultsDirectory(TestResultDirectory)
                    .EnableCollectCoverage()
                    .SetCoverletOutputFormat(CoverletOutputFormat.opencover)
                    .SetExcludeByFile("*.Generated.cs")
                    .EnableUseSourceLink()
                .CombineWith(testProjects, (b, z) => b
                    .SetProjectFile(z)
                    .SetLogger($"trx;LogFileName={z.Name}.trx")
                    .SetCoverletOutput(TestResultDirectory + $"{z.Name}.xml")));
        });

    Target PackNugetPackages => _ => _
        .DependsOn(RunUnitTests)
        .Executes(() =>
        {
            var projectsToPack = Solution.AllProjects.Where(s => !s.Name.Contains("Tests"));

            foreach (var project in projectsToPack)
            {
                DotNetPack(s => s
                    .SetProject(project)
                    .SetOutputDirectory(NugetOutputDirectory)
                    .SetNoBuild(true)
                    .SetNoRestore(true)
                    .SetConfiguration(Configuration)
                    .SetVersion(GitVersion.NuGetVersionV2));
            }
        });

    Target PushNugetPackages => _ => _
        .DependsOn(PackNugetPackages)
        .Executes(() =>
        {
            var nugetFiles = GlobFiles(NugetOutputDirectory, "*.nupkg");

            foreach (var file in nugetFiles)
            {
                DotNetNuGetPush(a => a
                        .SetApiKey(NugetApiKey)
                        .SetSource(NugetServerUrl)
                        .SetTargetPath(file));
            }
        });
}
