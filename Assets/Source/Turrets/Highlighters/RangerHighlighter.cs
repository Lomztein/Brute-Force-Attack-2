using Lomztein.BFA2.Turrets.Rangers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.Highlighters
{
    public class RangerHighlighter : HighlighterBase<IRanger>
    {
        public float ScaleMultiplier;

        private GameObject _instance;
        private IRanger _component;

        public override void Highlight(IRanger component)
        {
            _component = component;
            _instance = transform.GetChild(0).gameObject;
        }

        public override void Tick(float deltaTime)
        {
            float range = _component.GetRange();
            _instance.transform.localScale = new Vector3(range, range) * ScaleMultiplier + Vector3.forward;
        }

        public override void Tint(Color color)
        {
            _instance.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
