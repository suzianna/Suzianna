#tool "nuget:?package=xunit.runner.console"
#tool "nuget:?package=GitVersion.CommandLine"

using System.Text.RegularExpressions;

var solutionPath = Argument("SolutionPath", "../Code/Suzianna.sln");
var buildNumber = Argument("BuildNumber","0");

var projects = GetFiles("../Code/src/**/*.csproj");
var unitTestProjects = GetFiles("../Code/test/**/*Tests.Unit.csproj");
var integrationTestProjects = GetFiles("../Code/test/**/*Tests.Integration.csproj");
var allProjects = projects.Union(unitTestProjects).Union(integrationTestProjects).ToList();

var isRunningOnCiServer = AppVeyor.IsRunningOnAppVeyor;

Task("Clean")
    .Does(()=>{
      foreach(var project in allProjects) {
          DotNetCoreClean(project.FullPath);
      }
});

Task("Restore-NuGet-Packages")
    .Does(() =>
{
    var settings = new DotNetCoreRestoreSettings
    {
         PackagesDirectory = "../packages",
         DisableParallel = true,
    };
    foreach(var project in allProjects) {
        DotNetCoreRestore(project.FullPath, settings);
    }
});

Task("Version")
    .WithCriteria(isRunningOnCiServer)
    .Does(()=>
    {
        var gitVersion = GitVersion(new GitVersionSettings{ UpdateAssemblyInfo = true });

        var nugetVersion = gitVersion.NuGetVersion;

        Information("version is : " + gitVersion.NuGetVersion);

        var file = MakeAbsolute(File("../Code/Directory.Build.props"));
        var versionPattern = @"<Version>(.*)<\/Version>";

        var content = System.IO.File.ReadAllText(file.FullPath, Encoding.UTF8);
        var group = Regex.Match(content, versionPattern).Groups;
        var version = group[group.Count - 1].Value;
        version = "<Version>" + nugetVersion + "</Version>";
        content = Regex.Replace(content, versionPattern, version);

        System.IO.File.WriteAllText(file.FullPath, content, Encoding.UTF8);

        Information("Build.Props file updated");
    });



Task("Build")
    .Does(()=>
    {
        foreach(var project in allProjects) {
            DotNetCoreBuild(project.FullPath);
        }
    });

Task("Run-Unit-Tests")
    .Does(() =>
    {
        foreach(var file in unitTestProjects) {
            DotNetCoreTest(file.FullPath);
        }
    });

Task("Run-Integration-Tests")
    .Does(() =>
    {
        foreach(var file in integrationTestProjects) {
            DotNetCoreTest(file.FullPath);
        }
    });

Task("Create-Nuget-Packages")
    .WithCriteria(isRunningOnCiServer)
    .Does(()=>
    {
        CleanDirectories("./artifacts/**");
        var settings = new DotNetCorePackSettings
        {
            Configuration = "Release",
            OutputDirectory = "./artifacts/",
            IncludeSymbols = false,
            NoBuild=true,
            NoRestore=true,
        };

        DotNetCorePack(solutionPath, settings);
    });

Task("Push-Nuget-Packages")
    .WithCriteria(isRunningOnCiServer)
    .Does(() =>
    {
        var files = System.IO.Directory.GetFiles("./artifacts", "*.nupkg").Select(z => new FilePath(z)).ToList();
        var settings = new NuGetPushSettings()
        {
            Source = EnvironmentVariable("NUGET_SERVER_URL"),
            ApiKey = EnvironmentVariable("NUGET_API_KEY"),
        };

        foreach(var f in files){
            Console.WriteLine(f);
        }
        NuGetPush(files, settings);
    });

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Version")
    .IsDependentOn("Build")
    .IsDependentOn("Run-Unit-Tests")
    .IsDependentOn("Run-Integration-Tests")
    .IsDependentOn("Create-Nuget-Packages")
    .IsDependentOn("Push-Nuget-Packages")
    ;

RunTarget("Default");