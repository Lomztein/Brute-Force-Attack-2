using Lomztein.BFA2.Serialization.DataStruct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.IO
{
    public static class DataParse
    {
        private static readonly IDataParser[] Parsers = new IDataParser[]
        {
            new JsonParser (),
        };

        private static IDataParser GetParser (string path)
        {
            string extension = Path.GetExtension(path);
            return Parsers.First(x => x.FileExtension == extension);
        }

        public static IDataStruct FromFile (string path)
        {
            IDataParser parser = GetParser(path);
            return parser.FromFile(path);
        }
    }
}
