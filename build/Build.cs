using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Nuke.Common;
using Nuke.Common.CI.AppVeyor;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.MSBuild;

using Serilog;

using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.Tools.MSBuild.MSBuildTasks;

class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] 
    readonly Solution Solution;

    Target Restore => _ => _
        .Executes(() =>
        {
            MSBuild(s => s
                .SetTargetPath(Solution)
                .SetTargets("Restore"));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(async () =>
        {
            var artifactBaseUrl = "https://ci.appveyor.com/api/projects/nefarius/vigemclient/artifacts/bin/release/";

            var url64 = "x64/ViGEmClient.dll?job=Platform%3A%20x64";
            var url32 = "x86/ViGEmClient.dll?job=Platform%3A%20x86";
            
            var costura64 = Path.Combine(WorkingDirectory, @"ViGEmClient\costura64");
            var costura32 = Path.Combine(WorkingDirectory, @"ViGEmClient\costura32");
            var dll64Path = Path.Combine(costura64, "ViGEmClient.dll");
            var dll32Path = Path.Combine(costura32, "ViGEmClient.dll");

            Directory.CreateDirectory(costura64);
            Directory.CreateDirectory(costura32);

            static async Task DownloadFileAsync(HttpClient httpClient, string url, string path)
            {
                using var responseStream = await httpClient.GetStreamAsync(url);
                using var fileStream = File.Create(path);

                await responseStream.CopyToAsync(fileStream);
            }

            using var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(artifactBaseUrl)
            };

            Log.Information(">> Downloading native x64 DLL");
            await DownloadFileAsync(httpClient, url64, dll64Path);
            
            Log.Information(">> Downloading native x86 DLL");
            await DownloadFileAsync(httpClient, url32, dll32Path);

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
