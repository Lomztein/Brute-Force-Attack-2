using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Content
{
    public class ContentPackInfo
    {
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Description;
        [ModelProperty]
        public string Author;
        [ModelProperty]
        public string Version;
    }
}
