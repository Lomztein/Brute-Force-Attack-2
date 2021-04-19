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
        private IValueAssembler _propertyAssembler;

        public ObjectPopulator (IValueAssembler assembler)
        {
            _propertyAssembler = assembler;
        }

        public ObjectPopulator()
        {
            _propertyAssembler = new ValueAssembler();
        }

        public void Populate (object obj, ObjectModel model, AssemblyContext context)
        {
            Type modelType = obj.GetType();
            IEnumerable<IAssignableMemberInfo> fields = GetModelFields(modelType);

            foreach (IAssignableMemberInfo field in fields)
            {
                var property = model.GetProperty(field.Name);
                if (property != null)
                {
                    var value = _propertyAssembler.Assemble(property, field.ValueType, context);
                    field.SetValue(obj, value);
                }
                else
                {
                    Debug.LogWarning($"Could not find property for field {field.Name} in {modelType.Name}. Value is defualt.");
                }
            }
        }

        public ObjectModel Extract (object obj, DisassemblyContext context)
        {
            Type objectType = obj.GetType();
            var properties = new List<ObjectField>();

            IEnumerable<IAssignableMemberInfo> fields = GetModelFields(objectType);

            foreach (IAssignableMemberInfo info in fields)
            {
                try
                {
                    object componentValue = info.GetValue(obj);
                    var model = _propertyAssembler.Disassemble(componentValue, info.ValueType, context);
                    if (componentValue != null && componentValue.GetType() != info.ValueType)
                    {
                        model.MakeExplicit(componentValue.GetType());
                    }

                    properties.Add(new ObjectField(info.Name, model));
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }

            }

            return new ObjectModel(properties.ToArray());
        }

        private IEnumerable<IAssignableMemberInfo> GetModelFields(Type type)
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            var fields = type.GetFields(bindingFlags).Where(x => x.IsDefined(typeof(ModelPropertyAttribute), true));
            var properties = type.GetProperties(bindingFlags).Where(x => x.IsDefined(typeof(ModelPropertyAttribute), true));
            
            foreach (var field in fields)
            {
                yield return CreateMemberInfo(field);
            }

            foreach (var property in properties)
            {
                yield return CreateMemberInfo(property);
            }

            // That CreateMemberInfo factory method didn't really end up neccesary.
        }

        private interface IAssignableMemberInfo
        {
            string Name { get; }
            Type ValueType { get; }

            object GetValue(object obj);

            void SetValue(object obj, object value);
        }

        private class FieldAssignableMemberInfo : IAssignableMemberInfo
        {
            private readonly FieldInfo _info;

            public FieldAssignableMemberInfo (FieldInfo info)
            {
                _info = info;
            }

            public string Name => _info.Name;
            public Type ValueType => _info.FieldType;

            public object GetValue(object obj)
            {
                return _info.GetValue(obj);
            }

            public void SetValue(object obj, object value)
            {
                _info.SetValue(obj, value);
            }
        }

        private class PropertyAssignableMemberInfo : IAssignableMemberInfo
        {
            private readonly PropertyInfo _info;

            public PropertyAssignableMemberInfo(PropertyInfo info)
            {
                _info = info;
            }

            public string Name => _info.Name;
            public Type ValueType => _info.PropertyType;

            public object GetValue(object obj)
            {
                return _info.GetValue(obj);
            }

            public void SetValue(object obj, object value)
            {
                _info.SetValue(obj, value);
            }
        }

        private static IAssignableMemberInfo CreateMemberInfo (MemberInfo info)
        {
            if (info is FieldInfo field)
                return new FieldAssignableMemberInfo(field);
            if (info is PropertyInfo property)
                return new PropertyAssignableMemberInfo(property);
            return null;
        }
    }
}
