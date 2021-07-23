using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using static Lomztein.BFA2.Serialization.Assemblers.ObjectPopulator;

namespace Lomztein.BFA2.Serialization.Assemblers.PropertyAssembler
{
    public class AssetReferencePropertyAssembler : PropertyAssemblerBase
    {
        public override Type AttributeType => typeof(ModelAssetReferenceAttribute);
        private Dictionary<UnityEngine.Object, string> _memoryReferences = new Dictionary<UnityEngine.Object, string>();
        // This is probably a hack but should work well enough. The intend is for this to store the objects when outside the Editor, for when cloning.
        // Be on the lookout for potential memory leaks.

        private string[] _assetPaths = new string[]
        {
            "Assets/Content/",
        };
        private string _resourcesPath = "Assets/Resources/";

        private Dictionary<string, string> _assetToContentFileExtenisons = new Dictionary<string, string>()
        {
            { ".prefab", ".json" },
            { ".asset", ".json" },
        };

        private string GetAssetToContentFileExtension(string extension)
            => _assetToContentFileExtenisons.TryGetValue(extension, out string newExtension) ? newExtension : extension;

        public override void Assemble(object obj, IAssignableMemberInfo member, ValueModel model, Type expectedType, AssemblyContext context)
        {
            if (!ValueModel.IsNull(model))
            {
                if (model is PathModel pathModel)
                {
                    UnityEngine.Object value = (UnityEngine.Object)SerializationFileAccess.LoadObjectFromFile(pathModel.Path, expectedType);

                    if (!_memoryReferences.ContainsKey(value))
                        _memoryReferences.Add(value, pathModel.Path);

                    member.SetValue(obj, value);
                    return;
                }
                throw new InvalidOperationException(nameof(model) + " must be a " + nameof(PathModel));
            }
        }

        public override void Disassemble(ObjectField field, object obj, Type expectedType, DisassemblyContext context)
        {
            if (obj == null)
            {
                field.Model = new NullModel();
                return;
            } else if (obj is UnityEngine.Object uObj)
            {
#if UNITY_EDITOR 
                // This code cannot function without the UnityEditor assemblies.
                string assetPath = AssetDatabase.GetAssetPath(uObj);
#else
                string assetPath = _memoryReferences[uObj];
                Debug.LogWarning("Cloning objects with an AssetReference is discouraged. If possible, make that object a ScriptableObject and clone using Instantiate instead.");
#endif
                foreach (string path in _assetPaths)
                {
                    if (assetPath.StartsWith(path))
                    {
                        field.Model = new PathModel(Path.ChangeExtension(assetPath.Substring(path.Length), GetAssetToContentFileExtension(Path.GetExtension(assetPath))));
                        return;
                    }
                }
                if (assetPath.StartsWith(_resourcesPath))
                {
                    field.Model = new PathModel(Path.ChangeExtension(assetPath.Substring(7), null)); // Hardcoded length for "Assets/" so that only the part with and after Resources remains. Is there an easier way to do this?
                }

                
            }
            else
            {
                throw new InvalidOperationException("ModelAssetReference attribute is only applicable to UnityEngine.Object based objects.");
            }
        }
    }
}
