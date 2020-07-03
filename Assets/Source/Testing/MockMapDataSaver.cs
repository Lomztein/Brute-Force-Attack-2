using Lomztein.BFA2.World;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Testing
{
    public class MockMapDataSaver : MonoBehaviour
    {
        public string Path;

        public void Save ()
        {
            MapData data = new MapData("Classic", "A completely open field ready for war!", 25, 30);

            JToken token = data.Serialize();
            File.WriteAllText(Application.streamingAssetsPath + Path, token.ToString());
        }
    }
}
