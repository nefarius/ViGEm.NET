using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using Nuke.Common;
using Nuke.Common.BuildServers;
using Nuke.Common.Git;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.MSBuild;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.MSBuild.MSBuildTasks;

class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Compile);

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;

    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    static string VerpatchUrl =>
        "https://downloads.vigem.org/other/pavel-a/ddverpatch/verpatch-1.0.15.1-x86-codeplex.zip";

    Target Clean => _ => _
        .Executes(() =>
        {
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            MSBuild(s => s
                .SetTargetPath(SolutionFile)
                .SetTargets("Restore"));
        });

    private Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            var costura64 = Path.Combine(WorkingDirectory, @"ViGEmClient\costura64");
            var dll64 = Path.Combine(costura64, "ViGEmClient.dll");
            var costura32 = Path.Combine(WorkingDirectory, @"ViGEmClient\costura32");
            var dll32 = Path.Combine(costura32, "ViGEmClient.dll");

            if (AppVeyor.Instance != null
                && Configuration.Equals("Release", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Going to build .NET library, updating native DLL version information...");

                var verpatchZip = Path.Combine(
                        WorkingDirectory,
                        "verpatch-1.0.15.1-x86-codeplex.zip"
                    );

                using (var client = new WebClient())
                {
                    Console.WriteLine("Downloading verpatch tool...");
                    client.DownloadFile(
                        VerpatchUrl,
                        verpatchZip);
                }

                Console.WriteLine("Extracting verpatch...");
                ZipFile.ExtractToDirectory(verpatchZip, WorkingDirectory);

                var verpatchTool = Path.Combine(WorkingDirectory, "verpatch.exe");

                Console.WriteLine($"Stamping version {AppVeyor.Instance.BuildVersion} into {dll64}...");
                ProcessUtil.StartWithArguments(verpatchTool, $"{dll64} {AppVeyor.Instance.BuildVersion}");
                ProcessUtil.StartWithArguments(verpatchTool, $"{dll64} /pv {AppVeyor.Instance.BuildVersion}");
                Console.WriteLine("Done");

                Console.WriteLine($"Stamping version {AppVeyor.Instance.BuildVersion} into {dll32}");
                ProcessUtil.StartWithArguments(verpatchTool, $"{dll32} {AppVeyor.Instance.BuildVersion}");
                ProcessUtil.StartWithArguments(verpatchTool, $"{dll32} /pv {AppVeyor.Instance.BuildVersion}");
                Console.WriteLine("Done");
            }

            MSBuild(s => s
                .SetTargetPath(SolutionFile)
                .SetTargets("Rebuild")
                .SetConfiguration(Configuration)
                .SetMaxCpuCount(Environment.ProcessorCount)
                .SetNodeReuse(IsLocalBuild)
                .SetTargetPlatform(MSBuildTargetPlatform.x64)
                .SetAssemblyVersion(AppVeyor.Instance?.BuildVersion)
                .SetFileVersion(AppVeyor.Instance?.BuildVersion)
                .SetInformationalVersion(AppVeyor.Instance?.BuildVersion)
                .SetPackageVersion(AppVeyor.Instance?.BuildVersion));

            MSBuild(s => s
                .SetTargetPath(SolutionFile)
                .SetTargets("Rebuild")
                .SetConfiguration(Configuration)
                .SetMaxCpuCount(Environment.ProcessorCount)
                .SetNodeReuse(IsLocalBuild)
                .SetTargetPlatform(MSBuildTargetPlatform.x86)
                .SetAssemblyVersion(AppVeyor.Instance?.BuildVersion)
                .SetFileVersion(AppVeyor.Instance?.BuildVersion)
                .SetInformationalVersion(AppVeyor.Instance?.BuildVersion)
                .SetPackageVersion(AppVeyor.Instance?.BuildVersion));

            if (!Directory.Exists(costura64))
                Directory.CreateDirectory(costura64);

            if (!Directory.Exists(costura32))
                Directory.CreateDirectory(costura32);

            File.Copy(
                Path.Combine(WorkingDirectory, @"bin\x64\ViGEmClient.dll"),
                dll64,
                true
            );

            File.Copy(
                Path.Combine(WorkingDirectory, @"bin\Win32\ViGEmClient.dll"),
                dll32,
                true
            );
        });

    private Target Pack => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            MSBuild(s => s
                .SetTargetPath(SolutionFile)
                .SetTargets("Restore", "Pack")
                .SetPackageOutputPath(ArtifactsDirectory)
                .SetConfiguration(Configuration)
                .EnableIncludeSymbols());
        });
}
