using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.ContentSystem.Assemblers;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.MapEditor.Objects;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Serializers;
using Lomztein.BFA2.UI;
using Lomztein.BFA2.UI.ToolTip;
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
using UnityEngine.UI;

namespace Lomztein.BFA2.MapEditor
{
    public class MapEditorController : MonoBehaviour
    {
        private const int PREVIEW_RENDER_SIZE = 1024;

        private LooseDependancy<MapController> _mapController = new LooseDependancy<MapController>();

        public static MapEditorController Instance;
        public bool GridSnapEnabled;
        public Toggle GridSnapToggle;

        public MapObjectHandleProvider HandleProvider;
        public ComponentHandleProvider ComponentHandleProvider;
        
        public GameObject MapResizer;
        public int DefaultWidth;
        public int DefaultHeight;

        public MapData MapData;
        public GameObject MapParent;

        public InputField NameInput;
        public InputField DescriptionInput;

        public event Action<MapData, string> OnMapSaved;
        public event Action<MapData, string> OnMapLoaded;

        public void OpenSaveDialog ()
        {
            MapData.Name = NameInput.text;
            MapData.Description = DescriptionInput.text;

            SaveFileDialog.Create(Path.Combine(Content.UserContentPackPath, "Maps"), ".json", (name, path) =>
            {
                SaveMapFile(path);
            });
        }

        private void SaveMapFile(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            MapData.Name = NameInput.text;
            MapData.Description = DescriptionInput.text;
            MapData.Identifier = Guid.NewGuid().ToString();
            LazyTileFix(MapData.Tiles); // Lazy fix, probably should investigate why it happens. Shouldn't really matter though.

            // These paths are so hardcoded, severely needs to be improved if we are to ever have variable save destinations.
            Texture2D mapPreview = SnapMapPreview();
            var previewBase64 = mapPreview.ToBase64();
            MapData.Preview = mapPreview;

            MapData.Objects = DisassembleMapObjects();
            var model = MapData.Disassemble(new Serialization.Assemblers.DisassemblyContext()) as ObjectModel;

            ValueModelSerializer serializer = new ValueModelSerializer();
            var json = serializer.Serialize(model);
            json[FileBrowser.FILE_NAME] = MapData.Name;
            json[FileBrowser.FILE_DESCRIPTION] = MapData.Description;
            json[FileBrowser.FILE_IMAGE] = mapPreview.ToBase64();

            File.WriteAllText(path, json.ToString());
            Content.ResetIndex();

            OnMapSaved?.Invoke(MapData, path);
            Alert.Open("Map succesfully saved.");
        }

        public void ToggleGridSnap ()
        {
            GridSnapEnabled = GridSnapToggle.isOn;
        }

        private Texture2D SnapMapPreview()
            => Iconography.GenerateIcon(MapParent, PREVIEW_RENDER_SIZE);

        public void OpenLoadFileBrowser ()
        {
            Confirm.Open("Loading a map will delete any currently unsaved progress.\nConfirm?", () =>
            {
                FileBrowser.Create("Select Map File", Path.Combine(Content.UserContentPackPath, "Maps"), ".json", LoadFile);
            });
        }

        public void OpenMapResizer ()
        {
            MapResizer resizer = WindowManager.OpenWindowAboveOverlay(MapResizer).GetComponent<MapResizer>();
            resizer.Init(DefaultWidth, DefaultHeight, SetMapSize);
        }

        public void OnResizeMapButton ()
        {
            OpenMapResizer();
        }

        public void OnCreateNewMapButton()
        {
            Confirm.Open("Creating a new map will delete any currently unsaved progress.\nConfirm?", () =>
            {
                CreateNewMap();
            });
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

                handle.Assign(child.gameObject, ComponentHandleProvider.GetHandles(child.gameObject));
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
                        MapData.Tiles.SetTile(xx, yy, new TileType(old[x, y].TileType));
                    }
                }
            }

            MapData.Tiles.Width = width;
            MapData.Tiles.Height = height;
        }

        private void LazyTileFix (TileData data)
        {
            for (int x = 0; x < data.Width; x++)
            {
                for (int y = 0; y < data.Height; y++)
                {
                    if (string.IsNullOrEmpty(data.GetTile(x, y).TileType))
                    {
                        data.SetTile(x, y, TileType.Empty);
                    }
                }
            }
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
            NameInput.text = MapData.Name;
            DescriptionInput.text = MapData.Description;
        }

        private void LoadFile (string file)
        {
            JObject obj = JObject.Parse(File.ReadAllText(file));
            MapData = ObjectPipeline.BuildObject<MapData>(obj);

            _mapController.IfExists(x => x.ApplyMapData(MapData));

            NameInput.text = MapData.Name;
            DescriptionInput.text = MapData.Description;

            OnMapLoaded?.Invoke(MapData, file);
        }

        private ObjectModel[] DisassembleMapObjects ()
        {
            List<ObjectModel> models = new List<ObjectModel>();
            GameObjectAssembler assembler = new GameObjectAssembler();

            foreach (Transform child in _mapController.Dependancy.MapObjectParent)
            {
                models.Add(assembler.Disassemble(child.gameObject).Root as ObjectModel);
            }

            return models.ToArray();
        }
    }
}
