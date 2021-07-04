using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets.Attachment
{
    public interface IAttachable
    {
        Vector3 WorldPosition { get; }
        Quaternion WorldRotation { get; }
        IEnumerable<AttachmentPoint> GetPoints();
    }

    public static class AttachableExtensions
    {
        public static IEnumerable<Vector3> GetPointsWorldPositions (this IAttachable attachable)
        {
            foreach (AttachmentPoint point in attachable.GetPoints())
            {
                yield return point.GetWorldPosition(attachable.WorldPosition, attachable.WorldRotation);
            }
        }
    }
}
