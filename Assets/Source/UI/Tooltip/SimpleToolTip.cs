using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.ToolTip
{
    public class SimpleToolTip : MonoBehaviour, IHasToolTip
    {
        public const string RESOURCE_PATH = "Tooltips/SimpleToolTip";

        public string Title;
        public string Description;
        public string Footer;

        public GameObject GetToolTip()
        {
            return InstantiateToolTip(Title, Description, Footer);
        }

        public void SetTooltip (string title, string description, string footer)
        {
            Title = title;
            Description = description;
            Footer = footer;
        }

        public static GameObject InstantiateToolTip(string title = null, string description = null, string footer = null)
        {
            GameObject newTooltipObject = Instantiate(Resources.Load<GameObject>(RESOURCE_PATH));
            if (string.IsNullOrEmpty(title))
            {
                newTooltipObject.transform.Find("Title").gameObject.SetActive(false);
            }
            else
            {
                newTooltipObject.transform.Find("Title").GetComponent<Text>().text = title;
            }

            if (string.IsNullOrEmpty(description))
            {
                newTooltipObject.transform.Find("Description").gameObject.SetActive(false);
            }
            else
            {
                newTooltipObject.transform.Find("Description").GetComponent<Text>().text = description;
            }

            if (string.IsNullOrEmpty(footer))
            {
                newTooltipObject.transform.Find("Footer").gameObject.SetActive(false);
            }
            else
            {
                newTooltipObject.transform.Find("Footer").GetComponent<Text>().text = footer;
            }
            return newTooltipObject;
        }
    }
}
