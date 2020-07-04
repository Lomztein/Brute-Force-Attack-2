using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Placement
{
    public interface IGridObject
    {
        Grid.Size Width { get; }
        Grid.Size Height { get; }
    }
}
