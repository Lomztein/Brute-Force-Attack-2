using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Models
{
    public class ReferenceModel : ValueModel
    {
        public Guid ReferenceId { get; private set; }

        public ReferenceModel (Guid id)
        {
            ReferenceId = id;
        }

        public ReferenceModel()
        {
        }
    }
}
