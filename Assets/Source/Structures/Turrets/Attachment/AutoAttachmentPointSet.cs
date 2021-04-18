using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Grid = Lomztein.BFA2.World.Grid;

namespace Lomztein.BFA2.Structures.Turrets.Attachment
{
    [Serializable]
    class AutoAttachmentPointSet : IAttachmentPointSet
    {
        [ModelProperty]
        public Size Width;
        [ModelProperty]
        public Size Height;

        private IEnumerable<AttachmentPoint> _pointCache;

        public IEnumerable<AttachmentPoint> GetPoints()
        {
            if (_pointCache == null)
            {
                _pointCache = GeneratePoints();
            }
            return _pointCache;
        }

        private IEnumerable<AttachmentPoint> GeneratePoints()
        {
            for (int y = 0; y < (int)Height; y++)
            {
                for (int x = 0; x < (int)Width; x++)
                {
                    Vector3 offset = new Vector3((int)Width - 1, (int)Height - 1) / 2f;
                    yield return new AttachmentPoint(new Vector2(x - offset.x, y - offset.y), Size.Small);
                }
            }
        }
    }
}
