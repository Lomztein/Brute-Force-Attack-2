using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models.Property;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Models
{
    public class ObjectModel : IEnumerable<ObjectField>
    {
        public Type Type { get; private set; }

        /// <summary>
        /// A property type is considered implicit if the type can be fetched from somewhere else, such as a field or array during assembly.
        /// If it cannot be fetched from somewhere else, for instance if it is a subtype, then it must be explicit.
        /// </summary>
        public bool ImplicitType { get; private set; } = true;
        public virtual bool IsNull { get; private set; }

        private List<ObjectField> _properties = new List<ObjectField>();

        public ObjectModel()
        {
        }

        public ObjectModel(Type type) 
        {
            Type = type;
        }

        public ObjectModel MakeImplicit ()
        {
            ImplicitType = true;
            return this;
        }

        public ObjectModel MakeExplicit()
        {
            ImplicitType = false;
            return this;
        }

        public ObjectModel(Type type, params ObjectField[] properties)
        {
            Type = type;
            _properties = properties.ToList();
        }

        public static ObjectModel NullObject() =>
            new ObjectModel(null) { IsNull = true };

        public void Add (string name, object value)
        {
            _properties.Add(new ObjectField(name, PropertyModelFactory.Create(value)));
        }

        public ObjectModel(Type type, ObjectModel baseModel, params ObjectField[] properties) : this(type, properties)
        {
            _properties.AddRange(baseModel.GetProperties());
        }

        public ObjectField[] GetProperties() => _properties.ToArray();

        public IEnumerator<ObjectField> GetEnumerator()
        {
            return ((IEnumerable<ObjectField>)_properties).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ObjectField>)_properties).GetEnumerator();
        }

        public ObjectField GetField(string name)
            => GetProperties().FirstOrDefault(x => x.Name == name);

        public PropertyModel GetProperty(string name)
        {
            var field = GetField(name);
            return GetField(name).Model;
        }

        public T GetProperty<T>(string name) where T : PropertyModel
            => (T)GetProperty(name);

        public T GetValue<T>(string name)
        {
            var field = GetField(name);

            if (field == null)
            {

            }

            PrimitivePropertyModel property = field.Model as PrimitivePropertyModel;
            return property.ToObject<T>();
        }

        public ArrayPropertyModel GetArray(string name)
            => GetProperty<ArrayPropertyModel>(name);

        public ObjectModel GetObject(string name)
            => GetProperty<ComplexPropertyModel>(name).Model as ObjectModel;
    }
}
