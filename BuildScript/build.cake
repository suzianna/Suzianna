#tool "nuget:?package=xunit.runner.console"
#tool "nuget:?package=GitVersion.CommandLine"

var solutionPath = Argument("SolutionPath", "../Code/src/Suzianna.sln");
var buildNumber = Argument("BuildNumber","0");

var projects = GetFiles("../Code/src/**/*.csproj");
var unitTestProjects = GetFiles("../Code/test/**/*Tests.Unit.csproj");
var integrationTestProjects = GetFiles("../Code/test/**/*Tests.Integration.csproj");

var allProjects = projects.Union(unitTestProjects).Union(integrationTestProjects).ToList();

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
    .Does(()=>
    {
        
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

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Version")
    .IsDependentOn("Build")
    .IsDependentOn("Run-Unit-Tests")
    .IsDependentOn("Run-Integration-Tests")
   ;

RunTarget("Default");