using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.DataStruct;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Content
{
    public class ContentPackInfo : ISerializable
    {
        public string Name;
        public string Description;
        public string Author;
        public string Version;

        public void Deserialize(IDataStruct data)
        {
            Name = data.GetValue<string>("Name");
            Description = data.GetValue<string>("Description");
            Author = data.GetValue<string>("Description");
            Version = data.GetValue<string>("Version");
        }

        public IDataStruct Serialize()
        {
            return new JsonDataStruct(JToken.FromObject(this));
        }
    }
}
