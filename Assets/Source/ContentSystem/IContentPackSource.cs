using System.Collections.Generic;

namespace Lomztein.BFA2.ContentSystem
{
    public interface IContentPackSource
    {
        IEnumerable<IContentPack> GetPacks();
    }
}