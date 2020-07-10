using Lomztein.BFA2.UI.Style;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Content.Loaders.ContentLoaders
{
    public class UIStyleRawLoader : IRawContentTypeLoader
    {
        public Type ContentType => typeof (UIStyle);

        public object Load(string path)
        {
            JToken token = JToken.Parse(File.ReadAllText(path));
            UIStyle style = new UIStyle();
            style.Deserialize(token);
            return style;
        }
    }
}
