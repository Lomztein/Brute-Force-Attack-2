using Lomztein.BFA2.Editor.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Builder : MonoBehaviour
{
    private static readonly string buildPrefix = "BFA2";

    private static readonly Dictionary<BuildTarget, string> buildSuffixes = new Dictionary<BuildTarget, string>()
        {
            { BuildTarget.StandaloneWindows, "Win32" },
            { BuildTarget.StandaloneWindows64, "Win64" },
            { BuildTarget.StandaloneLinux64, "Linux" },
            { BuildTarget.StandaloneOSX, "OSX" },
        };

    private static readonly Dictionary<BuildTarget, string> buildExtensions = new Dictionary<BuildTarget, string>()
        {
            { BuildTarget.StandaloneWindows, ".exe" },
            { BuildTarget.StandaloneWindows64, ".exe" },
            { BuildTarget.StandaloneLinux64, ".x86" },
            { BuildTarget.StandaloneOSX, ".app" },
        };

    private static readonly Dictionary<BuildTarget, string> buildChannels = new Dictionary<BuildTarget, string>()
        {
            { BuildTarget.StandaloneWindows, "windows" },
            { BuildTarget.StandaloneWindows64, "windows" },
            { BuildTarget.StandaloneLinux64, "linux" },
            { BuildTarget.StandaloneOSX, "osx" },
        };

    private static readonly string[] buildScenes =
        {
            "Assets/Scenes/MainMenu.unity",
            "Assets/Scenes/Battlefield.unity",
            "Assets/Scenes/AssemblyEditor.unity",
            "Assets/Scenes/MapEditor.unity"
        };

    [MenuItem("BFA2/Build")]
    public static void BuildGame ()
    {
        SetBuildVersion();
        BuildGame(Directory.GetParent(Application.dataPath) + "\\Build\\", "StandaloneWindows64");
    }

    public static void CDBuildGame ()
    {
        SetBuildVersion();
        BuildGame("./build/", Environment.GetEnvironmentVariable("BUILD_TARGET"));
    }

    private static void SetBuildVersion ()
    {
        DateTime lastMinorRelease = new DateTime(2021, 7, 8);

        string patch = (DateTime.Now - lastMinorRelease).Days.ToString();
        string build = (DateTime.Now.Second + DateTime.Now.Minute * 60 + DateTime.Now.Hour * 3600).ToString();

        PlayerSettings.bundleVersion = $"0.2.{patch}.{build}";
    }

    public static void BuildGame(params string[] args)
    {
        BuildTarget[] targets = args.Skip(1).Select(x => (BuildTarget)Enum.Parse(typeof(BuildTarget), x)).ToArray();
        ContentCompiler.CompileAll();

        string buildDir = args[0];

        if (Directory.Exists(buildDir))
        {
            Directory.Delete(buildDir, true);
        }

        Directory.CreateDirectory(buildDir);

        foreach (var target in targets) {
            string dir = Path.Combine(buildDir, target.ToString());
            Directory.CreateDirectory(dir);

            BuildPlayerOptions options = new BuildPlayerOptions()
            {
                targetGroup = BuildTargetGroup.Standalone,
                target = target,
                scenes = buildScenes,
                options = BuildOptions.None,
                locationPathName = Path.Combine(dir, $"{buildPrefix}-{buildSuffixes[target]}{buildExtensions[target]}")
            };

            BuildPipeline.BuildPlayer(options);
            File.WriteAllText(Path.Combine(dir, "version.txt"), PlayerSettings.bundleVersion);
            File.WriteAllText(Path.Combine(dir, "channel.txt"), buildChannels[target]);
        }
    }   
}
