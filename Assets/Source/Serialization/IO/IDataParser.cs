using Lomztein.BFA2.Serialization.DataStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.IO
{
    public interface IDataParser
    {
        string FileExtension { get; }

        IDataStruct FromString (string input);
    }
}
