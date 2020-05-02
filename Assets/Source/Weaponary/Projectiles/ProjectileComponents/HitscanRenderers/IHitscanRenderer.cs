using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Projectiles.ProjectileComponents.HitscanRenderers
{
    public interface IHitscanRenderer
    {
        void SetPositions(Vector3 start, Vector3 end);
    }
}
