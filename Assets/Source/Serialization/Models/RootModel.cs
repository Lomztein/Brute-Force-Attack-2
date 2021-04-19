using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Models
{
    public class RootModel
    {
        public ValueModel Root { get; private set; }
        public ArrayModel Shared { get; private set; }

        public RootModel(ValueModel root, ArrayModel shared)
        {
            Root = root;
            Shared = shared;
        }

        public RootModel(ValueModel root) : this(root, new ArrayModel())
        {
        }

        public bool HasSharedReferences => Shared.Length != 0;
    }
}
