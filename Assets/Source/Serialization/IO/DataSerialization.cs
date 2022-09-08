using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Assertions;

namespace Lomztein.BFA2.Serialization.IO
{
    public static class DataSerialization
    {
        public static JToken FromFile (string path, IEnumerable<string> patches = null)
        {
            var baseToken = JToken.Parse(File.ReadAllText(path));
            if (patches == null)
            {
                return baseToken;
            }

            Assert.IsTrue(baseToken is JContainer, "Only JContainers can be merged.");
            var container = baseToken as JContainer;

            var patchTokens = patches.Select(x => JToken.Parse(File.ReadAllText(x)));
            foreach (var patchToken in patchTokens)
            {
                container.Merge(patchToken);
            }

            return baseToken;
        }
    }
}
