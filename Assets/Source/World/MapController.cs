using Lomztein.BFA2.ContentSystem.Assemblers;
using Lomztein.BFA2.Serialization.Models;
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

        public MapData MapData { get; private set; }
        public Graph MapGraph { get; private set; }
        private List<GameObject> _mapObjects = new List<GameObject>();

        public event Action<MapData> OnMapDataLoaded;

        public int Width => MapData.Width;
        public int Height => MapData.Height;

        public void Awake()
        {
            Instance = this;
        }

        public void ApplyMapData (MapData mapData)
        {
            MapData = mapData;
            MapGraph = MapData.GenerateGraph();

            ApplyMapToBackground();
            InstantiateMapObjects();

            OnMapDataLoaded?.Invoke(MapData);
        }

        private void ApplyMapToBackground ()
        {
            BackgroundQuad.localScale = new Vector3(MapData.Width, MapData.Height, 1);
            Material material = BackgroundQuad.GetComponent<Renderer>().sharedMaterial;

            material.mainTextureScale = new Vector2(MapData.Width, MapData.Height) / 2;
        }

        private void InstantiateMapObjects ()
        {
            // Clear for previous map.
            foreach (GameObject obj in _mapObjects)
            {
                Destroy(obj);
            }
            _mapObjects.Clear();

            // Assembly, duh.
            GameObjectAssembler assembler = new GameObjectAssembler();
            foreach (var obj in MapData.Objects)
            {
                _mapObjects.Add(assembler.Assemble(new RootModel (obj)));
            }

            // Assign to correct parent.
            foreach (var obj in _mapObjects)
            {
                obj.transform.SetParent(MapObjectParent, true);
                obj.BroadcastMessage("OnMapObjectAssembled", SendMessageOptions.DontRequireReceiver);
            }
        }

        public bool InInsideMap (Vector2 position)
        {
            if (position.x < -MapData.Width / 2f || position.x > MapData.Width / 2f)
                return false;
            if (position.y < -MapData.Height / 2f || position.y > MapData.Height / 2f)
                return false;
            return true;
        }

        public Vector2 ClampToMap (Vector2 position)
        {
            return new Vector2(
                Mathf.Clamp(position.x, -MapData.Width / 2f, MapData.Width / 2f),
                Mathf.Clamp(position.y, -MapData.Height / 2f, MapData.Height / 2f)
                );
        }
    }
}
