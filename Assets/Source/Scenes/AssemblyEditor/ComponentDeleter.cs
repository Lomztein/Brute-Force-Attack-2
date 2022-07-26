using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.Turrets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.AssemblyEditor
{
    public class ComponentDeleter : MonoBehaviour
    {
        private void Start()
        {
            Input.SecondaryClicKStarted += Input_SecondaryClicKStarted;
        }

        private void OnDestroy()
        {
            Input.SecondaryClicKStarted -= Input_SecondaryClicKStarted;
        }

        private void Input_SecondaryClicKStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            DeleteAtMouse();
        }

        public void DeleteAtMouse ()
        {
                Vector3 mousePos = MousePosition.WorldPosition;
                var cols = Physics2D.OverlapPointAll(mousePos);
                var components = cols.Select(x => x.GetComponent<TurretComponent>()).Where(x => x != null);

                TurretComponent highest = null;
                float highestNum = float.MinValue;

            foreach (var component in components)
            {
                float val = component.transform.position.z;
                if (val > highestNum && component.AttachmentSlots.GetSlotsOfType(Structures.Turrets.Attachment.AttachmentType.Upper).All(x => x.IsEmpty()))
                {
                    highestNum = val;
                    highest = component;
                }
            }

            if (highest != null)
            {
                Destroy(highest.gameObject);
                if (highest.transform.parent != null && highest.transform.parent.TryGetComponent(out TurretComponent parentComponent))
                {
                    StartCoroutine(DelayedRemoveDeadAttachments(parentComponent));
                }
            }
        }

        private IEnumerator DelayedRemoveDeadAttachments(TurretComponent component)
        {
            yield return null;
            component.RemoveAttachmentsToDeadChildren();
        }
    }
}
