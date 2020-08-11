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
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var cols = Physics2D.OverlapPointAll(mousePos);
                var components = cols.Select(x => x.GetComponent<ITurretComponent>()).Where(x => x != null);

                ITurretComponent highest = null;
                float highestNum = float.MinValue;

                foreach (var component in components)
                {
                    float val = (component as Component).transform.position.z;
                    if (val > highestNum && component.GetUpperAttachmentPoints().All(x => x.IsEmpty))
                    {
                        highestNum = val;
                        highest = component;
                    }
                }

                if (highest != null)
                {
                    Destroy((highest as Component).gameObject);
                }
            }
        }
    }
}
