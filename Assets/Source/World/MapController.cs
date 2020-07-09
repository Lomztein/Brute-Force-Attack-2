using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.World.Tiles;
using Lomztein.BFA2.World.Tiles.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.World
{
    public class MapController : MonoBehaviour
    {
        public static MapController Instance;
        public Transform BackgroundQuad;
        public Transform MapObjectParent;

        private MapData _mapData;
        public Graph MapGraph;
        private List<GameObject> _mapObjects = new List<GameObject>();

        public event Action<MapData> OnMapDataLoaded;

        public int Width => _mapData.Width;
        public int Height => _mapData.Height;

        public void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            ApplyMapData(Content.Content.Get("Core/Maps/Stormfront.json", typeof (MapData)) as MapData);
        }

        public void ApplyMapData (MapData mapData)
        {
            _mapData = mapData;
            MapGraph = _mapData.GenerateGraph();

            ApplyMapToBackground();
            InstantiateMapObjects();

            OnMapDataLoaded?.Invoke(_mapData);
        }

        private void ApplyMapToBackground ()
        {
            BackgroundQuad.localScale = new Vector3(_mapData.Width, _mapData.Height, 1);
            Material material = BackgroundQuad.GetComponent<Renderer>().sharedMaterial;

            material.mainTextureScale = new Vector2(_mapData.Width, _mapData.Height) / 2;
        }

        private void InstantiateMapObjects ()
        {
            IGameObjectAssembler assembler = new GameObjectAssembler();
            foreach (var obj in _mapData.Objects)
            {
                _mapObjects.Add(assembler.Assemble(obj));
            }

            foreach (var obj in _mapObjects)
            {
                obj.transform.SetParent(MapObjectParent, true);
                obj.BroadcastMessage("OnMapObjectAssembled", SendMessageOptions.DontRequireReceiver);
            }
        }

        public bool InInsideMap (Vector2 position)
        {
            if (position.x < -_mapData.Width / 2f || position.x > _mapData.Width / 2f)
                return false;
            if (position.y < -_mapData.Height / 2f || position.y > _mapData.Height / 2f)
                return false;
            return true;
        }

        public Vector2 ClampToMap (Vector2 position)
        {
            return new Vector2(
                Mathf.Clamp(position.x, -_mapData.Width / 2f, _mapData.Width / 2f),
                Mathf.Clamp(position.y, -_mapData.Height / 2f, _mapData.Height / 2f)
                );
        }

        public JToken SerializeMapData() => _mapData.Serialize();
    }
}
