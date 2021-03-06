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
                    CompilationTypeReference compileType = folderCompilationType.ContainsKey(relativeFolder) ? folderCompilationType[relativeFolder] : null;
                    if (compileType == null)
                    {
                        compileType = ScriptableObject.CreateInstance<CompilationTypeReference>();
                    }

                    CompileAsset(asset, Path.Combine(targetPath, relativeFolder, Path.GetFileName(path)), compileType.CompilationType, !compileType.WithExplicitType);
                }
            }
        }

        public static void CompileAsset(string guid, string targetPath, CompilationType type, bool implicitType)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            string directory = Directory.GetParent(targetPath).FullName;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

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
                    RootModel model = assembler.Disassemble(asset as GameObject);
                    json = ObjectPipeline.SerializeObject(model);
                    break;

                case CompilationType.SerializeAssembly:
                    TurretAssemblyAssembler tAssembler = new TurretAssemblyAssembler();
                    RootModel tModel = tAssembler.Disassemble((asset as GameObject).GetComponent<TurretAssembly>());
                    json = ObjectPipeline.SerializeObject(tModel);
                    break;

                case CompilationType.SerializeObject:

                default:
                    json = ObjectPipeline.UnbuildObject(asset, implicitType);
                    break;
            }

            File.WriteAllText(Path.ChangeExtension (targetPath, ".json"), json.ToString());
        }
    }
}
