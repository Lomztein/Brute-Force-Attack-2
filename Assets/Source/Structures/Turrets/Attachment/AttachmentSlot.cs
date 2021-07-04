using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets.Attachment
{
    
    [System.Serializable]
    public class AttachmentSlot
    {
        [ModelProperty]
        public AttachmentType Type;
        [ModelProperty]
        public Size MinSupportedSize;
        [ModelProperty]
        public Size MaxSupportedSize;
        [ModelProperty, SerializeField]
        public Vector3 LocalPosition;
        [ModelProperty, SerializeField]
        public float LocalAngle;
        public Attachment Attachment;

        public void Attach(IAttachable attachable)
            => Attachment = new Attachment(this, attachable);

        public bool CanSupport(AttachmentPoint point) => point.Size >= MinSupportedSize && point.Size <= MaxSupportedSize && Invert(point.Type) == Type;
        public bool IsEmpty() => Attachment == null || Attachment.Attachable == null;

        public static AttachmentType Invert (AttachmentType type)
        {
            if (type == AttachmentType.Upper || type == AttachmentType.Lower)
            {
                return type == AttachmentType.Upper ? AttachmentType.Lower : AttachmentType.Upper;
            }
            return type;
        }

        public Vector3 GetWorldPosition(Vector3 parentPosition, Quaternion parentRotation) => GetWorldRotation(parentRotation) * (parentPosition + LocalPosition);
        public Quaternion GetWorldRotation(Quaternion parentRotation) => parentRotation * Quaternion.Euler(0f, 0f, LocalAngle);
    }
}
