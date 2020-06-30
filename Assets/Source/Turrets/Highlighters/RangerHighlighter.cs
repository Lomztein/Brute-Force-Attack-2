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
        public GameObject RangeIndicator;
        public float ScaleMultiplier;

        private GameObject _instance;

        public override void EndHighlight()
        {
            Destroy(_instance);
            Destroy(gameObject);
        }

        public override void Highlight(IRanger component)
        {
            float range = component.GetRange();
            _instance = Instantiate(RangeIndicator, transform.position, transform.rotation, transform);
            _instance.transform.localScale = new Vector3(range, range) * ScaleMultiplier + Vector3.forward;
        }
    }
}
