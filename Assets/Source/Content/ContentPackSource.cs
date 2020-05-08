using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Content
{
    public class ContentPackSource : IContentPackSource
    {
        private IContentPackLoader _loader = new ContentPackLoader();
        private string[] Sources => new string[]
        {
            Paths.StreamingAssets + "Content/",
            Paths.Data + "Content/",
        };

        public IContentPack[] GetPacks()
        {
            List<IContentPack> packs = new List<IContentPack>();
            foreach (string source in Sources)
            {
                if (Directory.Exists(source))
                {
                    string[] directories = Directory.GetDirectories(source);
                    foreach (string directory in directories)
                    {
                        packs.Add(_loader.Load(directory));
                    }
                }
            }

            return packs.ToArray();
        }
    }
}
