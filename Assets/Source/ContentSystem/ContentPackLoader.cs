using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Plugins;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Serializers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public class ContentPackLoader : IContentPackLoader
    {
        private const string ABOUT_FILE = "About.json";
        private const string IMAGE_FILE = "Image.png";

        public IContentPack Load(string path)
        {
            ContentPackInfo info = new ContentPackInfo();
            Texture2D texture = new Texture2D(2, 2);

            try
            {
                JToken data = DataSerialization.FromFile(Path.Combine(path, ABOUT_FILE));
                info = ObjectPipeline.BuildObject<ContentPackInfo>(data);

                if (File.Exists(Path.Combine(path, IMAGE_FILE))) {
                    texture.LoadImage(File.ReadAllBytes(Path.Combine(path, IMAGE_FILE)));
                    texture.filterMode = FilterMode.Point; // TODO: Allow content packs to decide this individually.
                }
                else
                {
                    texture = null;
                }
            }
            catch (FileNotFoundException)
            {
                info.Name = Path.GetFileName(path);
                info.Author = "Unknown Author";
                info.Version = "Unknown Version";
                info.Description = "Unknown Description";

                File.WriteAllText(Path.Combine(path, ABOUT_FILE), ObjectPipeline.UnbuildObject(info, true).ToString());
            }

            return new ContentPack(path + "/", info.Name, info.Author, info.Description, info.Version, texture);
        }
    }
}
