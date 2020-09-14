﻿using Lomztein.BFA2.Serialization.Models.Property;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Assemblers.Property
{
    public class AllPropertyAssemblers : IPropertyAssembler
    {
        private static List<IPropertyAssembler> _assemblers;

        public AllPropertyAssemblers ()
        {
            if (_assemblers == null)
            {
                _assemblers = new List<IPropertyAssembler>();
                _assemblers = ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<IPropertyAssembler>(typeof(AllPropertyAssemblers), typeof (ObjectPropertyAssembler), typeof (ValuePropertyAssembler)).ToList();
                _assemblers.Add(new ObjectPropertyAssembler());
                _assemblers.Add(new ValuePropertyAssembler());
            }
        }

        public object Assemble(IPropertyModel model, Type type)
        {
            return GetAssembler(type).Assemble(model, type);
        }

        public IPropertyModel Disassemble(object obj, Type type)
        {
            return GetAssembler(type).Disassemble(obj, type);
        }

        public bool CanAssemble(Type type)
        {
            return GetAssembler(type) != null;
        }

        private IPropertyAssembler GetAssembler(Type type)
            => _assemblers.First(x => x.CanAssemble(type));
    }
}
