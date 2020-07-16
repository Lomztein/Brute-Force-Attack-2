using Lomztein.BFA2.Serialization.Models.GameObject;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.World.Tiles;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.World
{
    [System.Serializable]
    public class MapData : ISerializable
    {
        public string Name;
        public string Description;

        public int Width;
        public int Height;

        public TileData Tiles;
        public IGameObjectModel[] Objects = new IGameObjectModel[0];

        public MapData ()
        {
            Tiles = new TileData(Width, Height);
        }

        public MapData (string name, string desc, int width, int height)
        {
            Name = name;
            Description = desc;
            Width = width;
            Height = height;
            Tiles = new TileData(Width, Height);
        }

        private Vector2Int[] _nodeNeighbours = new Vector2Int[]
        {
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1),
            //new Vector2Int(-1, 1),
            //new Vector2Int(-1, -1),
            //new Vector2Int(1, 1),
            //new Vector2Int(1, -1),

        };

        public void ResetWalls (TileType type)
        {
            // Bit strange but live with it.
        }

        public Graph GenerateGraph ()
        {
            List<Graph.Node> nodes = new List<Graph.Node>();
            List<Graph.Edge> edges = new List<Graph.Edge>();
            Graph.Node[,] nodeMap = new Graph.Node[Width, Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Graph.Node node = new Graph.Node(new Vector3(x, y));
                    nodes.Add(node);
                    nodeMap[x, y] = node;

                    foreach (Vector2Int neighbour in _nodeNeighbours)
                    {
                        int xx = x + neighbour.x;
                        int yy = y + neighbour.y;

                        if (MapUtils.IsInsideMap(xx, yy, Width, Height))
                        {
                            edges.Add(new Graph.Edge(MapUtils.CoordsToIndex(x, y, Width), MapUtils.CoordsToIndex(xx, yy, Width)));
                        }
                    }
                }
            }

            return new Graph(nodes.ToArray(), edges.ToArray(), new TileGraphMap(Width, Height, nodeMap));
        }




        

        public JToken Serialize()
        {
            return new JObject()
            {
                {"Name", Name },
                {"Description", Description },
                {"Width", Width },
                {"Height", Height },
                {"Tiles", Tiles.Serialize() },
                {"Objects", new JArray(Objects.Select(x => x.Serialize()).ToArray()) }
            };
        }

        public void Deserialize(JToken source)
        {
            Name = source["Name"].ToObject<string>();
            Description = source["Description"].ToObject<string>();
            Width = source["Width"].ToObject<int>();
            Height = source["Height"].ToObject<int>();
            Tiles = DeserializeTileData(source["Tiles"]);
            Objects = (source["Objects"] as JArray).Select(x => DeserializeGameObjectModel(x)).ToArray();
        }

        private TileData DeserializeTileData (JToken token)
        {
            TileData data = new TileData(Width, Height);
            data.Deserialize(token);
            return data;
        }

        private IGameObjectModel DeserializeGameObjectModel (JToken token)
        {
            GameObjectModel model = new GameObjectModel();
            model.Deserialize(token);
            return model;
        }
    }
}