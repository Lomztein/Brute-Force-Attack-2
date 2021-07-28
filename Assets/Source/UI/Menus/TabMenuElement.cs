using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Menus
{
    public class TabMenuElement : MonoBehaviour, ITabMenuElement
    {
        [SerializeField] private string _name;
        public string Name => _name;

        public event Action<ITabMenuElement> OnNameChanged;

        public void CloseMenu()
        {
            gameObject.SetActive(false);
        }

        public void Init()
        {
        }

        public void OpenMenu()
        {
            gameObject.SetActive(true);
        }
    }
}
