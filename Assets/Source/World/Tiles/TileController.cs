using Lomztein.BFA2.World.Tiles.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.World.Tiles
{
    public class TileController : MonoBehaviour
    {
        public static TileController Instance;

        public TileData TileData;
        public TileRenderer[] TileRenderers;

        private void Awake()
        {
            Instance = this;
            MapController controller = GameObject.FindGameObjectWithTag("MapController")?.GetComponent<MapController>();
            if (controller != null)
            {
                controller.OnMapDataLoaded += OnMapDataLoaded;
            }
        }

        private void OnMapDataLoaded(MapData obj)
        {
            TileData = obj.Tiles;
            RenderTiles();
        }

        private void Update()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(GetTile(mousePos).WallType);
        }

        private Vector2Int WorldToTileCoords (Vector2 position)
        {
            return new Vector2Int(
                Mathf.RoundToInt(position.x - 0.5f + TileData.Width / 2f),
                Mathf.RoundToInt(position.y - 0.5f + TileData.Height / 2f));
        }

        public void RenderTiles()
        {
            transform.position = -new Vector3(TileData.Width, TileData.Height) / 2f;
            foreach (TileRenderer renderer in TileRenderers)
            {
                renderer.RegenerateMesh(TileData);
            }
        }

        public TileTypeReference GetTile(Vector2 position)
        {
            Vector2Int pos = WorldToTileCoords(position);
            return TileData.GetTile(pos.x, pos.y);
        }

        public TileTypeReference[,] GetTiles (Vector2 from, Vector2 to)
        {
            Vector2Int f = WorldToTileCoords(from);
            Vector2Int t = WorldToTileCoords(to);

            return TileData.GetTiles(f, t);
        }

        public void SetTiles (Vector2 from, Vector2 to, TileType type)
        {
            Vector2Int f = WorldToTileCoords(from);
            Vector2Int t = WorldToTileCoords(to);

            TileData.SetTiles(f, t, type);
        }
    }
}
