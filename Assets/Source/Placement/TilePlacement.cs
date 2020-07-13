using Lomztein.BFA2.Grid;
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
    public class TilePlacement : IPlacement
    {
        public enum Behaviour { Set, Replace }

        public event Action OnFinished;

        private TileType _type;
        private Behaviour _behaviour;

        private TileType DeleteType => TileType.Empty;

        private GameObject _placementGraphic;
        private string _placementGraphicPath = "Prefabs/TilePlacementGraphic";

        private Vector2 _startPosition;

        public void SetType (TileType type, Behaviour behaviour)
        {
            _type = type;
            _behaviour = behaviour;
        }

        private GameObject GetPlacementGraphic ()
        {
            if (_placementGraphic == null)
            {
                _placementGraphic = UnityEngine.Object.Instantiate(Resources.Load<GameObject>(_placementGraphicPath));
            }
            return _placementGraphic;
        }

        private void TintGraphic (Color color)
        {
            GetPlacementGraphic().GetComponentInChildren<Renderer>().material.color = color;
        }

        private void TileObject(GameObject obj, Vector2 scale)
        {
            GetPlacementGraphic().GetComponentInChildren<Renderer>().material.mainTextureScale = scale;
        }

        public void StartDrag (int mb, Vector2 position)
        {
            _startPosition = GridDimensions.SnapToGrid(ClampToMap(position), Size.Small);
            if (mb == 0)
            {
                TintGraphic(Color.green);
            }
            else
            {
                TintGraphic(Color.red);
            }
            GetPlacementGraphic().transform.localScale = Vector3.zero;
        }

        public void ToPosition (Vector2 position)
        {
            GetPlacementGraphic().transform.position = GridDimensions.SnapToGrid(ClampToMap(position), Size.Small);
            GetPlacementGraphic().transform.localScale = Vector3.one;
            TileObject(GetPlacementGraphic(), Vector2.one);
        }

        public (Vector2 from, Vector2 to) Normalize (Vector2 from, Vector2 to)
        {
            Vector2 f = new Vector2(Mathf.Min(from.x, to.x), Mathf.Min(from.y, to.y));
            Vector2 t = new Vector2(Mathf.Max(from.x, to.x), Mathf.Max(from.y, to.y));
            return (f, t);
        }

        public (Vector2 from, Vector2 to) ExpandNormalized(Vector2 from, Vector2 to, float amount)
        {
            Vector2 f = from - new Vector2(amount, amount);
            Vector2 t = to + new Vector2(amount, amount);
            return (f, t);
        }

        public void Drag (int mb, Vector2 position)
        {
            position = GridDimensions.SnapToGrid(ClampToMap(position), Size.Small);
            (Vector2 from, Vector2 to) = Normalize(_startPosition, position);
            (from, to) = ExpandNormalized(from, to, 0.5f);

            Vector2 delta = to - from;
            Vector2 pos = from + delta / 2f;
            TileObject(GetPlacementGraphic(), delta);

            GetPlacementGraphic().transform.position = pos;
            GetPlacementGraphic().transform.localScale = new Vector3(delta.x, delta.y, 1f);
        }

        public void EndDrag (int mb, Vector2 position)
        {
            position = GridDimensions.SnapToGrid(ClampToMap(position), Size.Small);
            (Vector2 from, Vector2 to) = Normalize(_startPosition, position);
            (from, to) = ExpandNormalized(from, to, 0.1f);

            switch (_behaviour)
            {
                case Behaviour.Replace:
                    TileController.Instance.ReplaceTiles(from, to, GetTypeReverse(mb), GetType(mb));
                    break;

                case Behaviour.Set:
                    TileController.Instance.SetTiles(from, to, GetType(mb));
                    break;
            }
            GetPlacementGraphic().transform.localScale = Vector3.one;
            TintGraphic(Color.white);
        }

        private TileType GetType(int mb) => mb == 0 ? _type : DeleteType;
        private TileType GetTypeReverse(int mb) => mb == 0 ? DeleteType : _type;

        private Vector2 ClampToMap (Vector2 position)
        {
            Vector2 clamped = MapController.Instance.ClampToMap(position);
            return new Vector2(
                Mathf.Max(clamped.x, -MapController.Instance.Width / 2f + 0.5f),
                Mathf.Max(clamped.y, -MapController.Instance.Height / 2f + 0.5f)
                );
        }

        public bool Finish()
        {
            OnFinished?.Invoke();
            UnityEngine.Object.Destroy(GetPlacementGraphic());
            return true;
        }
    }
}
