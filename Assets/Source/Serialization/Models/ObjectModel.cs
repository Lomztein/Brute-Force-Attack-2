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
    public class ObjectModel : IObjectModel, IEnumerable<ObjectField>
    {
        public Type Type { get; private set; }
        public bool IsTypeImplicit => Type == null;

        private List<ObjectField> _properties = new List<ObjectField>();

        public ObjectModel(Type type) 
        {
            Type = type;
        }

        public ObjectModel(Type type, params ObjectField[] properties)
        {
            Type = type;
            _properties = properties.ToList();
        }

        public ObjectModel(params ObjectField[] properties)
        {
            _properties = properties.ToList();
        }

        public void Add (string name, object value)
        {
            _properties.Add(new ObjectField(name, PropertyModelFactory.Create(value)));
        }

        public ObjectModel(Type type, IObjectModel baseModel, params ObjectField[] properties) : this(type, properties)
        {
            _properties.AddRange(baseModel.GetProperties());
        }

        public ObjectModel(IObjectModel baseModel, params ObjectField[] properties) : this(null, baseModel, properties)
        {
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
    }
}
