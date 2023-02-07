using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.ContextMenu
{
    public class SubMenuRoot : MonoBehaviour
    {
        public void CloseChildren (SubMenuHandler except)
        {
            foreach (SubMenuHandler handler in GetChildHandlers())
            {
                if (handler != except)
                {
                    handler.Close();
                }
            }
        }

        private IEnumerable<SubMenuHandler> GetChildHandlers ()
        {
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out SubMenuHandler handler))
                {
                    yield return handler;
                }
            }
        }
    }
}
