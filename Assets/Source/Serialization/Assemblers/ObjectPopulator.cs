using Lomztein.BFA2.Serialization.Assemblers.PropertyAssembler;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public class ObjectPopulator
    {
        private static IEnumerable<PropertyAssemblerBase> _propertyAssemblers;

        private static IEnumerable<PropertyAssemblerBase> GetPropertyAssemblers ()
        {
            if (_propertyAssemblers == null)
            {
                _propertyAssemblers = ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<PropertyAssemblerBase>();
            }
            return _propertyAssemblers;
        }

        private PropertyAssemblerBase GetPropertyAssembler(Type attributeType) => GetPropertyAssemblers().First(x => x.AttributeType == attributeType);

        public void Populate (object obj, ObjectModel model, AssemblyContext context)
        {
            Type modelType = obj.GetType();
            IEnumerable<IAssignableMemberInfo> fields = GetModelFields(modelType);

            foreach (IAssignableMemberInfo field in fields)
            {
                var property = model.GetProperty(field.Name);
                if (property != null)
                {
                    GetPropertyAssembler(field.AttributeType).Assemble(obj, field, property, field.ValueType, context);
                }
            }

            context.MakeReferencable(obj, model.Guid);
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
                    ObjectField objectField = new ObjectField();
                    objectField.Name = info.Name;

                    object memberValue = info.GetValue(obj);

                    if (memberValue != null)
                    {
                        GetPropertyAssembler(info.AttributeType).Disassemble(objectField, memberValue, info.ValueType, context);

                        if (memberValue.GetType() != info.ValueType)
                        {
                            objectField.Model.MakeExplicit(memberValue.GetType());
                        }
                    }
                    else
                    {
                        objectField.Model = new NullModel();
                    }

                    properties.Add(objectField);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }

            }

            return context.MakeReferencable (obj, new ObjectModel(properties.ToArray())) as ObjectModel;
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

        public interface IAssignableMemberInfo
        {
            string Name { get; }
            Type ValueType { get; }
            Type AttributeType { get; }

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
            public Type AttributeType => _info.GetCustomAttribute<ModelPropertyAttribute>().GetType();

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
            public Type AttributeType => _info.GetCustomAttribute<ModelPropertyAttribute>().GetType();

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
