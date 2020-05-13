using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.IO
{
    public static class DataSerialization
    {
        public static JToken FromFile (string path)
        {
            string contents = File.ReadAllText(path);
            return JToken.Parse(contents); 
        }
    }
}
