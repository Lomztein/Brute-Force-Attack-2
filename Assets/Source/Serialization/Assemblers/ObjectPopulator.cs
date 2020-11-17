﻿using Lomztein.BFA2.Serialization.Assemblers.Property;
using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public class ObjectPopulator
    {
        private IPropertyAssembler _propertyAssembler;

        public ObjectPopulator (IPropertyAssembler assembler)
        {
            _propertyAssembler = assembler;
        }

        public ObjectPopulator()
        {
            _propertyAssembler = new AllPropertyAssemblers();
        }

        public void Populate (object obj, ObjectModel model)
        {
            Type modelType = model.Type ?? obj.GetType();
            IEnumerable<FieldInfo> fields = GetModelFields(modelType);

            foreach (var field in fields)
            {
                var property = model.GetProperty(field.Name);
                if (property != null)
                {
                    var value = _propertyAssembler.Assemble(property, field.FieldType);
                    field.SetValue(obj, value);
                }
                else
                {
                    Debug.LogWarning($"Could not find property for field {field.Name} in {modelType.Name}. Value is defualt.");
                }
            }
        }

        public ObjectModel Extract (object obj)
        {
            Type objectType = obj.GetType();
            var properties = new List<ObjectField>();

            IEnumerable<FieldInfo> fields = GetModelFields(objectType);

            foreach (FieldInfo info in fields)
            {
                try
                {
                    object componentValue = info.GetValue(obj);
                    var model = _propertyAssembler.Disassemble(componentValue, info.FieldType);
                    if (componentValue != null && componentValue.GetType() != info.FieldType)
                    {
                        model.MakeExplicit();
                    }

                    properties.Add(new ObjectField(info.Name, model));
                }
                catch (Exception e)
                {
                    Debug.Log(objectType.Name + ": " + info.Name);
                    Debug.LogException(e);
                }

            }

            return new ObjectModel(objectType, properties.ToArray());
        }

        private IEnumerable<FieldInfo> GetModelFields(Type type)
        {
            BindingFlags _bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            return type.GetFields(_bindingFlags).Where(x => x.IsDefined(typeof(ModelPropertyAttribute), true));
        }
    }
}
