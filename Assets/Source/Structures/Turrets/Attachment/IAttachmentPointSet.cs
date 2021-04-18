using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets.Attachment
{
    public interface IAttachmentPointSet
    {
        IEnumerable<AttachmentPoint> GetPoints();
    }
}
