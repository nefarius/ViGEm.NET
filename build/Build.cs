using Nuke.Common.Tools.MSBuild;
using Nuke.Core;
using Nuke.Core.BuildServers;
using static Nuke.Common.Tools.MSBuild.MSBuildTasks;
using static Nuke.Core.IO.FileSystemTasks;
using static Nuke.Core.IO.PathConstruction;

class Build : NukeBuild
{
    // Auto-injection fields:

    // [GitVersion] readonly GitVersion GitVersion;
    // Semantic versioning. Must have 'GitVersion.CommandLine' referenced.

    // [GitRepository] readonly GitRepository GitRepository;
    // Parses origin, branch name and head from git config.

    // [Parameter] readonly string MyGetApiKey;
    // Returns command-line arguments and environment variables.

    Target Clean => _ => _
        .OnlyWhen(() => false) // Disabled for safety.
        .Executes(() =>
        {
            DeleteDirectories(GlobDirectories(SourceDirectory, "**/bin", "**/obj"));
            EnsureCleanDirectory(OutputDirectory);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            MSBuild(s => DefaultMSBuildRestore);
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            MSBuild(s => DefaultMSBuildCompile
                .SetAssemblyVersion(AppVeyor.Instance.BuildVersion)
                .SetFileVersion(AppVeyor.Instance.BuildVersion)
                .SetInformationalVersion(AppVeyor.Instance.BuildVersion));
        });

    // Console application entry. Also defines the default target.
    public static int Main()
    {
        return Execute<Build>(x => x.Compile);
    }
}