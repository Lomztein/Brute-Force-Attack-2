using Lomztein.BFA2.Serialization.Models.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Models
{
    public interface IObjectModel
    {
        Type Type { get; }

        ObjectField[] GetProperties();
    }

    public static class ObjectModelExtensions
    {
        public static ObjectField GetField(this IObjectModel model, string name)
            => model.GetProperties().FirstOrDefault(x => x.Name == name);

        public static IPropertyModel GetProperty(this IObjectModel model, string name)
            => GetField(model, name).Model;

        public static T GetProperty<T>(this IObjectModel model, string name) where T : IPropertyModel
            => (T)GetProperty(model, name);

        public static T GetValue<T>(this IObjectModel model, string name)
        {
            ValuePropertyModel property = GetField(model, name).Model as ValuePropertyModel;
            return property.GetValue<T>();
        }

        public static ArrayPropertyModel GetArray(this IObjectModel model, string name)
            => GetField(model, name).Model as ArrayPropertyModel;

        public static IObjectModel GetObject(this IObjectModel model, string name)
            => GetField(model, name).Model as IObjectModel;
    }
}
