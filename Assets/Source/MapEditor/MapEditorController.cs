using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models.GameObject;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.World;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.MapEditor
{
    public class MapEditorController : MonoBehaviour
    {
        private LooseDependancy<MapController> _mapController = new LooseDependancy<MapController>();

        public MapData MapData;

        public void OpenSaveDialog ()
        {
            SaveFileDialog.Create(Application.streamingAssetsPath + "/Content/Custom/Maps", ".json", SaveFile);
        }

        public void OpenLoadFileBrowser ()
        {
            FileBrowser.Create(Application.streamingAssetsPath + "/Content/Custom/Maps", ".json", LoadFile);
        }

        public void CreateNewMap (int width, int height)
        {
            MapData = new MapData(string.Empty, string.Empty, width, height);
            _mapController.IfExists(x => x.ApplyMapData(MapData));
        }

        private void LoadFile (string file)
        {
            JObject obj = JObject.Parse(File.ReadAllText(file));
            MapData = new MapData();
            MapData.Deserialize(obj);

            _mapController.IfExists(x => x.ApplyMapData(MapData));
        }

        private void SaveFile (string name, string path)
        {
            MapData.Name = name;
            MapData.Objects = AssembleMapObjects();
            JToken token = MapData.Serialize();
            File.WriteAllText(path, token.ToString());
        }

        private IGameObjectModel[] AssembleMapObjects ()
        {
            List<IGameObjectModel> models = new List<IGameObjectModel>();
            GameObjectAssembler assembler = new GameObjectAssembler();

            foreach (Transform child in _mapController.Dependancy.MapObjectParent)
            {
                models.Add(assembler.Disassemble(child.gameObject));
            }

            return models.ToArray();
        }
    }
}
