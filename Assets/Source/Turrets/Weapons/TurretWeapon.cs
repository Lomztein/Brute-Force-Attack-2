using Lomztein.BFA2.Animation.FireAnimations;
using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets.Rangers;
using Lomztein.BFA2.Turrets.Targeters;
using Lomztein.BFA2.Turrets.TargetProviders;
using Lomztein.BFA2.UI.Tooltip;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.Weaponary;
using Lomztein.BFA2.Weaponary.FireControl;
using Lomztein.BFA2.Weaponary.Projectiles;
using Lomztein.BFA2.World;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Color = Lomztein.BFA2.Colorization.Color;

namespace Lomztein.BFA2.Turrets.Weapons
{
    public class TurretWeapon : TurretComponent, IColorProvider, IWeapon {

        public ITargeter Targeter;
        public ITargetProvider Provider;
        public IRanger Ranger;

        [ModelProperty]
        public float FireTreshold;
        [ModelProperty]
        public LayerMask HitLayer;

        private Transform[] _muzzles;
        private const string MUZZLE_PARENT = "Muzzles";
        public IStatReference Damage;
        public IStatReference ProjectileAmount;
        public IStatReference Spread;
        public IStatReference Speed;
        public IStatReference Firerate;
        public float Cooldown => 1f / Firerate.GetValue();

        private IWeaponFire _weaponFire;
        private IFireAnimation _fireAnimation;
        private IFireControl _fireControl;

        [ModelProperty]
        public Color Color;
        private bool _chambered;

        private ParticleSystem[] _fireParticles;

        public override void End()
        {
        }

        public float GetDamage() => Damage.GetValue();

        public float GetFirerate() => Firerate.GetValue();

        public int GetProjectileAmount() => (int)ProjectileAmount.GetValue();

        public float GetSpread() => Spread.GetValue();

        public float GetSpeed() => Speed.GetValue();

        private Transform[] GetMuzzles ()
        {
            List<Transform> muzzles = new List<Transform>();
            Transform muzzleParent = transform.Find(MUZZLE_PARENT);
            foreach (Transform child in muzzleParent)
            {
                muzzles.Add(child);
            }
            return muzzles.ToArray();
        }

        private ParticleSystem[] GetFireParticles (Transform[] muzzles)
        {
            ParticleSystem[] particles = new ParticleSystem[muzzles.Length];
            for (int i = 0; i < muzzles.Length; i++)
            {
                Transform child = muzzles[i].Find("FireParticle");
                if (child)
                {
                    particles[i] = child.GetComponent<ParticleSystem>();
                }
            }
            return particles;
        }

        public override void Init()
        {
            Targeter = GetComponentInParent<ITargeter>();
            Provider = GetComponentInParent<ITargetProvider>();
            Ranger = GetComponentInParent<IRanger>();

            _weaponFire = GetComponent<WeaponFire>();
            _fireAnimation = GetComponent<IFireAnimation>() ?? new NoFireAnimation();
            _fireControl = GetComponent<IFireControl>() ?? new NoFireControl();
            _muzzles = GetMuzzles();
            _fireParticles = GetFireParticles(_muzzles);

            Damage = Stats.AddStat("Damage", "Damage", "The damage each projectile does.");
            ProjectileAmount = Stats.AddStat("ProjectileAmount", "Projectile Amount", "How many projectiles are fired at once.");
            Spread = Stats.AddStat("Spread", "Spread", "How much the projectiles spread.");
            Speed = Stats.AddStat("Speed", "Speed", "How fast the projectiles fly.");
            Firerate = Stats.AddStat("Firerate", "Firerate", "How quickly the weapon fires.");

            AddAttribute(Modification.ModdableAttribute.Weapon);

            if (isActiveAndEnabled)
            {
                _chambered = false;
                StartCoroutine(Rechamber(Cooldown));
            }
        }

        private IEnumerator Rechamber(float cooldown)
        {
            yield return UnityUtils.WaitForFixedSeconds(cooldown);
            _chambered = true;
        }

        public override void Tick(float deltaTime)
        {
            if (Targeter != null && Targeter.GetDistance() < FireTreshold)
            {
                TryFire();
            }
        }

        public bool TryFire()
        {
            if (_chambered)
            {
                Fire();
                _chambered = false;
                StartCoroutine(Rechamber(Cooldown));
                return true;
            }
            return false;
        }

        private void Fire()
        {
            IProjectileInfo info = new ProjectileInfo
            {
                Color = Color,
                Damage = Damage.GetValue(),
                Layer = HitLayer,
                Target = Provider?.GetTarget(),
                Range = GetRange()
            };

            _fireAnimation.Play(Cooldown);
            _fireControl.Fire(_muzzles.Length, Cooldown, (i) =>
            {
                _weaponFire.Fire(_muzzles[i].position, _muzzles[i].rotation, info, Speed.GetValue(), Spread.GetValue(), (int)ProjectileAmount.GetValue());
                EmitFireParticle(i);
            });
        }

        private void EmitFireParticle (int index)
        {
            if (_fireParticles[index])
            {
                _fireParticles[index].Play();
            }
        }

        public Color GetColor()
        {
            return Color;
        }
        public float GetRange()
        {
            MapController controller = MapController.Instance;
            if (controller != null)
            {
                return Mathf.Max(controller.Width, controller.Height) * 2f;
            }
            return 50f;
        }
    }
}