using Lomztein.BFA2.ContentSystem.Assemblers;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Structures.Turrets;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Lomztein.BFA2.Editor.Content
{
    public static class ContentCompiler
    {
        public enum CompilationType { Copy, SerializeObject, SerializeGameObject, SerializeAssembly }
        private static string RootContentFolder => "Assets/Content";
        private static string RootTargetContentFolder => Path.Combine(Application.streamingAssetsPath, "Content");

        public const string CompilationTypeReferenceFileName = "CompilationType.asset";

        [MenuItem("BFA2/Build Content")]
        public static void CompileAll ()
        {
            string[] folders = Directory.GetDirectories(RootContentFolder);
            foreach (string folder in folders)
            {
                string folderName = Path.GetFileName(folder);
                CompileFolder(folder, Path.Combine(RootTargetContentFolder, folderName), true);
            }
        }

        public static void CompileFolder (string folderPath, string targetPath, bool recursive)
        {
            string[] compilationTypeSearch = AssetDatabase.FindAssets("t:CompilationTypeReference", new string[] { folderPath });
            string[] compilationTypeFiles = compilationTypeSearch.Select(x => AssetDatabase.GUIDToAssetPath(x)).ToArray();
            Dictionary<string, CompilationTypeReference> folderCompilationType = compilationTypeFiles.ToDictionary(x => Path.GetDirectoryName(x.Substring(folderPath.Length + 1)), y => AssetDatabase.LoadAssetAtPath<CompilationTypeReference>(y));

            IEnumerable<string> assets = AssetDatabase.FindAssets(string.Empty, new string[] { folderPath }).Where(x => !compilationTypeSearch.Contains(x)).ToArray();
            foreach (string asset in assets)
            {
                string path = AssetDatabase.GUIDToAssetPath(asset);
                string relativeFolder = Path.GetDirectoryName(path.Substring(folderPath.Length + 1));

                if (!AssetDatabase.IsValidFolder(path))
                {
                    CompilationType type = folderCompilationType.ContainsKey(relativeFolder) ? folderCompilationType[relativeFolder].CompilationType : CompilationType.Copy;
                    CompileAsset(asset, Path.Combine(targetPath, relativeFolder, Path.GetFileName(path)), type);
                }
            }
        }

        public static void CompileAsset(string guid, string targetPath, CompilationType type)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            if (type == CompilationType.Copy)
            {
                File.Copy(assetPath, targetPath, true);
                return;
            }

            object asset = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof (Object));
            JToken json;

            switch (type)
            {
                case CompilationType.SerializeGameObject:
                    GameObjectAssembler assembler = new GameObjectAssembler();
                    ValueModel model = assembler.Disassemble(asset as GameObject);
                    json = ObjectPipeline.SerializeObject(model as ObjectModel);
                    break;

                case CompilationType.SerializeAssembly:
                    TurretAssemblyAssembler tAssembler = new TurretAssemblyAssembler();
                    ValueModel tModel = tAssembler.Disassemble((asset as GameObject).GetComponent<TurretAssembly>());
                    json = ObjectPipeline.SerializeObject(tModel as ObjectModel);
                    break;

                case CompilationType.SerializeObject:

                default:
                    json = ObjectPipeline.UnbuildObject(asset, true);
                    break;
            }

            File.WriteAllText(Path.ChangeExtension (targetPath, ".json"), json.ToString());
        }
    }
}
