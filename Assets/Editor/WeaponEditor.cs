using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lomztein.BFA2.Structures.Turrets.Weapons;
using UnityEditor;
using Lomztein.BFA2.Weaponary;

namespace Lomztein.BFA2
{
    public class WeaponEditorBase<T> : UnityEditor.Editor where T : class
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            TurretWeaponBase weapon = target as TurretWeaponBase;
            float dps = ComputeDPS(weapon);
            EditorGUILayout.LabelField("DPS: " + dps);
            if (weapon.BasePierce > 0f)
            {
                EditorGUILayout.LabelField("Pierce DPS: " + ComputePierceDPS(weapon));
            }
        }

        private float ComputeDPS (TurretWeaponBase weapon)
            => weapon.BaseDamage * weapon.BaseFirerate * weapon.BaseProjectileAmount * weapon.GetComponent<IWeapon>().MuzzleCount;

        private float ComputePierceDPS (TurretWeaponBase weapon)
        {
            float dps = ComputeDPS(weapon);
            float res = dps;
            float prev = 0;
            for (int i = 0; i < 20; i++)
            {
                float iterDps = dps * Mathf.Pow(weapon.BasePierce, i);
                if (Mathf.Abs(iterDps - prev) < 0.1f)
                    break;
                res += iterDps;
            }
            return res;
        }
    }


    [CustomEditor(typeof(TurretWeapon))]
    public class TurretWeaponEditor : WeaponEditorBase<TurretWeapon>
    {
    }

    [CustomEditor(typeof(FireOnEventTurretWeapon))]
    public class FireOnEventTurretWeaponEditor : WeaponEditorBase<FireOnEventTurretWeapon>
    {
    }
}
