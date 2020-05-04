using Lomztein.BFA2.Purchasing.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Purchasing
{
    public interface IPurchasable
    {
        string Name { get; }
        string Description { get; }
        IResourceCost Cost { get; }
        Sprite Sprite { get; }
    }
}
