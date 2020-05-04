using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Placement
{
    public interface IGridPlaceable : IPlaceable
    {
        Grid.Size Size { get; }
    }
}
