#tool "nuget:?package=xunit.runner.console"

var solutionPath = Argument("SolutionPath", "../Code/src/Suzianna.sln");
var buildNumber = Argument("BuildNumber","0");

var projects = GetFiles("../Code/src/**/*.csproj");
var testProjects = GetFiles("../Code/test/**/*.csproj");
var allProjects = projects.Union(testProjects).ToList();

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
    foreach(var file in testProjects) {
        DotNetCoreTest(file.FullPath);
    }
});

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build")
    .IsDependentOn("Run-Unit-Tests")
   ;

RunTarget("Default");