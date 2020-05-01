using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.Assemblers
{
    public class TurretAssembler : ITurretAssembler
    {
        public void Assemble(ITurretAssembly assembly)
        {
            ITurretComponent[] components = assembly.GetComponents();
            foreach (ITurretComponent component in components)
            {
                AssembleComponent(component);
            }
        }

        private void AssembleComponent (ITurretComponent component)
        {
            GameObject componentObject = (component as TurretComponent).gameObject;
            Type componentType = component.GetType();
            IEnumerable<FieldInfo> fields = componentType.GetFields().Where(x => x.IsDefined(typeof(TurretComponentAttribute)));
            foreach (FieldInfo field in fields)
            {
                field.SetValue(component, GetNearestComponent(componentObject, field.FieldType));
            }
        }

        private object GetNearestComponent(GameObject componentObject, Type type)
        {
            return componentObject.GetComponentInParent(type);
        }
    }
}
