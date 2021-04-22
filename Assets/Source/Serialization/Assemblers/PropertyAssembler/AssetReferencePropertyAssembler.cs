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
                    var value = SerializationFileAccess.LoadObjectFromFile(pathModel.Path, expectedType);
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
            } else if (obj is UnityEngine.Object uObj)
            {
                string assetPath = AssetDatabase.GetAssetPath(uObj);
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
