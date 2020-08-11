using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.Attachment
{
    public class AttachmentPoint
    {
        public Vector2 LocalPosition { get; private set; }
        public AttachmentPoint AttachedPoint { get; private set; }
        public bool IsEmpty => AttachedPoint == null;

        public AttachmentPoint (Vector2 localPosition)
        {
            LocalPosition = localPosition;
        }

        public void AttachTo(AttachmentPoint other) => AttachedPoint = other;

        public void Detach() => AttachedPoint = null;

        public Vector3 LocalToWorldPosition(Vector3 worldPosition)
            => (Vector3)LocalPosition + worldPosition;
    }

    public static class AttachmentPointExtensions
    {
        public static Vector3[] LocalToWorldPosition(this AttachmentPoint[] points, Vector3 worldPosition)
            => points.Select(x => x.LocalToWorldPosition(worldPosition)).ToArray();

        public static AttachmentPoint GetPoint (this AttachmentPoint[] points, Vector3 worldPosition, Vector3 point, float margin = 0.1f)
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
