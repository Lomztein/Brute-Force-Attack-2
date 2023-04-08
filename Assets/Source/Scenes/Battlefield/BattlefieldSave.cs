using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.World;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.Battlefield
{
    public class BattlefieldSave : IAssemblable
    {
        public static string PATH_ROOT => Path.Combine(Application.persistentDataPath, "Saves");

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

        public static JObject ToJSON (BattlefieldSave save)
        {
            var json = ObjectPipeline.UnbuildObject(save);
            return json as JObject;
        }

        public static void LoadToBattlefield(BattlefieldSave save, BattlefieldController controller)
        {
            controller.InitializeBattlefield(save.Settings);

            BattlefieldAssembler assembler = new BattlefieldAssembler();
            assembler.Assemble(controller, save.BattlefieldData, new AssemblyContext());
        }

        public static BattlefieldSave SaveFromBattlefield(BattlefieldController controller)
        {
            BattlefieldAssembler assembler = new BattlefieldAssembler();
            var settings = controller.CurrentSettings;

            ObjectModel data = assembler.Disassemble(controller, new DisassemblyContext());
            return new BattlefieldSave(settings, data);
        }

        public static void SaveCurrentToFile(string path)
        {
            var save = SaveFromBattlefield(BattlefieldController.Instance);
            var json = ToJSON(save);
            json[FileBrowser.FILE_DESCRIPTION] = GetSaveDescription();
            json[FileBrowser.FILE_IMAGE] = TakeScreenshot().ToBase64();
            File.WriteAllText(path, json.ToString());
        }

        private static Texture2D TakeScreenshot()
        {
            MapData data = BattlefieldController.Instance.MapController.MapData;
            Rect screenRect = new Rect(
                -data.Width / 2f,
                -data.Height / 2f,
                data.Width,
                data.Height
                );
            return CameraCapture.CaptureOrtho(screenRect, new Vector2Int(128, 128));
        }

        private static string GetSaveDescription()
        {
            var settings = BattlefieldController.Instance.CurrentSettings;
            return $"Map: {BattlefieldController.Instance.MapController.MapData.Name}" +
                $"\nDifficulty: {settings.Difficulty.Name}" +
                $"\nWave: {RoundController.Instance.NextIndex}";
        }
    }
}
