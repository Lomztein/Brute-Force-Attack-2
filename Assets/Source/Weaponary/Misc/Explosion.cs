using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.Weaponary.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Misc
{
    public class Explosion : MonoBehaviour
    {
        [ModelProperty]
        public LayerMask Layer;
        [ModelProperty]
        public float ParticleBaseRange;
        [ModelProperty]
        public float Life;

        public void Explode (double damage, float range, Action<DamageInfo> onDoDamage)
        {
            var particles = GetComponentInChildren<ParticleSystem>();
            particles.transform.localScale = Vector3.one * range / ParticleBaseRange;
            particles.Play();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, Layer);

            foreach (Collider2D col in colliders)
            {
                if (col.TryGetComponent<IDamagable>(out var damagable))
                {
                    var info = new DamageInfo(this, new TransformTarget(col.transform), damage, Colorization.Color.Red, true);
                    damagable.TakeDamage(info);
                    onDoDamage(info);
                }
            }

            Destroy(gameObject, Life);
        }
    }
}
