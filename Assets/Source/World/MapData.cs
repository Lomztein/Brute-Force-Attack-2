using Lomztein.BFA2.Content.Assemblers;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
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
    public class MapData : IAssemblable
    {
        public string Name;
        public string Description;

        public int Width;
        public int Height;

        public TileData Tiles;
        public ObjectModel[] Objects = new ObjectModel[0];

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

        public ValueModel Disassemble()
        {
            GameObjectAssembler assembler = new GameObjectAssembler();
            ObjectAssembler objectAssembler = new ObjectAssembler();

            ObjectModel obj = new ObjectModel(
                new ObjectField("Name", new PrimitiveModel(Name)),
                new ObjectField("Description", new PrimitiveModel("Junk")),
                new ObjectField("Width", new PrimitiveModel(Width)),
                new ObjectField("Height", new PrimitiveModel(Height)),
                new ObjectField("Tiles", Tiles.Disassemble()),
                new ObjectField("Objects", new ArrayModel(Objects))
                );

            return obj;
        }

        public void Assemble(ValueModel source)
        {
            ObjectModel obj = (source as ObjectModel);

            Name = obj.GetValue<string>("Name");
            Description = obj.GetValue<string>("Description");
            Width = obj.GetValue<int>("Width");
            Height = obj.GetValue<int>("Height");
            Tiles = AssembleTileData(obj.GetArray("Tiles"));
            Objects = obj.GetArray("Objects").Elements.Cast<ObjectModel>().ToArray();
        }

        private TileData AssembleTileData (ArrayModel array)
        {
            TileData data = new TileData(Width, Height);
            data.Assemble(array);
            return data;
        }
    }
}