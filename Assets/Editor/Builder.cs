using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [MenuItem("BFA2/Build")]
    public static void BuildGame ()
    {
        BuildGame(Directory.GetParent(Application.dataPath) + "\\Build\\", "StandaloneWindows", "StandaloneWindows64", "StandaloneLinux64", "StandaloneOSX");
    }

    public static void BuildGame(params string[] args)
    {
        string buildPrefix = "BFA2";
        BuildTarget[] targets = args.Skip(1).Select(x => (BuildTarget)Enum.Parse(typeof(BuildTarget), x)).ToArray();

        Dictionary<BuildTarget, string> buildSuffixes = new Dictionary<BuildTarget, string>()
        {
            { BuildTarget.StandaloneWindows, "Win32" },
            { BuildTarget.StandaloneWindows64, "Win64" },
            { BuildTarget.StandaloneLinux64, "Linux" },
            { BuildTarget.StandaloneOSX, "OSX" },
        };
        Dictionary<BuildTarget, string> buildExtensions = new Dictionary<BuildTarget, string>()
        {
            { BuildTarget.StandaloneWindows, ".exe" },
            { BuildTarget.StandaloneWindows64, ".exe" },
            { BuildTarget.StandaloneLinux64, ".x86" },
            { BuildTarget.StandaloneOSX, ".app" },
        };

        string[] buildScenes =
        {
            "Assets/Scenes/MainMenu.unity",
            "Assets/Scenes/Battlefield.unity",
            "Assets/Scenes/AssemblyEditor.unity",
            "Assets/Scenes/MapEditor.unity"
        };

        string buildDir = args[0];

        DateTime lastMinorRelease = new DateTime(2020, 7, 16);

        string patch = (DateTime.Now - lastMinorRelease).Days.ToString();
        string build = (DateTime.Now.Second + DateTime.Now.Minute * 60 + DateTime.Now.Hour * 3600).ToString();

        PlayerSettings.bundleVersion = $"0.1.{patch}.{build}";

        if (Directory.Exists(buildDir))
        {
            Directory.Delete(buildDir, true);
        }

        Directory.CreateDirectory(buildDir);
        File.WriteAllText(Path.Combine(buildDir, "version.txt"), PlayerSettings.bundleVersion);

        foreach (var target in targets) {
            string dir = Path.Combine(buildDir, buildSuffixes[target]);
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
        }
    }   
}
