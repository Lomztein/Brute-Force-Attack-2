using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Projectiles.ProjectileComponents.HitscanRenderers
{
    public class StraightHitscanRenderer : MonoBehaviour, IHitscanRenderer
    {
        private LineRenderer _renderer;

        public void SetPositions(Vector3 start, Vector3 end)
        {
            _renderer.SetPosition(0, start);
            _renderer.SetPosition(1, end);
        }
    }
}
