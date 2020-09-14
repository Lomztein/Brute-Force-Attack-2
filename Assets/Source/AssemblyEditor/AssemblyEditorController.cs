using Lomztein.BFA2.Serialization.Assemblers.Turret;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models.Turret;
using Lomztein.BFA2.Serialization.Serializers.Turret;
using Lomztein.BFA2.Turrets;
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

        public ITurretAssembly CurrentAsssembly;

        public void Awake()
        {
            Instance = this;
        }

        public void DeleteAssembly ()
        {
            Destroy((CurrentAsssembly as Component).gameObject);
            CurrentAsssembly = null;
        }

        public void SetAssembly (ITurretAssembly assembly)
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
            GameObjectTurretAssemblyAssembler assembler = new GameObjectTurretAssemblyAssembler();
            CurrentAsssembly.Name = name;
            var model = assembler.Disassemble(CurrentAsssembly);

            TurretAssemblyModelSerializer turretSerializer = new TurretAssemblyModelSerializer();
            var json = turretSerializer.Serialize(model);
            File.WriteAllText(path, json.ToString());
        }

        private void LoadFile(string path)
        {
            GameObjectTurretAssemblyAssembler assembler = new GameObjectTurretAssemblyAssembler();
            var json = JObject.Parse(File.ReadAllText(path));

            TurretAssemblyModelSerializer turretSerializer = new TurretAssemblyModelSerializer();
            ITurretAssemblyModel model = turretSerializer.Deserialize(json);

            CurrentAsssembly = assembler.Assemble(model);

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
