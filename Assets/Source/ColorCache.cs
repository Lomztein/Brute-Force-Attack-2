using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2
{
    [Serializable]
    public class ColorCache
    {
        public string ColorName;
        private Color _cache;

        public Color Get()
        {
            if (_cache == null)
            {
                _cache = Color.GetColor(ColorName);
            }
            return _cache;
        }
    }
}
