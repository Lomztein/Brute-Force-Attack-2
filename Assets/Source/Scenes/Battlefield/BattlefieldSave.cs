using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.Battlefield
{
    public class BattlefieldSave : IAssemblable
    {
        public BattlefieldSettings Settings;
        public ObjectModel BattlefieldData;

        public BattlefieldSave(BattlefieldSettings settings, ObjectModel battlefieldData)
        {
            Settings = settings;
            BattlefieldData = battlefieldData;
        }

        public BattlefieldSave() { }

        public ValueModel Disassemble(DisassemblyContext context)
        {
            return new ObjectModel(
                new ObjectField("Settings", ValueModelFactory.Create(Settings, context)),
                new ObjectField("Data", BattlefieldData));
        }

        public void Assemble(ValueModel source, AssemblyContext context)
        {
            ObjectModel model = source as ObjectModel;
            ObjectAssembler assembler = new ObjectAssembler();
            Settings = assembler.Assemble(model.GetProperty("Settings"), typeof(BattlefieldSettings), context) as BattlefieldSettings;
            BattlefieldData = model.GetObject("Data");
        }

        public static BattlefieldSave LoadFromFile (string path)
        {
            var json = JObject.Parse(File.ReadAllText(path));
            return ObjectPipeline.BuildObject<BattlefieldSave>(json);
        }

        public static void SaveToFile (BattlefieldSave save, string path)
        {
            var json = ObjectPipeline.UnbuildObject(save);
            File.WriteAllText(path, json.ToString());
        }

        public static void LoadToBattlefield(BattlefieldSave save, BattlefieldController controller)
        {
            controller.InitializeBattlefield(save.Settings);
            // Load ALL THE DATA

        }

        public static BattlefieldSave SaveFromBattlefield(BattlefieldController controller, BattlefieldSettings settings)
        {
            // Save ALL THE DATA.

            ObjectModel data = new ObjectModel();
            return new BattlefieldSave(settings, data);
        }
    }
}
