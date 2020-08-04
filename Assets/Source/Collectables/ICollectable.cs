using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Collectables
{
    public interface ICollectable
    {
        float CollectionTime { get; }

        void Collect();
    }
}
