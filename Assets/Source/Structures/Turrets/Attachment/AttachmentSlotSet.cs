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

        public IEnumerable<AttachmentSlot> GetSlots() => _attachmentPoints;
        public IEnumerable<AttachmentSlot> GetSupportingSlots(AttachmentPoint other) => _attachmentPoints.Where(x => x.CanSupport(other));
        public IEnumerable<AttachmentSlot> GetSlotsOfType(AttachmentType type) => _attachmentPoints.Where(x => x.Type == type);
        public AttachmentSlot GetNearestSupportingSlot (AttachmentPoint forPoint, Transform parentTransform, Transform childTransform)
            => GetSupportingSlots(forPoint).FirstOrDefault(x => IsNearby(x, forPoint, parentTransform, childTransform));

        private bool IsNearby(AttachmentSlot slot, AttachmentPoint point, Transform parentTransform, Transform childTransform)
            => Vector2.Distance(slot.GetWorldPosition(parentTransform), point.GetWorldPosition(childTransform)) < 0.1f;
    }
}
