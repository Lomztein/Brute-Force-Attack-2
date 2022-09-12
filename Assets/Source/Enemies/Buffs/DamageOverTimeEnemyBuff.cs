using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Weaponary.Targeting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Buffs
{
    [CreateAssetMenu(menuName = "BFA2/Enemy Buffs/Damage Over Time", fileName = "New " + nameof(DamageOverTimeEnemyBuff))]
    public class DamageOverTimeEnemyBuff : EnemyBuff
    {
        [ModelProperty]
        public ContentPrefabReference Effect;
        [ModelProperty]
        public float EffectPostLife;

        private GameObject _effectObj;

        [ModelProperty]
        public Colorization.Color DamageColor;

        private float CurrentDPS => Power * CurrentStack;

        public override void Begin(Enemy target, int stackSize, float time)
        {
            base.Begin(target, stackSize, time);
            _effectObj = Effect.Instantiate();
            _effectObj.transform.localScale = target.GetComponent<Collider2D>().bounds.size;
        }

        public override void End()
        {
            base.End();
            if (_effectObj.TryGetComponent(out ParticleSystem system)) system.Stop();
            Destroy(_effectObj, EffectPostLife);
        }

        public override void Tick(float dt)
        {
            base.Tick(dt);
            Target.TakeDamage(new Weaponary.DamageInfo(this, new TransformTarget(Target.transform), CurrentDPS * Coeffecient * dt, DamageColor));
            _effectObj.transform.position = Target.transform.position;
        }
    }
}
