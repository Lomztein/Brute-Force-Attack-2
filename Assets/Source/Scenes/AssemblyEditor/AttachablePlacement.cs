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
    public class AttachablePlacement : ISimplePlacement
    {
        public event Action<GameObject> OnPlaced;
        public event Action OnFinished;

        private GameObject _obj;
        private GameObject _model;
        private IAttachable _attachable;
        private Match _currentMatch;

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
            _attachable = _obj.GetComponent<IAttachable>();
            _obj.SetActive(false);
            return _attachable != null;
        }

        public bool Place()
        {
            if (_currentMatch != null)
            {
                GameObject placed = UnityEngine.Object.Instantiate(_obj);
                placed.SetActive(true);
                placed.transform.position = _currentMatch.Position;
                placed.transform.rotation = _currentMatch.Rotation;
                placed.transform.parent = _currentMatch.Component.transform;

                foreach (var pair in _currentMatch.SlotPointMappings)
                {
                    pair.Key.Attach(_attachable);
                }

                OnPlaced?.Invoke(placed);
                return true;
            }
            return false;
        }

        private IEnumerable<Match> GetMatches(Vector3 position)
        {
            var groups = _attachable.GetPoints().GroupBy(x => x.Type); // Group by type.

            foreach (var group in groups)
            {
                TurretComponent[] components = Physics2D.OverlapPointAll(position).Select(x => x.GetComponent<TurretComponent>()).Where(x => x != null).ToArray();
                AttachmentPoint checkPoint = group.First();

                if (checkPoint == null)
                {
                    yield break;
                }

                foreach (TurretComponent component in components)
                {
                    IEnumerable<AttachmentSlot> points = component.AttachmentSlots.GetSupportingPoints(checkPoint);
                    foreach (var point in points)
                    {
                        Vector2 offset = point.LocalPosition - checkPoint.LocalPosition;
                        var mappings = MapPointsToSlots(points, group, checkPoint.RequiredPoints, component.transform.position, component.transform.rotation, offset, _attachable.WorldRotation);
                        if (mappings.Count == checkPoint.RequiredPoints)
                        {
                            Quaternion rot = point.GetWorldRotation(component.transform.rotation);
                            yield return new Match(component, component.transform.position + (Vector3)offset + Vector3.back, rot, mappings);
                        }
                    }
                }
            }
        }

        private Match GetClosestMatch(IEnumerable<Match> matches, Vector3 position)
        {
            float max = float.MaxValue;
            Match curr = null;

            foreach (var match in matches)
            {
                float dist = Vector2.Distance(position, match.Position);
                if (dist < max)
                {
                    max = dist;
                    curr = match;
                }
            }

            return curr;
        }

        private Dictionary<AttachmentSlot, AttachmentPoint> MapPointsToSlots(IEnumerable<AttachmentSlot> parentSlots, IEnumerable<AttachmentPoint> childPoints, int targetCount, Vector3 parentPos, Quaternion parentRot, Vector3 childPos, Quaternion childRot)
        {
            var mappings = new Dictionary<AttachmentSlot, AttachmentPoint>();
            foreach (AttachmentPoint point in childPoints)
            {
                AttachmentSlot slot = GetSlot (parentSlots, point, parentPos, parentRot, childPos, childRot);
                if (slot != null)
                {
                    mappings.Add(slot, point);
                }
            }
            return mappings;
        }

        private AttachmentSlot GetSlot(IEnumerable<AttachmentSlot> parentSlots, AttachmentPoint point, Vector3 parentPos, Quaternion parentRot, Vector3 childPos, Quaternion childRot)
        {
            var p = GetSlotAtPosition(parentSlots, parentPos, parentRot, point.GetWorldPosition(childPos, childRot));
            return p != null && p.IsEmpty() ? p : null;
        }

        private AttachmentSlot GetSlotAtPosition(IEnumerable<AttachmentSlot> set, Vector3 parentPos, Quaternion parentRot, Vector3 pointWorldPos, float sqrEpsilon = 0.1f)
        {
            foreach (AttachmentSlot slot in set)
            {
                Vector3 slotPos = slot.GetWorldPosition(parentPos, parentRot);
                if (Vector2.SqrMagnitude((parentPos + pointWorldPos) - slotPos) < sqrEpsilon)
                {
                    return slot;
                }
            }
            return null;
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
                _currentMatch = GetClosestMatch(matches, position);
                _model.transform.position = _currentMatch.Position;
                _model.transform.rotation = _currentMatch.Rotation;
                return true;
            }
        }

        private class Match
        {
            public TurretComponent Component { get; }
            public Vector3 Position { get; }
            public Quaternion Rotation { get; }
            public Dictionary<AttachmentSlot, AttachmentPoint> SlotPointMappings { get; }

            public Match(TurretComponent component, Vector3 position, Quaternion rotation, Dictionary<AttachmentSlot, AttachmentPoint> slotPointMappings)
            {
                Component = component;
                Position = position;
                Rotation = rotation;
                SlotPointMappings = slotPointMappings;
            }
        }
    }
}
