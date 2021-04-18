using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets.Attachment
{
    [Serializable]
    public class AttachmentPointSet : IAttachmentPointSet
    {
        [ModelProperty]
        public AttachmentPoint[] Points;

        public IEnumerable<AttachmentPoint> GetPoints()
        {
            return Points;
        }
    }
}
