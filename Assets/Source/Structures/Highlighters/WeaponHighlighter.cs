using Lomztein.BFA2.Weaponary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Highlighters
{
    public class WeaponHighlighter : HighlighterBase<IWeapon>
    {
        public LineRenderer LeftSide;
        public LineRenderer RightSide;

        private IWeapon _component;

        // Could be refactored so that Highlight merely assigns the component to a varaible, while all the highlighting is done in Tick().
        public override void Highlight(IWeapon component)
        {
            _component = component;
        }

        public override void Tick(float deltaTime)
        {
            float spread = _component.Spread;
            float range = _component.Range;

            Vector3 l = new Vector3(1f, Mathf.Sin(Mathf.Deg2Rad * -spread), 0f) * range;
            Vector3 r = new Vector3(1f, Mathf.Sin(Mathf.Deg2Rad * spread), 0f) * range;

            LeftSide.SetPosition(0, Vector3.zero);
            RightSide.SetPosition(0, Vector3.zero);

            LeftSide.SetPosition(1, l);
            RightSide.SetPosition(1, r);
        }

        public override void Tint(Color color)
        {
            LeftSide.material.color = color;
            RightSide.material.color = color;
        }
    }
}
