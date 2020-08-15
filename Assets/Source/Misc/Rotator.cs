using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Misc
{
    public class Rotator : MonoBehaviour
    {
        [ModelProperty]
        public Vector3 Rotation;

        private void FixedUpdate()
        {
            transform.Rotate(Rotation * Time.fixedDeltaTime);
        }
    }
}
