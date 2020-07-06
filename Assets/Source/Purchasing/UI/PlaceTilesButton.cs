using Lomztein.BFA2.Placement;
using Lomztein.BFA2.World.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Purchasing.UI
{
    public class PlaceTilesButton : MonoBehaviour
    {
        public TileType TileType = TileType.PlayerWall;

        public void Click ()
        {
            TilePlacement placement = new TilePlacement();
            placement.SetType(TileType);
            PlacementController.Instance.TakePlacement(placement);
        }
    }
}
