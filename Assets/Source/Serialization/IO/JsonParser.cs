using Lomztein.BFA2.Serialization.DataStruct;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.IO
{
    public class JsonParser : IDataParser
    {
        public string FileExtension => ".json";

        public IDataStruct FromString(string input)
        {
            return new JsonDataStruct (JToken.Parse(input));
        }
    }
}
