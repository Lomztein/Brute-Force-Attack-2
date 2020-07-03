using Lomztein.BFA2.Serialization.Models.GameObject;
using Lomztein.BFA2.World.Tiles;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.World
{
    public class MapData : ISerializable
    {
        public string Name;
        public string Description;

        public int Width;
        public int Height;

        public TileTypeReference[,] Walls;
        public IGameObjectModel[] Objects = new IGameObjectModel[0];

        public MapData (string name, string desc, int width, int height)
        {
            Name = name;
            Description = desc;
            Width = width;
            Height = height;

            ResetWalls(TileType.Empty);
        }

        public MapData () { }

        private Vector2Int[] _nodeNeighbours = new Vector2Int[]
        {
            new Vector2Int(1, 0),
            new Vector2Int(1, -1),
            new Vector2Int(0, -1),
            new Vector2Int(-1, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(-1, 1),
            new Vector2Int(0, 1),
            new Vector2Int(1, 1),
        };

        public void ResetWalls (TileType type)
        {
            // Bit strange but live with it.
            Walls = TwoDifyWalls(new TileTypeReference[Width * Height].Select(x => new TileTypeReference(type.Name)).ToArray());
        }

        public Graph GenerateGraph ()
        {
            List<Graph.Node> nodes = new List<Graph.Node>();
            List<Graph.Edge> edges = new List<Graph.Edge>();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    nodes.Add(new Graph.Node(new Vector2Int(x, y)));
                    foreach (Vector2Int neighbour in _nodeNeighbours)
                    {
                        int xx = x + neighbour.x;
                        int yy = y + neighbour.y;

                        if (IsInsideMap(xx, yy))
                        {
                            edges.Add(new Graph.Edge(CoordsToIndex(x, y), CoordsToIndex(xx, yy)));
                        }
                    }
                }
            }

            return new Graph(nodes.ToArray(), edges.ToArray());
        }

        private int CoordsToIndex(int x, int y) => y * Width + x;
        private (int x, int y) IndexToCoords(int index)
        {
            int x = index % Width;
            int y = Mathf.FloorToInt((float)index / Width);
            return (x, y);
        }

        public bool IsInsideMap (int x, int y)
        {
            if (x < 0 || x > Width - 1)
                return false;
            if (y < 0 || y > Height - 1)
                return false;
            return true;
        }

        private TileTypeReference[] EnflattenWalls(TileTypeReference[,] walls)
        {
            TileTypeReference[] flattened = new TileTypeReference[Width * Height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    flattened[CoordsToIndex(x, y)] = walls[x, y];
                }
            }
            return flattened;
        }

        private TileTypeReference[,] TwoDifyWalls (TileTypeReference[] walls)
        {
            TileTypeReference[,] twodi = new TileTypeReference[Width, Height];
            for (int i = 0; i < walls.Length; i++)
            {
                (int x, int y) = IndexToCoords(i);
                twodi[x, y] = walls[i];
            }
            return twodi;
        }

        public JToken Serialize()
        {
            return new JObject()
            {
                {"Name", Name },
                {"Description", Description },
                {"Width", Width },
                {"Height", Height },
                {"Walls", new JArray(EnflattenWalls(Walls).Select(x => x.Serialize()).ToArray()) },
                {"Objects", new JArray(Objects.Select(x => x.Serialize()).ToArray()) }
            };
        }

        public void Deserialize(JToken source)
        {
            Name = source["Name"].ToObject<string>();
            Description = source["Description"].ToObject<string>();
            Width = source["Width"].ToObject<int>();
            Height = source["Height"].ToObject<int>();
            Walls = TwoDifyWalls((source["Walls"] as JArray).Select(x => DeserializeWallTypeReference(x)).ToArray());
            Objects = (source["Objects"] as JArray).Select(x => DeserializeGameObjectModel(x)).ToArray();
        }

        private TileTypeReference DeserializeWallTypeReference (JToken token)
        {
            TileTypeReference reference = new TileTypeReference();
            reference.Deserialize(token);
            return reference;
        }

        private IGameObjectModel DeserializeGameObjectModel (JToken token)
        {
            GameObjectModel model = new GameObjectModel();
            model.Deserialize(token);
            return model;
        }
    }
}