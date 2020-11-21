using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Modifiers.EventMods;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
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
        [SerializeReference] private ValueModel _model;
        private object _cache;

        public void Assemble(ValueModel source)
        {
            _model = source;
        }

        public ValueModel Disassemble()
        {
            if (_model == null)
            {
                _model = new ObjectModel();
            }
            return _model;
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
            T value = (T)assembler.Assemble(_model, typeof(T));
            return value;
        }
    }
}
