using Lomztein.BFA2.World;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Content.Loaders.ContentLoaders
{
    public class MapDataLoader : IRawContentTypeLoader
    {
        public Type ContentType => typeof (MapData);

        public object Load(string path)
        {
            string text = File.ReadAllText(path);
            JToken token = JToken.Parse(text);
            MapData mapData = new MapData();
            mapData.Deserialize(token);
            return mapData;
        }
    }
}
