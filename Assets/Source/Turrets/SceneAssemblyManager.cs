using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public class SceneAssemblyManager : MonoBehaviour
    {
        public static SceneAssemblyManager Instance;

        public event Action<ITurretAssembly> OnAssemblyAdded;
        public event Action<ITurretAssembly> OnAssemblyRemoved;

        public event Action<ITurretComponent> OnComponentAdded;
        public event Action<ITurretComponent> OnComponentRemoved;

        public ITurretAssembly[] Assemblies => _assemblies.ToArray();
        public ITurretComponent[] Components => _assemblies.SelectMany(x => x.GetComponents()).ToArray();

        private List<ITurretAssembly> _assemblies = new List<ITurretAssembly>();

        private void Awake()
        {
            Instance = this;
        }

        public void AddAssembly (ITurretAssembly assembly)
        {
            _assemblies.Add(assembly);
            OnAssemblyAdded?.Invoke(assembly);
        }

        public void AddComponent (ITurretComponent component)
        {
            OnComponentAdded?.Invoke(component);
        }

        public void RemoveAssembly (ITurretAssembly assembly)
        {
            _assemblies.Remove(assembly);
            OnAssemblyRemoved?.Invoke(assembly);
        }

        public void RemoveComponent(ITurretComponent component)
        {
            OnComponentRemoved?.Invoke(component);
        }
    }
}
