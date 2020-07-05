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

        private MapData _mapData;
        public Graph MapGraph;
        private List<GameObject> _mapObjects;

        public event Action<MapData> OnMapDataLoaded;

        public void Awake()
        {
            Instance = this;
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
        }



        public JToken SerializeMapData() => _mapData.Serialize();
    }
}
