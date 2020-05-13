using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Content
{
    public class ContentPackInfo : ISerializable
    {
        public string Name;
        public string Description;
        public string Author;
        public string Version;

        public void Deserialize(JToken data)
        {
            Name = data["Name"].ToString();
            Description = data["Description"].ToString();
            Author = data["Author"].ToString();
            Version = data["Version"].ToString();
        }

        public JToken Serialize()
        {
            return JToken.FromObject(this);
        }
    }
}
