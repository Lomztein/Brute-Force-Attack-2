﻿using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets.Attachment
{
    [System.Serializable]
    public class AttachmentPoint
    {
        [ModelProperty]
        public AttachmentType Type;
        [ModelProperty]
        public Size Size;
        [ModelProperty]
        public Vector3 LocalPosition;
        [ModelProperty]
        public float LocalAngle;
        [ModelProperty]
        public int RequiredPoints;

        public Vector3 GetWorldPosition(Vector3 parentPosition, Quaternion parentRotation) => GetWorldRotation(parentRotation) * (parentPosition + LocalPosition);
        public Quaternion GetWorldRotation(Quaternion parentRotation) => parentRotation * Quaternion.Euler(0f, 0f, LocalAngle);
    }
}
