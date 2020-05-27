using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models.Turret;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Content.Loaders.ContentLoaders
{
    public class TurretAssemblyRawLoader : IRawContentTypeLoader
    {
        public Type ContentType => typeof(ContentCachedTurretAssemblyPrefab);

        public object Load(string path)
        {
            TurretAssemblyModel model = new TurretAssemblyModel();
            model.Deserialize(DataSerialization.FromFile(path));
            return new ContentCachedTurretAssemblyPrefab (model);
        }
    }
}
