using Lomztein.BFA2.Grid;
using Lomztein.BFA2.Turrets;
using Lomztein.BFA2.Turrets.Highlighters;
using Lomztein.BFA2.UI.Tooltip;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Placement
{
    public class GridPlacement : ISimplePlacement
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

        public GridPlacement (params Func<string>[] placeRequirements)
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
            StringBuilder reasons = new StringBuilder();
            Vector2 size = new Vector2 (GridDimensions.SizeOf (_placeable.Width), GridDimensions.SizeOf(_placeable.Height)) / 2.1f;
            if (Physics2D.OverlapBox(position, size, 0))
            {
                reasons.AppendLine(" - Space is occupied");
            }
            if (!MapController.Instance.InInsideMap(position))
            {
                reasons.AppendLine(" - Outside map area");
            }
            foreach (var requirement in _placeRequirements)
            {
                string reason = requirement.Invoke();
                if (!string.IsNullOrEmpty(reason))
                {
                    reasons.AppendLine(" - " + reason);
                }
            }

            return reasons.ToString().TrimEnd();
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
            return true;
        }
    }
}
