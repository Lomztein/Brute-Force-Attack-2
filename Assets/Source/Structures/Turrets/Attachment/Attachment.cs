using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Structures.Turrets.Attachment
{
    public enum AttachmentType { Lower, Upper, Side, Extension }

    public class Attachment
    {
        public AttachmentSlot Slot;
        [ModelReference]
        public IAttachable Attachable;

        public Attachment(AttachmentSlot _slot, IAttachable attachable)
        {
            Slot = _slot;
            Attachable = attachable;
        }
    }
}
