using Nuke.Common;
using Nuke.Common.BuildServers;
using Nuke.Common.Git;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Tools.MSBuild;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.MSBuild.MSBuildTasks;

class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly string Configuration = IsLocalBuild ? "Debug" : "Release";

    [Solution("ViGEm.NET.sln")] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [GitVersion] readonly GitVersion GitVersion;

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
                .SetTargetPath(Solution)
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

            //
            // Build native DLL (x64)
            // 
            MSBuild(s => s
                .SetTargetPath(Solution)
                .SetTargets("Rebuild")
                .SetMaxCpuCount(Environment.ProcessorCount)
                .SetNodeReuse(IsLocalBuild)
                .SetConfiguration($"{Configuration}_DLL")
                .SetTargetPlatform(MSBuildTargetPlatform.x64));

            //
            // Create costura64 path (used to automatically embed DLL in assembly)
            // 
            if (!Directory.Exists(costura64))
                Directory.CreateDirectory(costura64);

            //
            // Copy native DLL to embedder path
            // 
            File.Copy(
                Path.Combine(WorkingDirectory, $@"bin\{Configuration}\x64\ViGEmClient.dll"),
                dll64,
                true
            );

            //
            // Build native DLL (x86)
            // 
            MSBuild(s => s
                .SetTargetPath(Solution)
                .SetTargets("Rebuild")
                .SetMaxCpuCount(Environment.ProcessorCount)
                .SetNodeReuse(IsLocalBuild)
                .SetConfiguration($"{Configuration}_DLL")
                .SetTargetPlatform(MSBuildTargetPlatform.x86));

            //
            // Create costura32 path (used to automatically embed DLL in assembly)
            // 
            if (!Directory.Exists(costura32))
                Directory.CreateDirectory(costura32);

            //
            // Copy native DLL to embedder path
            // 
            File.Copy(
                Path.Combine(WorkingDirectory, $@"bin\{Configuration}\x86\ViGEmClient.dll"),
                dll32,
                true
            );

            //
            // If we run on build server, stamp DLLs with build version
            // 
            if (AppVeyor.Instance != null)
            {
                Console.WriteLine("Going to build .NET library, updating native DLL version information...");

                //
                // TODO: tidy up this section
                // 
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

                Console.WriteLine($"Stamping version {AppVeyor.Instance.BuildVersion} into {dll32}...");
                ProcessUtil.StartWithArguments(verpatchTool, $"{dll32} {AppVeyor.Instance.BuildVersion}");
                ProcessUtil.StartWithArguments(verpatchTool, $"{dll32} /pv {AppVeyor.Instance.BuildVersion}");
                Console.WriteLine("Done");
            }

            //
            // Build .NET assembly
            // 
            MSBuild(s => s
                .SetTargetPath(Solution)
                .SetTargets("Rebuild")
                .SetMaxCpuCount(Environment.ProcessorCount)
                .SetNodeReuse(IsLocalBuild)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(AppVeyor.Instance?.BuildVersion)
                .SetFileVersion(AppVeyor.Instance?.BuildVersion)
                .SetInformationalVersion(AppVeyor.Instance?.BuildVersion)
                .SetPackageVersion(AppVeyor.Instance?.BuildVersion));
        });

    private Target Pack => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            MSBuild(s => s
                .SetTargetPath(Solution)
                .SetTargets("Restore", "Pack")
                .SetPackageOutputPath(ArtifactsDirectory)
                .SetConfiguration(Configuration)
                .EnableIncludeSymbols());
        });
}
