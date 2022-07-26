using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.ModBroadcasters
{
    public class BuffModBroadcaster : ModBroadcaster
    {
        public float BuffTime;

        public override IEnumerable<IModdable> GetPotentialBroadcastTargets()
        {
            return GetComponents<IModdable>();
        }

        public static BuffModBroadcaster AddBuff (Mod mod, GameObject target, float time)
        {
            var broadcaster = target.AddComponent<BuffModBroadcaster>();
            broadcaster.BuffTime = time;
            broadcaster.Mod = mod;
            broadcaster.BroadcastMod();
            return broadcaster;
        }

        public static IEnumerable<BuffModBroadcaster> AddBuffsInArea(Mod modPrefab, Vector3 position, float range, float time, int layerMask = Physics2D.AllLayers)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, range, layerMask);
            BuffModBroadcaster[] broadcasters = new BuffModBroadcaster[colliders.Length];
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out IModdable _))
                {
                    broadcasters[i] = AddBuff(Instantiate(modPrefab), colliders[i].gameObject, time);
                }
            }
            return broadcasters;
        }

        private void FixedUpdate()
        {
            BuffTime -= Time.fixedDeltaTime;
            if (BuffTime < 0f)
            {
                ClearMod();
                Destroy(this);
            }
        }
    }
}
