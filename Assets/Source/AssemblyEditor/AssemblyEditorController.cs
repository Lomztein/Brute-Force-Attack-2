using Lomztein.BFA2.Content.Assemblers;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Structures.Turrets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.AssemblyEditor
{
    public class AssemblyEditorController : MonoBehaviour
    {
        public static AssemblyEditorController Instance;

        public TurretAssembly CurrentAsssembly;

        public void Awake()
        {
            Instance = this;
        }

        public void DeleteAssembly ()
        {
            Destroy((CurrentAsssembly as Component).gameObject);
            CurrentAsssembly = null;
        }

        public void SetAssembly (TurretAssembly assembly)
        {
            CurrentAsssembly = assembly;
        }

        public void Update()
        {
            if (CurrentAsssembly != null && CurrentAsssembly.GetRootComponent() == null)
            {
                DeleteAssembly();
            }
        }

        public void Save ()
        {
            SaveFileDialog.Create(Application.streamingAssetsPath + "/Content/Custom/Assemblies", ".json", SaveFile);
        }

        private void SaveFile (string name, string path)
        {
            TurretAssemblyAssembler assembler = new TurretAssemblyAssembler();
            CurrentAsssembly.Name = name;
            var model = assembler.Disassemble(CurrentAsssembly);
            File.WriteAllText(path, ObjectPipeline.SerializeObject(model).ToString());
        }

        private void LoadFile(string path)
        {
            var json = JObject.Parse(File.ReadAllText(path));
            var model = ObjectPipeline.DeserializeObject(json);

            TurretAssemblyAssembler assembler = new TurretAssemblyAssembler();
            CurrentAsssembly = assembler.Assemble(model as ObjectModel);

            if (CurrentAsssembly is Component comp)
            {
                comp.transform.position = Vector3.zero;
                comp.transform.rotation = Quaternion.identity;
            }
        }

        public void Load ()
        {
            FileBrowser.Create(Application.streamingAssetsPath + "/Content/Custom/Assemblies", ".json", LoadFile);
        }
    }
}
