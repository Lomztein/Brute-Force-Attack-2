using Lomztein.BFA2.Structures.Turrets.Rangers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ObjectVisualizers
{
    public class RangerVisualizer : ObjectVisualizerBase<IRanger>
    {
        public float ScaleMultiplier;

        private GameObject _instance;
        private IRanger _component;

        public override void Visualize(IRanger component)
        {
            _component = component;
            if (component is Component comp)
            {
                Follow(comp.transform);
            }
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
