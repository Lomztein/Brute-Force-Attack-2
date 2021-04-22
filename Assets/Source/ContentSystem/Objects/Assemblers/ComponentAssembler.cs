using Lomztein.BFA2.ContentSystem.Assemblers.EngineComponent;
using Lomztein.BFA2.ContentSystem.Objects.Assemblers.ComponentConverters;
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

namespace Lomztein.BFA2.ContentSystem.Assemblers
{
    public class ComponentAssembler
    {

        private static IEnumerable<IEngineComponentAssembler> _engineSerializers;
        private static IEnumerable<IComponentConverter> _converters;

        public ComponentAssembler ()
        {
            if (_engineSerializers == null)
            {
                _engineSerializers = ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<IEngineComponentAssembler>();
            }
            if (_converters == null)
            {
                _converters = ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<IComponentConverter>();
            }
        }

        private IEngineComponentAssembler GetEngineComponentAssembler(Type type) => _engineSerializers.FirstOrDefault(x => x.Type == type);
        private IComponentConverter GetComponentConverter (Type type) => _converters.FirstOrDefault(x => x.CanConvert (type));

        public void Assemble (ObjectModel model, GameObject target, AssemblyContext context)
        {
            var serializer = GetEngineComponentAssembler(model.GetModelType());
            if (serializer != null)
            {
                Component comp = target.GetComponent(model.GetModelType());
                if (comp == null)
                {
                     comp = target.AddComponent(model.GetModelType());
                }

                serializer.Assemble(model, comp, context);
                context.MakeReferencable(comp, model.Guid);
                return;
            }

            Component component = target.AddComponent(model.GetModelType());
            ObjectPopulator populator = new ObjectPopulator();
            populator.Populate(component, model, context);
        }
        
        public ObjectModel Disassemble(Component component, DisassemblyContext context)
        {
            var converter = GetComponentConverter(component.GetType());
            if (converter != null)
            {
                Component converted = converter.ConvertComponent(component, component.gameObject);
                return context.MakeReferencable(component, (ObjectModel)Disassemble(converted, context).MakeExplicit(converted.GetType()));
            }

            var assembler = GetEngineComponentAssembler(component.GetType());
            if (assembler != null)
            {
                return context.MakeReferencable(component, (ObjectModel)assembler.Disassemble(component, context).MakeExplicit(component.GetType())); // Components are always explicit.
            }

            ObjectPopulator populator = new ObjectPopulator();
            return (ObjectModel)populator.Extract(component, context).MakeExplicit(component.GetType());
        }
    }
}
