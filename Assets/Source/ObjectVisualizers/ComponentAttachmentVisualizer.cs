using Lomztein.BFA2.Structures.Turrets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.ObjectVisualizers
{
    public class ComponentAttachmentVisualizer : ObjectVisualizerBase<TurretComponent>
    {
        public GameObject SlotPrefab;
        public GameObject PointPrefab;

        private List<GameObject> _currentRenderers = new List<GameObject>();

        private TurretComponent _component;
        private Color _tint;

        public override void Visualize(TurretComponent component)
        {
            Follow(component.transform);
            _component = component;
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
            ClearLineRenderers();
            if (_component)
            {
                float size = Mathf.Max(World.Grid.SizeOf(_component.Width), World.Grid.SizeOf(_component.Height)) * 1024f;
                Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, size);

                foreach (var point in _component.AttachmentPoints)
                {
                    GameObject indicator = Instantiate(PointPrefab, point.GetWorldPosition(_component.transform) + Vector3.back * 1f, Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + point.LocalAngle));
                    indicator.GetComponentInChildren<SpriteRenderer>().color = _tint;
                    indicator.transform.SetParent(transform);
                    _currentRenderers.Add(indicator);
                }


                var targetComponents = targets.Select(x => x.GetComponent<TurretComponent>()).Where(x => x != null);
                foreach (var component in targetComponents)
                {
                    var slots = component.AttachmentSlots.GetSlots().Where(x => _component.AttachmentPoints.Any(y => x.CanSupport(y)) && x.IsEmpty());

                    foreach (var slot in slots)
                    {
                        Vector3 worldPos = slot.GetWorldPosition(component.transform);
                        GameObject indicator = Instantiate(SlotPrefab, worldPos, Quaternion.identity);
                        indicator.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                        indicator.transform.position = worldPos + Vector3.back * 1f;
                        _currentRenderers.Add(indicator);
                    }
                }
            }
        }

        public override void EndVisualization()
        {
            base.EndVisualization();
            ClearLineRenderers();
        }

        private void ClearLineRenderers()
        {
            foreach (GameObject renderer in _currentRenderers)
            {
                if (renderer)
                {
                    Destroy(renderer);
                }
            }
            _currentRenderers.Clear();
        }

        public override void Tint(Color color)
        {
            _tint = color;
        }
    }
}
