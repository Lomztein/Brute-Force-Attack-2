using Lomztein.BFA2.Modification.Modifiers.ModBroadcasters;
using Lomztein.BFA2.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Visuals
{
    public class ModBroadcasterOrb : MonoBehaviour
    {
        public bool SelfAssignToParent = true;

        public Transform OrbTransform;
        public TrailRenderer Renderer;

        public float HoverDistanceMultiplier;
        private float _hoverDistance = 1f;
        private float _hoverAngle;

        public float HoverDistanceBobAmount;
        public float HoverDistanceBobFrequency;
        public float HoverAngularSpeed;

        private float _sign;

        private void Start()
        {
            _sign = UnityEngine.Random.Range(0, 2) == 1 ? -1f : 1f;
            _hoverAngle = UnityEngine.Random.Range(0f, 360f);
            if (SelfAssignToParent)
            {
                Assign(GetComponentInParent<ModBroadcaster>());
            }
            UpdateTrailPosition();
        }

        public void Assign (ModBroadcaster broadcaster)
        {
            Structure structure = broadcaster.GetComponentInParent<Structure>();
            _hoverDistance = new Vector2(World.Grid.SizeOf(structure.Width), World.Grid.SizeOf(structure.Height)).magnitude;
            Color color = Colorization.ColorInfo.Get(broadcaster.Mod.Color).DisplayColor;
            Renderer.material.color = color;

            Color curColor = Renderer.material.GetColor("_EmissionColor");
            Renderer.material.SetColor("_EmissionColor", color * curColor.maxColorComponent);
            Renderer.Clear();
        }

        private float GetHoverDistance ()
        {
            float sin = Mathf.Sin(Time.time * Mathf.PI * 2f * HoverDistanceBobFrequency);
            float distance = (_hoverDistance / 2f) * HoverDistanceMultiplier + sin * (HoverDistanceBobAmount * _hoverDistance);
            return distance;
        }

        private void FixedUpdate()
        {
            _hoverAngle += HoverAngularSpeed * Time.fixedDeltaTime * _sign;
            UpdateTrailPosition();
        }

        private void UpdateTrailPosition ()
        {
            Vector3 position = transform.position + Quaternion.Euler(0f, 0f, _hoverAngle) * Vector3.right * GetHoverDistance();
            OrbTransform.position = position;
        }
    }
}
