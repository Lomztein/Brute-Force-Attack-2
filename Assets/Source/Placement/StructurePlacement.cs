using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.ObjectVisualizers;
using Lomztein.BFA2.Structures;
using Lomztein.BFA2.Structures.StructureManagement;
using Lomztein.BFA2.UI.ToolTip;
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
        private const string VISUALIZER_SET_PATH = "Core/VisualizerSets/Placement.json";

        private GameObject _obj;
        private GameObject _model;
        private IGridObject _placeable;
        private LayerMask _blockingLayer = LayerMask.GetMask("Structure");

        public event Action<GameObject> OnPlaced;
        public event Action OnFinished;

        private Func<string>[] _placeRequirements;
        private CompositeGameObjectVisualizer _visualizers;

        private const string SpawnPointTag = "EnemySpawnPoint";
        private Transform[] _enemySpawnPoints;

        public StructurePlacement (params Func<string>[] placeRequirements)
        {
            _placeRequirements = placeRequirements;
        }

        private ObjectVisualizerSet GetVisualizerSet() => Content.Get<ObjectVisualizerSet>(VISUALIZER_SET_PATH);

        public bool Pickup(GameObject obj)
        {
            Structure objStructure = obj.GetComponent<Structure>();
            if (objStructure)
            {
                GlobalStructureModManager.Instance.ApplyMods(objStructure);
            }

            _visualizers = CompositeGameObjectVisualizer.CreateFrom(GetVisualizerSet());
            _visualizers.Visualize(obj);

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
                GameObject go = UnityEngine.Object.Instantiate(_obj,_model.transform.position, _model.transform.rotation);

                ReflectionUtils.DynamicBroadcastInvoke(go, "OnInstantiated", true);
                ReflectionUtils.DynamicBroadcastInvoke(go, "OnPostInstantiated", true);

                go.SetActive(true);
                OnPlaced?.Invoke(go);
                return true;
            }
            return false;
        }

        // Returns width with rotation taken into consideration.
        private static Size GetWidth (IGridObject structure)
        {
            int rotIndex = GetRotationIndex(structure);
            if (rotIndex == 0 || rotIndex == 2) return structure.Width;
            if (rotIndex == 1 || rotIndex == 3) return structure.Height;
            throw new InvalidOperationException("RotIndex somehow not between 0-4");
        }

        // Returns height with rotation taken into consideration.
        private static Size GetHeight(IGridObject structure)
        {
            int rotIndex = GetRotationIndex(structure);
            if (rotIndex == 0 || rotIndex == 2) return structure.Height;
            if (rotIndex == 1 || rotIndex == 3) return structure.Width;
            throw new InvalidOperationException("RotIndex somehow not between 0-4");
        }

        // Returns 0 for pointing right, 1 for up, 2 for right, 3 for down.
        private static int GetRotationIndex(IGridObject structure)
        {
            if (structure is Component component)
            {
                float angle = component.transform.eulerAngles.z;
                return Mathf.RoundToInt(angle / 90f);
            }
            return 0;
        }

        public bool ToPosition(Vector2 position)
        {
            position = World.Grid.SnapToGrid(position, GetWidth(_placeable), GetHeight(_placeable));
            _obj.transform.position = position;
            _model.transform.position = position;
            string canPlace = CanPlace(_model.transform.position, _model.transform.rotation);
            if (string.IsNullOrEmpty(canPlace)) {
                _visualizers.Tint(Color.green);
                TintObject(_model, Color.green);
                ForcedTooltipUpdater.ResetTooltip();
                return true;
            }
            else {
                _visualizers.Tint(Color.red);
                TintObject(_model, Color.red);
                ForcedTooltipUpdater.SetTooltip(() => SimpleToolTip.InstantiateToolTip("Cannot place here", canPlace, null), canPlace);
                return false;
            }
        }

        private string CanPlace (Vector2 position, Quaternion rotation)
        {
            CannotPlaceReasons reasons = new CannotPlaceReasons();
            Vector2 size = new Vector2(World.Grid.SizeOf(GetWidth(_placeable)), World.Grid.SizeOf(GetHeight(_placeable)));

            if (Physics2D.OverlapBox(position, size * 0.9f, 0, _blockingLayer))
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
                        if (TileController.Instance.GetTile(pos).TileType == "BlockingWall")
                        {
                            reasons.BlockingWalls = true;
                        }
                        if (TileController.Instance.GetTile(pos).TileType == "NoBuild")
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

        private float GetAngleToNearestSpawnPoint (Vector3 position, float rounding = 90)
        {
            Vector3 pos = GetNearest(position, GetSpawnPoints()).position;
            float angle = Mathf.Atan2(pos.y - position.y, pos.x - position.x) * Mathf.Rad2Deg;
            return Mathf.Round(angle / rounding) * rounding;
        }

        private Transform GetNearest(Vector3 position, Transform[] transforms)
        {
            float dist = float.MaxValue;
            Transform nearest = null;

            foreach (Transform trans in transforms)
            {
                float curDist = Vector3.SqrMagnitude(trans.position - position);
                if (curDist < dist)
                {
                    dist = curDist;
                    nearest = trans;
                }
            }

            return nearest;
        }

        private Transform[] GetSpawnPoints ()
        {
            if (_enemySpawnPoints == null)
            {
                _enemySpawnPoints = GameObject.FindGameObjectsWithTag(SpawnPointTag).Select(x => x.transform).ToArray();
            }
            return _enemySpawnPoints;
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
            _visualizers.EndVisualization();
            ForcedTooltipUpdater.ResetTooltip();
            SetNoBuildTiles(false);
            return true;
        }

        public void Init()
        {
            SetNoBuildTiles(true);
        }

        public bool ToRotation(Quaternion rotation)
        {
            _obj.transform.rotation = rotation;
            _model.transform.rotation = rotation;
            return true;
        }

        public bool Flip()
        {
            return true;
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
