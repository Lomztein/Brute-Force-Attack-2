using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Turrets.Attachment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public interface ITurretComponent : IIdentifiable
    {  
        string Name { get; }
        string Description { get; }
        TurretComponentCategory Category { get; }

        Grid.Size Width { get; }
        Grid.Size Height { get; }

        AttachmentPoint[] GetLowerAttachmentPoints();
        AttachmentPoint[] GetUpperAttachmentPoints();
    }
}