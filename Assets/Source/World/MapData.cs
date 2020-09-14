using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.GameObject;
using Lomztein.BFA2.Serialization.Models.Property;
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

        public IObjectModel Disassemble()
        {
            GameObjectAssembler assembler = new GameObjectAssembler();
            ObjectAssembler objectAssembler = new ObjectAssembler();

            return new ObjectModel(typeof(MapData),
                new ObjectField("Name", new ValuePropertyModel(Name)),
                new ObjectField("Description", new ValuePropertyModel(Description)),
                new ObjectField("Width", new ValuePropertyModel(Width)),
                new ObjectField("Height", new ValuePropertyModel(Height)),
                new ObjectField("Tiles", new ObjectPropertyModel(Tiles.Disassemble())),
                new ObjectField("Objects", new ArrayPropertyModel(typeof (GameObjectModel[]), Objects.Select(x => new ObjectPropertyModel (objectAssembler.Disassemble(x))).ToArray()))
                );
        }

        public void Assemble(IObjectModel source)
        {
            Name = source.GetValue<string>("Name");
            Description = source.GetValue<string>("Description");
            Width = source.GetValue<int>("Width");
            Height = source.GetValue<int>("Height");
            Tiles = AssembleTileData(source.GetObject("Tiles"));
            Objects = source.GetArray("Objects").Select(x => AssembleGameObject((x as ObjectPropertyModel).Model)).ToArray();
        }

        private TileData AssembleTileData (IObjectModel model)
        {
            ObjectAssembler assembler = new ObjectAssembler();
            TileData data = (TileData)assembler.Assemble(model);
            return data;
        }

        private IGameObjectModel AssembleGameObject(IObjectModel token)
        {
            ObjectAssembler objectAssembler = new ObjectAssembler();
            IGameObjectModel model = (IGameObjectModel)objectAssembler.Assemble(token);
            return model;
        }
    }
}