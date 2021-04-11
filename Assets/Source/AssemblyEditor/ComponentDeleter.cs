using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.AssemblyEditor
{
    public class ComponentDeleter : MonoBehaviour
    {
        public void Update ()
        {
            if (Input.SecondaryPerformed)
            {
                Vector3 mousePos = MousePosition.WorldPosition;
                var cols = Physics2D.OverlapPointAll(mousePos);
                var components = cols.Select(x => x.GetComponent<TurretComponent>()).Where(x => x != null);

                TurretComponent highest = null;
                float highestNum = float.MinValue;

                foreach (var component in components)
                {
                    float val = component.transform.position.z;
                    if (val > highestNum && component.GetUpperAttachmentPoints().All(x => x.IsEmpty))
                    {
                        highestNum = val;
                        highest = component;
                    }
                }

                if (highest != null)
                {
                    Destroy(highest.gameObject);
                }
            }
        }
    }
}
