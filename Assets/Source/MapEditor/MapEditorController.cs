using Lomztein.BFA2.MapEditor.Objects;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models.GameObject;
using Lomztein.BFA2.UI.Windows;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.World;
using Lomztein.BFA2.World.Tiles;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Lomztein.BFA2.MapEditor
{
    public class MapEditorController : MonoBehaviour
    {
        private LooseDependancy<MapController> _mapController = new LooseDependancy<MapController>();

        public static MapEditorController Instance;
        public MapObjectHandleProvider HandleProvider;
        
        public GameObject MapResizer;
        public int DefaultWidth;
        public int DefaultHeight;

        public MapData MapData;

        public void OpenSaveDialog ()
        {
            SaveFileDialog.Create(Application.streamingAssetsPath + "/Content/Custom/Maps", ".json", SaveFile);
        }

        public void OpenLoadFileBrowser ()
        {
            FileBrowser.Create(Application.streamingAssetsPath + "/Content/Custom/Maps", ".json", LoadFile);
        }

        public void OpenMapResizer ()
        {
            MapResizer resizer = WindowManager.OpenWindowAboveOverlay(MapResizer).GetComponent<MapResizer>();
            resizer.Init(DefaultWidth, DefaultHeight, SetMapSize);
        }

        private void Awake()
        {
            Instance = this;
        }

        private void SetMapSize (int width, int height)
        {
            MapData.Width = width;
            MapData.Height = height;
            ResizeTiles(width, height);
            ApplyMapData();
        }

        private void Start()
        {
            CreateNewMap();
            _mapController.IfExists(x => x.OnMapDataLoaded += OnMapDataLoaded);
        }

        private void OnMapDataLoaded(MapData obj)
        {
            HandleProvider.ClearHandles();

            foreach (Transform child in _mapController.Dependancy.MapObjectParent)
            {
                MapObjectHandle handle = HandleProvider.GetHandle(child.gameObject);
                handle.transform.position = child.position;
                handle.transform.rotation = child.rotation;

                handle.Assign(child.gameObject);
            }
        }

        public void ResizeTiles(int width, int height)
        {
            TileTypeReference[,] old = MapData.Tiles.Tiles;
            int ow = MapData.Tiles.Width;
            int oh = MapData.Tiles.Height;

            MapData.Tiles.Tiles = new TileTypeReference[width, height];
            MapData.Tiles.Width = width;
            MapData.Tiles.Height = height;

            int dx = width - ow;
            int dy = height - oh;

            int xo = dx / 2;
            int yo = dy / 2;

            for (int y = 0; y < oh; y++)
            {
                for (int x = 0; x < ow; x++)
                {
                    int xx = x + xo;
                    int yy = y + yo;

                    if (MapUtils.IsInsideMap(xx, yy, width, height) && MapUtils.IsInsideMap(x, y, ow, oh))
                    {
                        MapData.Tiles.SetTile(xx, yy, new TileType(old[x, y].WallType));
                    }
                }
            }

            MapData.Tiles.Width = width;
            MapData.Tiles.Height = height;
        }

        public void AddMapObject(GameObject obj) => _mapController.IfExists(x => obj.transform.SetParent(x.MapObjectParent));

        public void CreateNewMap ()
        {
            MapData = new MapData();
            OpenMapResizer();
            ApplyMapData();
        }

        private void ApplyMapData ()
        {
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
