﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models.Property;

namespace Lomztein.BFA2.Serialization.Assemblers.Property
{
    public class ComplexPropertyAssembler : IPropertyAssembler
    {
        private ObjectAssembler _internalAssembler = new ObjectAssembler();

        public bool CanAssemble(Type type) => IsComplex(type);

        public static bool IsComplex(Type type)
            => !type.IsPrimitive && type != typeof(string) && !type.IsEnum;

        public object Assemble(IPropertyModel model, Type type) => _internalAssembler.Assemble((model as ComplexPropertyModel).Model);

        public IPropertyModel Disassemble(object value) => new ComplexPropertyModel(_internalAssembler.Disassemble(value));
    }
}
