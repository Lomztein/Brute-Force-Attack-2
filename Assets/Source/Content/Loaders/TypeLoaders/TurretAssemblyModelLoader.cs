using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models.Turret;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Content.Loaders.TypeLoaders
{
    public class TurretAssemblyModelLoader : IRawContentTypeLoader
    {
        public Type ContentType => typeof(ITurretAssemblyModel);

        public object Load(string path)
        {
            TurretAssemblyModel model = new TurretAssemblyModel();
            model.Deserialize(DataSerialization.FromFile(path));
            return model;
        }
    }
}
