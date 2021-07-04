using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets.Attachment
{
    [Serializable]
    public class AttachmentSlotSet
    {
        [ModelProperty, SerializeField]
        private AttachmentSlot[] _attachmentPoints;

        public IEnumerable<AttachmentSlot> GetPoints() => _attachmentPoints;
        public IEnumerable<AttachmentSlot> GetSupportingPoints(AttachmentPoint other) => _attachmentPoints.Where(x => x.CanSupport(other));
        public IEnumerable<AttachmentSlot> GetPointsOfType(AttachmentType type) => _attachmentPoints.Where(x => x.Type == type);
    }
}
