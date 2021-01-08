using System;
using System.IO;
using System.Linq;
using System.Net;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.AppVeyor;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.MSBuild;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.MSBuild.MSBuildTasks;

[CheckBuildProjectConfigurations]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            MSBuild(s => s
                .SetTargetPath(Solution)
                .SetTargets("Restore"));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            var url64 =
                "https://ci.appveyor.com/api/projects/nefarius/vigemclient/artifacts/bin/release/x64/ViGEmClient.dll?job=Platform%3A%20x64";
            var costura64 = Path.Combine(WorkingDirectory, @"ViGEmClient\costura64");
            var dll64 = Path.Combine(costura64, "ViGEmClient.dll");
            var url32 =
                "https://ci.appveyor.com/api/projects/nefarius/vigemclient/artifacts/bin/release/x86/ViGEmClient.dll?job=Platform%3A%20x86";
            var costura32 = Path.Combine(WorkingDirectory, @"ViGEmClient\costura32");
            var dll32 = Path.Combine(costura32, "ViGEmClient.dll");

            if (!Directory.Exists(costura64))
                Directory.CreateDirectory(costura64);
            if (!Directory.Exists(costura32))
                Directory.CreateDirectory(costura32);

            using (var wc = new WebClient())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(">> Downloading native x64 DLL");
                wc.DownloadFile(url64, dll64);
                Console.WriteLine(">> Downloading native x86 DLL");
                wc.DownloadFile(url32, dll32);
                Console.ResetColor();
            }

            //
            // Build .NET assembly
            // 
            MSBuild(s => s
                .SetTargetPath(Solution)
                .SetTargets("Rebuild")
                .SetMaxCpuCount(1)
                .SetNodeReuse(IsLocalBuild)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(AppVeyor.Instance?.BuildVersion)
                .SetFileVersion(AppVeyor.Instance?.BuildVersion)
                .SetInformationalVersion(AppVeyor.Instance?.BuildVersion)
                .SetPackageVersion(AppVeyor.Instance?.BuildVersion));
        });

}
