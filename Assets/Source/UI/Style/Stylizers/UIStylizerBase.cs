using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Style.Stylizers
{
    public abstract class UIStylizerBase<T> : MonoBehaviour, IUIStylizer where T : Component
    {
        protected T GetGraphic() => GetComponent<T>();
        protected UIStyleController GetController() => GetComponentInParent<UIStyleController>();

        public abstract void ApplyStyle(UIStyle style);

        public void ApplyStyle()
            => ApplyStyle(GetController().GetCurrentStyle());

        public void Start()
        {
            ApplyStyle();
        }
    }
}
