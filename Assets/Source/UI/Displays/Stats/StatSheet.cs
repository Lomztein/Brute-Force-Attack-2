using Lomztein.BFA2.UI.Displays.Stats.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Displays.Stats
{
    public class StatSheet : MonoBehaviour
    {
        public GameObject Target;
        public bool AutoUpdate;
        private List<IStatSheetElement> _elements = new List<IStatSheetElement>();

        private void Start()
        {
            _elements.AddRange(GetComponentsInChildren<IStatSheetElement>());
        }

        public void AddStatSheetElements (params IStatSheetElement[] elements)
        {
            foreach (IStatSheetElement element in elements)
            {
                _elements.Add(element);
                if (element is Component component)
                {
                    component.transform.SetParent(transform);
                }
            }
        }

        public void SetTarget (GameObject newTarget)
        {
            Target = newTarget;
            ForceUpdate();
        }

        public void ForceUpdate ()
        {
            if (Target)
            {
                foreach (IStatSheetElement element in _elements)
                {
                    element.UpdateDisplay(Target);
                }
            }
        }

        private void Update()
        {
            if (AutoUpdate)
            {
                ForceUpdate();
            }
        }
    }
}
