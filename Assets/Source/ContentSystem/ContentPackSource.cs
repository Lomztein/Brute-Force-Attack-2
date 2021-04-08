using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.ContentSystem
{
    public class ContentPackSource : IContentPackSource
    {
        private IContentPackLoader _loader = new ContentPackLoader();
        private string[] Sources => new string[]
        {
            Paths.StreamingAssets + "Content/", // Built-in content.
            Paths.Data + "Content/", // Downloaded content.
            Paths.PersistantData + "Content/", // User content.
        };

        public IEnumerable<IContentPack> GetPacks()
        {
            List<IContentPack> packs = new List<IContentPack>();
            packs.Add(new ResourcesContentPack());

            foreach (string source in Sources)
            {
                if (Directory.Exists(source))
                {
                    string[] directories = Directory.GetDirectories(source);
                    foreach (string directory in directories)
                    {
                        IContentPack pack = _loader.Load(directory);
                        pack.Init();

                        packs.Add(pack);
                    }
                }
            }

            return packs;
        }
    }
}
