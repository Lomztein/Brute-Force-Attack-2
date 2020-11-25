using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Modifiers.EventMods;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.References
{
    [Serializable]
    public class EmbeddedValueModel : IAssemblable
    {
        [SerializeReference] public ValueModel Model;
        private object _cache;

        public void Assemble(ValueModel source)
        {
            ObjectModel model = source as ObjectModel;
            Model = model.GetProperty("EmbeddedModel");
        }

        public ValueModel Disassemble()
        {
            if (Model == null)
            {
                Model = new ObjectModel();
            }

            return new ObjectModel(
                new ObjectField("EmbeddedModel", Model));
        }

        public T GetCache<T> ()
        {
            if (_cache == null)
            {
                _cache = GetNew<T>();
            }

            return (T)_cache;
        }

        public T GetNew<T> ()
        {
            ValueAssembler assembler = new ValueAssembler();
            T value = (T)assembler.Assemble(Model, typeof(T));
            return value;
        }
    }
}
