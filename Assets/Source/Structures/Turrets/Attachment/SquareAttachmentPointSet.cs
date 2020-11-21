using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets.Attachment
{
    public class SquareAttachmentPointSet : IAttachmentPointSet
    {
        private List<AttachmentPoint> _localPointCache;
        private Grid.Size _width;
        private Grid.Size _height;

        public SquareAttachmentPointSet (Grid.Size width, Grid.Size height)
        {
            _width = width;
            _height = height;
        }

        public AttachmentPoint[] GetPoints()
        {
            if (_localPointCache != null)
            {
                return _localPointCache.ToArray();
            }

            _localPointCache = new List<AttachmentPoint>();
            for (int y = 0; y < (int)_height; y++)
            {
                for (int x = 0; x < (int)_width; x++)
                {
                    Vector3 offset = new Vector3((int)_width - 1, (int)_height - 1) / 2f;
                    _localPointCache.Add(new AttachmentPoint(new Vector2(x - offset.x, y - offset.y)));
                }
            }

            return _localPointCache.ToArray();
        }
    }
}
