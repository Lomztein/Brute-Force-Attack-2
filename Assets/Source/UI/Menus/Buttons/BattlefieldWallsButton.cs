using Lomztein.BFA2.UI.ToolTip;
using Lomztein.BFA2.World;
using Lomztein.BFA2.World.Tiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Menus.Buttons
{
    public class BattlefieldWallsButton : MonoBehaviour
    {
        public Button Button;

        private void Awake()
        {
            MapController.Instance.OnMapDataLoaded += OnMapDataLoaded;
        }

        private void OnMapDataLoaded(MapData obj)
        {
            bool canBuildWalls = false;
            for (int y = 0; y < obj.Tiles.Height; y++)
            {
                for (int x = 0; x < obj.Tiles.Width; x++)
                {
                    TileTypeReference reference = obj.Tiles.GetTile(x, y);
                    if (reference.IsType(TileType.Empty) || reference.IsType(TileType.PlayerWall))
                    {
                        canBuildWalls = true;
                    }
                }
            }
            Button.interactable = canBuildWalls;

            if (!canBuildWalls)
            {
                GetComponent<SimpleToolTip>().Description = "Unavailable on this map.";
            }
        }
    }
}
