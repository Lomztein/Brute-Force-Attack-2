using Lomztein.BFA2.Placement;
using Lomztein.BFA2.World.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Buttons
{
    public class PlaceTilesButton : MonoBehaviour
    {
        [SerializeField] private string _type;

        public void Click ()
        {
            TilePlacement placement = new TilePlacement();
            placement.SetType(new TileType(_type));
            PlacementController.Instance.TakePlacement(placement);
        }
    }
}
