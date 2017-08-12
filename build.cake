var target = Argument("target", "Default");

Task("Default")
  .Does(() =>
{
  Information("Hello World!");

  MSBuild("./WebApplication1.sln", configurator =>
    configurator.SetConfiguration("Release")
        //.SetVerbosity(Verbosity.Minimal)
        .SetMaxCpuCount(0)
        .UseToolVersion(MSBuildToolVersion.VS2017)
        // .SetMSBuildPlatform(MSBuildPlatform.x86)
        .SetPlatformTarget(PlatformTarget.MSIL));
});

RunTarget(target);