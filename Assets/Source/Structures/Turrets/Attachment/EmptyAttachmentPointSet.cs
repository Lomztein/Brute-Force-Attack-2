using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets.Attachment
{
    public class EmptyAttachmentPointSet : IAttachmentPointSet
    {
        public AttachmentPoint[] GetPoints()
        {
            return new AttachmentPoint[0];
        }
    }
}
