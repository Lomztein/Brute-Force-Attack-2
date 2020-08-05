using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Misc
{
    public class RandomRotation : MonoBehaviour
    {
        public void Start()
        {
            transform.Rotate(0f, 0f, Random.Range(0, 360));
        }
    }
}
