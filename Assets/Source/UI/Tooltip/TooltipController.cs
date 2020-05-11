using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Tooltip
{
    public class TooltipController : MonoBehaviour
    {
        public Transform TooltipTransform;
        public Text TooltipText;
        public Vector2 Offset;

        public GameObject[] Updaters;
        private ITooltipUpdater[] _updaters;

        private void Awake()
        {
            _updaters = Updaters.Select(x => x.GetComponent<ITooltipUpdater>()).ToArray();
        }

        private void Update()
        {
            TooltipText.text = string.Empty;

            foreach (ITooltipUpdater updater in _updaters)
            {
                string result = updater.GetTooltip();
                if (!string.IsNullOrEmpty(result))
                {
                    TooltipText.text = result;
                    break;
                }
            }

            TooltipTransform.gameObject.SetActive(!string.IsNullOrEmpty(TooltipText.text));

            Vector2 mousePos = Input.mousePosition;
            TooltipTransform.position = mousePos + Offset;
        }
    }
}
