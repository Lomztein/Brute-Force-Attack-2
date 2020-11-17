using Lomztein.BFA2.Content.Assemblers.EngineComponent;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content.Assemblers
{
    public class ComponentAssembler
    {

        private static IEnumerable<IEngineComponentAssembler> _engineSerializers;

        public ComponentAssembler ()
        {
            if (_engineSerializers == null)
            {
                _engineSerializers = ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<IEngineComponentAssembler>();
            }
        }

        private IEngineComponentAssembler GetEngineComponentAssembler(Type type) => _engineSerializers.FirstOrDefault(x => x.Type == type);

        public void Assemble (ObjectModel model, GameObject target)
        {
            var serializer = GetEngineComponentAssembler(model.Type);
            if (serializer != null)
            {
                Component comp = target.GetComponent(model.Type);
                if (comp == null)
                {
                     comp = target.AddComponent(model.Type);
                }

                serializer.Assemble(model, comp);
                return;
            }

            Component component = target.AddComponent(model.Type);
            ObjectPopulator populator = new ObjectPopulator();
            populator.Populate(component, model);
        }
        
        public ObjectModel Disassemble(Component component)
        {
            var assembler = GetEngineComponentAssembler(component.GetType());
            if (assembler != null)
            {
                return assembler.Disassemble(component).MakeExplicit(); // Components are always explicit.
            }

            ObjectPopulator populator = new ObjectPopulator();
            return populator.Extract(component).MakeExplicit();
        }
    }
}
