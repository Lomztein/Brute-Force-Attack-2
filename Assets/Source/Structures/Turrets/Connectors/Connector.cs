using Lomztein.BFA2.Grid;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.Turrets.Attachment;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets.Connectors
{
    public class Connector : TurretComponent
    {
        [ModelProperty]
        public Vector2 LocalTargetPosition;
        private IModdable _target;

        [ModelProperty]
        public Size UpperAttachmentPointWidth;
        [ModelProperty]
        public Size UpperAttachmentPointHeight;

        public override TurretComponentCategory Category => TurretComponentCategories.Connector;

        private IModdable GetTarget ()
        {
            foreach (Transform child in transform)
            {
                if (Vector2.Distance(child.localPosition, LocalTargetPosition) < 0.1f)
                {
                    IModdable moddable = child.GetComponent<IModdable>();
                    if (moddable != null)
                    {
                        return moddable;
                    }
                }
            }
            return null;
        }

        public override void Init()
        {
            _upperAttachmentPoints = new SquareAttachmentPointSet(UpperAttachmentPointWidth, UpperAttachmentPointHeight);
        }

        public override void Tick(float deltaTime)
        {
        }

        public override void End()
        {
        }
    }
}
