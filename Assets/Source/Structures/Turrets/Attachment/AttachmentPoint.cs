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
    public class AttachmentPoint
    {
        [ModelProperty]
        public Vector2 LocalPosition;
        public Size Size;

        public AttachmentPoint AttachedPoint { get; private set; }
        public bool IsEmpty => AttachedPoint == null;

        public AttachmentPoint ()
        {
        }

        public AttachmentPoint (Vector2 localPosition, Size size)
        {
            LocalPosition = localPosition;
            Size = size;
        }

        public void AttachTo(AttachmentPoint other) => AttachedPoint = other;

        public void Detach() => AttachedPoint = null;

        public Vector3 LocalToWorldPosition(Vector3 worldPosition)
            => (Vector3)LocalPosition + worldPosition;
    }

    public static class AttachmentPointExtensions
    {
        public static Vector3[] LocalToWorldPosition(this IEnumerable<AttachmentPoint> points, Vector3 worldPosition)
            => points.Select(x => x.LocalToWorldPosition(worldPosition)).ToArray();

        public static AttachmentPoint GetPoint (this IEnumerable<AttachmentPoint> points, Vector3 worldPosition, Vector3 point, float margin = 0.1f)
        {
            foreach (AttachmentPoint ap in points)
            {
                float dist = Vector2.Distance(ap.LocalToWorldPosition(worldPosition), point);
                if (dist < margin)
                {
                    return ap;
                }
            }
            return null;
        }
    }
}
