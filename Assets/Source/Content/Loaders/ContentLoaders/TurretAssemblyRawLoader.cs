using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models.Turret;
using Lomztein.BFA2.Serialization.Serializers.Turret;
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
            var data = DataSerialization.FromFile(path);
            TurretAssemblyModelSerializer serializer = new TurretAssemblyModelSerializer();
            ITurretAssemblyModel model = serializer.Deserialize(data);
            return new ContentCachedTurretAssemblyPrefab (model);
        }
    }
}
