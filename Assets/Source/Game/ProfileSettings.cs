using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI.Style;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Game
{
    public class ProfileSettings
    {
        [ModelProperty]
        public UIStyle UIStyle = UIStyle.Default;
    }
}
