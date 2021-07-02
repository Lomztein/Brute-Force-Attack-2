using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.FacadeComponents
{
    public abstract class SceneFacadeSubComponent<T> : FacadeSubComponent<T> where T : SceneFacadeComponent
    {
        public override void Init()
        {
            GetParentComponent().OnSceneLoaded += OnSceneLoaded;
            GetParentComponent().OnSceneUnloaded += OnSceneUnloaded;
        }

        public abstract void OnSceneLoaded();
        public abstract void OnSceneUnloaded();
    }
}
