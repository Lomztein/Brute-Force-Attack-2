using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.World.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies
{
    public class EnemyPoint : MonoBehaviour
    {

        private LooseDependancy<TileController> _tiles = new LooseDependancy<TileController>();
        private TileType[] _blockingTiles = new TileType[]
        {
            TileType.BlockingWall, TileType.PlayerWall
        };

        public bool IsTileBlocked ()
        {
            if (_tiles.Exists)
            {
                return _blockingTiles.Any(x => _tiles.Dependancy.GetTile(transform.position).TileType == x.Name);
            }
            return false;
        }
    }
}
