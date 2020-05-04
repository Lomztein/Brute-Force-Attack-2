using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Projectiles.ProjectileComponents.HitscanRenderers
{
    public class StraightHitscanRenderer : MonoBehaviour, IHitscanRenderer
    {
        public LineRenderer Renderer;
        [ModelProperty] public float ShrinkTime;

        public void SetPositions(Vector3 start, Vector3 end)
        {
            Renderer.SetPosition(0, start);
            Renderer.SetPosition(1, end);
            StartCoroutine(ShrinkBeam());
        }

        private IEnumerator ShrinkBeam()
        {
            float width = Renderer.startWidth;
            float shrinkSpeed = width / ShrinkTime;

            for (int i = 0; i < 1 / Time.fixedDeltaTime * ShrinkTime; i++)
            {
                width -= shrinkSpeed * Time.fixedDeltaTime;
                Renderer.startWidth = width;
                Renderer.endWidth = width;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
