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
        private TurretComponent _componenet;
        private Match _currentMatch;
        private Transform _probe;
        private float _angleOffset;

        public bool Finish()
        {
            OnFinished?.Invoke();
            UnityEngine.Object.Destroy(_model);
            UnityEngine.Object.Destroy(_obj);
            UnityEngine.Object.Destroy(_probe.gameObject);
            return true;
        }

        public void Init()
        {
            _probe = new GameObject().transform;
        }

        public bool Pickup(GameObject obj)
        {
            _obj = obj;
            _model = UnityUtils.InstantiateMockGO(_obj);
            _componenet = _obj.GetComponent<TurretComponent>();
            _obj.SetActive(false);
            return _componenet != null;
        }

        public bool Place()
        {
            if (_currentMatch != null)
            {
                GameObject placed = UnityEngine.Object.Instantiate(_obj);
                placed.SetActive(true);
                placed.transform.position = _currentMatch.Position + Vector3.back * 0.1f;
                placed.transform.rotation = _currentMatch.Rotation;
                placed.transform.parent = _currentMatch.Component.transform;

                foreach (var pair in _currentMatch.SlotPointMappings)
                {
                    pair.Key.Attach(_componenet);
                }

                OnPlaced?.Invoke(placed);
                return true;
            }
            return false;
        }

        private IEnumerable<Match> GetMatches(Vector3 position)
        {
            var points = _componenet.GetPoints();
            _probe.transform.position = position;
            _probe.transform.localScale = _componenet.transform.localScale;

            foreach (var point in points)
            {
                TurretComponent[] components = Physics2D.OverlapCircleAll(position, 1f).Select(x => x.GetComponent<TurretComponent>()).Where(x => x != null).ToArray();

                foreach (TurretComponent component in components)
                {
                    IEnumerable<AttachmentSlot> compatables = component.AttachmentSlots.GetSupportingPoints(point).Where(x => x.IsEmpty());
                    foreach (var compatable in compatables)
                    {
                        Vector2 diffPos = point.GetWorldPosition(_probe) - compatable.GetWorldPosition(component.transform);
                        _probe.transform.position -= (Vector3)diffPos;

                        Quaternion rotation = Quaternion.Euler(0f, 0f, compatable.LocalAngle + point.LocalAngle + _angleOffset);
                        _probe.transform.rotation = component.transform.rotation * rotation;

                        var pointsToTry = points.Where(x => x.Type == point.Type && x.Size == point.Size);
                        var matchingPoints = new Dictionary<AttachmentSlot, AttachmentPoint>();

                        foreach (var toTry in pointsToTry)
                        {
                            var toTryPosition = toTry.GetWorldPosition(_probe);
                            var slotAtPosition = FindSlotAtPosition(compatables, component.transform, toTryPosition);
                        
                            if (slotAtPosition != null)
                            {
                                matchingPoints.Add(slotAtPosition, toTry);
                            }
                        }

                        if (matchingPoints.Count >= point.RequiredPoints)
                        {
                            yield return new Match(component, _probe.position + Vector3.forward * component.transform.position.z, _probe.rotation, matchingPoints);
                        }
                    }
                }
            }
        }

        private AttachmentSlot FindSlotAtPosition (IEnumerable<AttachmentSlot> slots, Transform parent, Vector3 position)
        {
            float epsilon = 0.1f;
            foreach (var slot in slots)
            {
                Vector3 pos = slot.GetWorldPosition(parent);
                if (Vector2.Distance(pos, position) < epsilon)
                {
                    return slot;
                }
            }
            return null;
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

        public bool ToPosition(Vector2 position)
        {
            var matches = GetMatches(position);
            if (!matches.Any())
            {
                _model.transform.position = position;
                _model.transform.rotation = Quaternion.Euler(0f, 0f, _angleOffset);
                return false;
            }
            else
            {
                _currentMatch = GetClosestMatch(matches, position);
                _model.transform.position = _currentMatch.Position + Vector3.back * 0.1f;
                _model.transform.rotation = _currentMatch.Rotation;
                return true;
            }
        }

        public bool ToRotation (Quaternion rotation)
        {
            _angleOffset = rotation.eulerAngles.z;
            return true;
        }

        public bool Flip ()
        {
            _componenet.Flip();
            _model.transform.localScale = _componenet.transform.localScale;
            return true;
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
