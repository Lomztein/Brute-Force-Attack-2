using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Content.Loaders.ResourceLoaders
{
    public interface IResourceTypeConverter
    {
        Type InputType { get; }
        Type OutputType { get; }

        object Convert(object input);
    }
}
