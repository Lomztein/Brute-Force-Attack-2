using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content
{
    public class ContentManager : MonoBehaviour
    {
        private string[] Sources => new string[]
        {
            Paths.StreamingAssets + "Content/",
            Paths.Data + "Content/",
        };

        public void Init ()
        {
            IContentPack[] packs = InitializeContentPacks(Sources);
            foreach (IContentPack pack in packs)
            {
                Debug.Log(pack.ToString());
            }
        }

        // Loading of content packs should perhaps be offloaded to a ContentPackLoader
        private IContentPack[] InitializeContentPacks (string[] sources)
        {
            List<IContentPack> packs = new List<IContentPack>();
            foreach (string source in sources)
            {
                if (Directory.Exists(source))
                {
                    string[] directories = Directory.GetDirectories(source);
                    foreach (string directory in directories)
                    {
                        packs.Add(new ContentPack(directory, Path.GetFileName (directory), "Unauthored", ""));
                    }
                }
            }

            return packs.ToArray();
        }
    }
}
