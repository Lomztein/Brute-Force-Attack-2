using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Assets.Source.Utilities
{
    public static class StringUtils
    {
        public static string ExtractContent(string input, char sc, char ec)
        {
            int from = input.IndexOf(sc);
            int to = input.LastIndexOf(ec);
            string result = input.Substring(from + 1, to - from - 1);
            return result;
        }
    }
}
