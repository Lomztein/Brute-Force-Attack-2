using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Models
{
    public class PathModel : ValueModel
    {
        public string Path { get; private set; }

        public PathModel(string path)
        {
            Path = path;
        }

        public PathModel()
        {
        }
    }
}
