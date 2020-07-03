using Lomztein.BFA2.World;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Testing
{
    public class MapSerializationTest : MonoBehaviour
    {
        public static string Path = "*/Maps/";
        public static string SavePath = "Core/Maps/Test.json";

        public Text NameText;
        private int _selection = -1;

        private MapData[] _maps;
        private MapData Current => _maps[_selection];

        public void Cycle ()
        {
            _selection++;
            _selection %= _maps.Length;
            MapController.Instance.ApplyMapData(Current);
            NameText.text = Current.Name;
        }

        public void Awake()
        {
            _maps = Content.Content.GetAll(Path, typeof(MapData)).Cast<MapData>().ToArray();
            Cycle();
        }

        public void SaveCurrent ()
        {
            JToken token = MapController.Instance.SerializeMapData();
            File.WriteAllText(Application.streamingAssetsPath + "/" + SavePath, token.ToString());
        }
    }
}
