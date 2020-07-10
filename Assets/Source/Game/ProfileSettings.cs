using Lomztein.BFA2.UI.Style;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Game
{
    public class ProfileSettings : ISerializable
    {
        public UIStyle UIStyle = UIStyle.Default;

        public void Deserialize(JToken source)
        {
            UIStyle = new UIStyle();
            UIStyle.Deserialize(source["UIStyle"]);
        }

        public JToken Serialize()
        {
            return new JObject()
            {
                { "UIStyle", UIStyle.Serialize() }
            };
        }
    }
}
