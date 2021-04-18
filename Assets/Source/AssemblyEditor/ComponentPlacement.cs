using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.Structures.Turrets.Attachment;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.AssemblyEditor
{
    public class ComponentPlacement : ISimplePlacement
    {
        public event Action<GameObject> OnPlaced;
        public event Action OnFinished;

        private GameObject _obj;
        private GameObject _model;
        private TurretComponent _component;
        private Tuple<TurretComponent, Vector3> _target;

        public bool Finish()
        {
            OnFinished?.Invoke();
            UnityEngine.Object.Destroy(_model);
            UnityEngine.Object.Destroy(_obj);
            return true;
        }

        public void Init()
        {
        }

        public bool Pickup(GameObject obj)
        {
            _obj = obj;
            _model = UnityUtils.InstantiateMockGO(_obj);
            _component = _obj.GetComponent<TurretComponent>();
            _obj.SetActive(false);
            return _component != null;
        }

        public bool Place()
        {
            if (_target != null)
            {
                GameObject placed = UnityEngine.Object.Instantiate(_obj);
                placed.SetActive(true);
                placed.transform.position = _target.Item2;
                placed.transform.parent = (_target.Item1 as Component).transform;
                OnPlaced?.Invoke(placed);
                return true;
            }
            return false;
        }

        private IEnumerable<Tuple<TurretComponent, Vector3>> GetMatches(Vector3 position)
        {
            TurretComponent[] components = Physics2D.OverlapPointAll(position).Select(x => x.GetComponent<TurretComponent>()).Where(x => x != null).ToArray();
            AttachmentPoint checkPoint = _component.GetLowerAttachmentPoints().FirstOrDefault();

            if (checkPoint == null)
            {
                return new Tuple<TurretComponent, Vector3>[0];
            }

            var matches = new List<Tuple<TurretComponent, Vector3>>();
            foreach (TurretComponent component in components)
            {
                IEnumerable<AttachmentPoint> points = component.GetUpperAttachmentPoints();
                foreach (var point in points)
                {
                    Vector2 offset = point.LocalPosition - checkPoint.LocalPosition;
                    if (Matches(points, _component.GetLowerAttachmentPoints(), Vector3.zero, offset))
                    {
                        matches.Add(new Tuple<TurretComponent, Vector3>(component, (component as Component).transform.position + (Vector3)offset + Vector3.back));
                    }
                }
            }

            return matches;
        }

        private Tuple<TurretComponent, Vector3> GetClosestMatch(IEnumerable<Tuple<TurretComponent, Vector3>> matches, Vector3 position)
        {
            float max = float.MaxValue;
            Tuple<TurretComponent, Vector3> curr = null;

            foreach (var match in matches)
            {
                float dist = Vector2.Distance(position, match.Item2);
                if (dist < max)
                {
                    max = dist;
                    curr = match;
                }
            }

            return curr;
        }

        private bool Matches(IEnumerable<AttachmentPoint> parentPoints, IEnumerable<AttachmentPoint> childPoints, Vector3 parentPosition, Vector3 childPosition)
            => childPoints.All(x => IsPointAvailable(parentPoints, x, parentPosition, childPosition));

        private bool IsPointAvailable(IEnumerable<AttachmentPoint> parentPoints, AttachmentPoint point, Vector3 parentPos, Vector3 childPos)
        {
            var p = parentPoints.GetPoint(parentPos, point.LocalToWorldPosition(childPos));
            return p != null && p.IsEmpty;
        }

        public bool ToPosition(Vector2 position, Quaternion rotation)
        {
            var matches = GetMatches(position);
            if (!matches.Any())
            {
                _model.transform.position = position;
                return false;
            }
            else
            {
                _target = GetClosestMatch(matches, position);
                _model.transform.position = _target.Item2;
                return true;
            }
        }
    }
}
