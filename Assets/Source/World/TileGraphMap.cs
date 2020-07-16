using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.World
{
    public class TileGraphMap : IGraphMap
    {
        private int _width;
        private int _height;

        private Graph.Node[,] _nodes;

        public TileGraphMap(int width, int height, Graph.Node[,] nodes)
        {
            _width = width;
            _height = height;
            _nodes = nodes;
        }

        public Graph.Node GetNode(Vector3 position)
        {
            Vector2Int pos = MapUtils.WorldToTileCoords(position, _width, _height);
            return _nodes[pos.x, pos.y];
        }
    }
}
