using Lomztein.BFA2.Structures.Turrets.Weapons;
using Lomztein.BFA2.Weaponary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Displays.Stats.Elements
{
    public class DPSStatSheetElement : StatSheetElementBase
    {
        public bool IncludeInactive;

        public override bool UpdateDisplay(GameObject target)
        {
            float dps = ComputeDPS(target);
            if (dps > 0.01)
            {
                SetText($"{Mathf.RoundToInt(dps)} DPS");
                gameObject.SetActive(true);
                return true;
            }
            else
            {
                gameObject.SetActive(false);
                return false;
            }
        }

        private float ComputeDPS (GameObject root)
        {
            IWeapon[] weapons = root.GetComponentsInChildren<IWeapon>(IncludeInactive);
            return weapons.Sum(x => x.Damage * x.Firerate * x.ProjectileAmount * x.MuzzleCount);
        }
    }
}
