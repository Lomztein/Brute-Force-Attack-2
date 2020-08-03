using Lomztein.BFA2.Grid;
using Lomztein.BFA2.Turrets;
using Lomztein.BFA2.Turrets.Highlighters;
using Lomztein.BFA2.UI.Tooltip;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.World;
using Lomztein.BFA2.World.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Placement
{
    public class StructurePlacement : ISimplePlacement
    {
        private GameObject _obj;
        private GameObject _model;
        private IGridObject _placeable;
        private LayerMask _blockingLayer;

        public event Action OnPlaced;
        public event Action OnCancelled;
        public event Action OnFinished;

        private Func<string>[] _placeRequirements;
        private HighlighterCollection _highlighters;

        public StructurePlacement (params Func<string>[] placeRequirements)
        {
            _placeRequirements = placeRequirements;
        }

        public bool Pickup(GameObject obj)
        {
            _highlighters = HighlighterCollection.Create(obj);
            _highlighters.Highlight();

            _obj = obj;
            _obj.SetActive(false);
            _model = UnityUtils.InstantiateMockGO(_obj);
            _placeable = obj.GetComponent<IGridObject>();

            return _placeable != null;
        }

        private void SetNoBuildTiles (bool value)
        {
            GameObject.FindGameObjectWithTag("NoBuildTileRenderer").GetComponent<MeshRenderer>().enabled = value;
        }

        private void TintObject (GameObject obj, Color color)
        {
            foreach (SpriteRenderer renderer in obj.GetComponentsInChildren<SpriteRenderer>())
            {
                renderer.color = color;
            }
        }

        private void ResetObjectTint(GameObject obj) => TintObject(obj, Color.white);

        public bool Place()
        {
            if (string.IsNullOrEmpty(CanPlace (_model.transform.position, _model.transform.rotation)))
            {
                UnityEngine.Object.Instantiate(_obj,_model.transform.position, _model.transform.rotation).SetActive(true);
                OnPlaced?.Invoke();
                return true;
            }
            return false;
        }

        public bool ToPosition(Vector2 position, Quaternion rotation)
        {
            position = GridDimensions.SnapToGrid(position, _placeable.Width, _placeable.Height);
            _obj.transform.position = position;
            _obj.transform.rotation = rotation;
            _model.transform.position = position;
            _model.transform.rotation = rotation;
            string canPlace = CanPlace(position, rotation);
            if (string.IsNullOrEmpty(canPlace)) {
                _highlighters.Tint(Color.green);
                TintObject(_model, Color.green);
                ForcedTooltipUpdater.ResetTooltip();
                return true;
            }
            else {
                _highlighters.Tint(Color.red);
                TintObject(_model, Color.red);
                ForcedTooltipUpdater.SetTooltip("Cannot place here", canPlace, null);
                return false;
            }
        }

        private string CanPlace (Vector2 position, Quaternion rotation)
        {
            CannotPlaceReasons reasons = new CannotPlaceReasons();
            Vector2 size = new Vector2 (GridDimensions.SizeOf (_placeable.Width), GridDimensions.SizeOf(_placeable.Height));


            if (Physics2D.OverlapBox(position, size * 0.9f, 0))
            {
                reasons.SpaceOccupied = true;
            }

            (Vector2Int from, Vector2Int to) = GetFromToChecks(position, size);
            for (int y = from.y; y < to.y; y++)
            {
                for (int x = from.x; x < to.x; x++)
                {
                    Vector2 pos = new Vector2(x, y) + new Vector2(0.5f, 0.5f);
                    if (!MapController.Instance.InInsideMap(pos))
                    {
                        reasons.OutsideMapArea = true;
                    }
                    else
                    {
                        if (TileController.Instance.GetTile(pos).WallType == "BlockingWall")
                        {
                            reasons.BlockingWalls = true;
                        }
                        if (TileController.Instance.GetTile(pos).WallType == "NoBuild")
                        {
                            reasons.NoBuild = true;
                        }
                    }

                    Debug.DrawRay(pos, Vector3.right, Color.red);
                }
            }

            StringBuilder requirementReasons = new StringBuilder();
            foreach (var requirement in _placeRequirements)
            {
                string reason = requirement.Invoke();
                if (!string.IsNullOrEmpty(reason))
                {
                    requirementReasons.AppendLine(" - " + reason);
                }
            }

            return (reasons.ToString() + requirementReasons.ToString()).TrimEnd();
        }

        private (Vector2Int from, Vector2Int to) GetFromToChecks (Vector2 position, Vector2 size)
        {
            Vector2Int from = new Vector2Int(
                Mathf.RoundToInt(position.x - size.x / 2f),
                Mathf.RoundToInt(position.y - size.y / 2f)
                );

            Vector2Int to = new Vector2Int(
                Mathf.RoundToInt(position.x + size.x / 2f),
                Mathf.RoundToInt(position.y + size.y / 2f)
            );


            return (from, to);
        }

        public override string ToString()
        {
            return _placeable.ToString();
        }

        public bool Finish()
        {
            UnityEngine.Object.Destroy(_obj);
            UnityEngine.Object.Destroy(_model);
            OnFinished?.Invoke();
            _highlighters.EndHighlight();
            ForcedTooltipUpdater.ResetTooltip();
            SetNoBuildTiles(false);
            return true;
        }

        public void Init()
        {
            SetNoBuildTiles(true);
        }

        private struct CannotPlaceReasons
        {
            public bool SpaceOccupied;
            public bool OutsideMapArea;
            public bool BlockingWalls;
            public bool NoBuild;

            public override string ToString()
            {
                StringBuilder builder = new StringBuilder();

                if (SpaceOccupied)
                {
                    builder.AppendLine(" - Space is occupied");
                }

                if (OutsideMapArea)
                {
                    builder.AppendLine(" - Outside map area");
                }

                if (BlockingWalls)
                {
                    builder.AppendLine(" - Cannot place on blocking walls");
                }

                if (NoBuild)
                {
                    builder.AppendLine(" - Cannot place here");
                }

                return builder.ToString();
            }
        }
    }
}
