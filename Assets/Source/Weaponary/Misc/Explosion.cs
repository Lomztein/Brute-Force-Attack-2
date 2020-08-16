using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Utilities;
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

        public void Explode (float damage, float range)
        {
            var particles = GetComponentInChildren<ParticleSystem>();
            particles.Scale(range / ParticleBaseRange);
            particles.Play();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, Layer);

            foreach (Collider2D col in colliders)
            {
                IDamagable damagable = col.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    damagable.TakeDamage(new DamageInfo(damage, Colorization.Color.Red));
                }
            }

            Destroy(gameObject, Life);
        }
    }
}
