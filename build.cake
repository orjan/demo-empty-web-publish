#tool nuget:?package=vswhere

var target = Argument("target", "Default");

// DirectoryPath vsLatest  = VSWhereLatest(new VSWhereLatestSettings { Requires = "Microsoft.VisualStudio.Product.BuildTools"});

DirectoryPath buildToolsInstallation = VSWhereProducts("Microsoft.VisualStudio.Product.BuildTools").Single();
FilePath msBuildPathX64 = buildToolsInstallation.CombineWithFilePath("./MSBuild/15.0/Bin/amd64/MSBuild.exe");

Task("Default")
  .Does(() =>
{
  Information("Hello World!");

  MSBuild("./WebApplication1.sln", configurator => {
    configurator.ToolPath = msBuildPathX64;
    configurator.SetConfiguration("Release")
        .SetVerbosity(Verbosity.Diagnostic)
        .SetMaxCpuCount(0)
        // .UseToolVersion(MSBuildToolVersion.VS2017)
        // .SetMSBuildPlatform(MSBuildPlatform.x64)
        .SetPlatformTarget(PlatformTarget.MSIL)
        .WithTarget("Rebuild") 
        // .WithProperty("DeployOnBuild", "true") // Needed for publishing the shit
        // .WithProperty("PublishProfile","FolderProfile")
        .WithProperty("BuildInParallel", "true") // This is the default value
        .SetDetailedSummary(true);

        // .WithProperty("TargetFrameworkSDKToolsDirectory", "\"C:/Program Files (x86)/Microsoft SDKs/Windows/v10.0A/bin/NETFX 4.6.2 Tools/\"")  
  });

});

RunTarget(target);