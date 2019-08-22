#tool "nuget:?package=xunit.runner.console&version=2.4.1"
#tool "nuget:?package=GitVersion.CommandLine&version=4.0.0"
#tool "nuget:?package=OpenCover&version=4.7.922"
#tool nuget:?package=Codecov&version=1.5.0
#addin nuget:?package=Cake.Codecov&version=0.6.0

using System.Text.RegularExpressions;

var solutionPath = Argument("SolutionPath", "../Code/Suzianna.sln");
var buildNumber = Argument("BuildNumber","0");
var shouldPublish = Argument("ShouldPublish", false);
var branchName = Argument("BranchName", "");

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

Task("Prepare-Test-Result-Folder")
    .Does(() =>
    {
        EnsureDirectoryExists("./test-results");
        CleanDirectories("./test-results");
    }
);

Task("Run-Unit-Tests")
    .Does(() =>
    {
        foreach(var file in unitTestProjects) {
            var name = System.IO.Path.GetFileName(file.FullPath).Replace(".Tests.Unit.csproj","");

            var settings = new OpenCoverSettings().WithFilter("+[" + name + "*]*").WithFilter("-[*Tests*]*");
            settings.MergeOutput = true;
            settings.OldStyle = true;

            OpenCover(tool => {
                tool.DotNetCoreTest(file.FullPath);
            },
                new FilePath("./test-results/result.xml"),
                settings
            );
        }
});

Task("Publish-Unit-Tests-Coverage-Result")
    .WithCriteria(isRunningOnCiServer)
    .Does(() =>
    {
        Codecov(new CodecovSettings(){
            Files = new[] { "./test-results/result.xml" },
            Token = EnvironmentVariable("CODECOV_TOKEN"),
            Branch = branchName
        });
    }
);

Task("Run-Integration-Tests")
    .Does(() =>
    {
        foreach(var file in integrationTestProjects) {
            DotNetCoreTest(file.FullPath);
        }
    });

Task("Create-Nuget-Packages")
    .WithCriteria(isRunningOnCiServer && shouldPublish)
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
    .WithCriteria(isRunningOnCiServer && shouldPublish)
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
    .IsDependentOn("Prepare-Test-Result-Folder")
    .IsDependentOn("Run-Unit-Tests")
    .IsDependentOn("Publish-Unit-Tests-Coverage-Result")
    .IsDependentOn("Run-Integration-Tests")
    .IsDependentOn("Create-Nuget-Packages")
    .IsDependentOn("Push-Nuget-Packages")
    ;

RunTarget("Default");