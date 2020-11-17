using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models.Turret;
using Lomztein.BFA2.Serialization.Serializers.Turret;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.ContentSystem.Loaders.ContentLoaders
{
    public class TurretAssemblyLoaderStrategy : IContentLoaderStrategy
    {
        public bool CanLoad (Type type) => typeof(ContentCachedTurretAssemblyPrefab).IsAssignableFrom(type);

        public object Load(string path, Type type)
        {
            var data = DataSerialization.FromFile(path);
            TurretAssemblyModelSerializer serializer = new TurretAssemblyModelSerializer();
            ITurretAssemblyModel model = serializer.Deserialize(data);
            return new ContentCachedTurretAssemblyPrefab (model);
        }
    }
}
