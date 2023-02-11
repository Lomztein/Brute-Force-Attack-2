using Lomztein.BFA2.Settings;
using Lomztein.BFA2.Settings.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Menus
{
    public class SettingsMenu : MonoBehaviour
    {
        public GameObject CategoryButtonPrefab;
        public Transform CategoryButtonParent;

        public GameObject TabPrefab;
        public Transform TabParent;

        public TabMenu TabMenu;

        public void Start()
        {
            var categories = SettingCategory.GetCategories();
            GenerateTabs(categories);
        }

        private void GenerateTabs (IEnumerable<SettingCategory> categories)
        {
            List<Button> _tabButtons = new List<Button>();
            List<ITabMenuElement> _elements = new List<ITabMenuElement>();

            var sorted = categories.ToArray();
            Array.Sort(sorted, (x, y) => x.Order - y.Order);

            foreach (var category in sorted)
            {
                GameObject newButtonGO = Instantiate(CategoryButtonPrefab, CategoryButtonParent);
                Button butt = newButtonGO.GetComponent<Button>();
                _tabButtons.Add(butt);
                _elements.Add(GenerateTab(category));

                newButtonGO.transform.Find("Image").GetComponent<Image>().sprite = category.Sprite.Get();
                newButtonGO.transform.Find("Text").GetComponent<Text>().text = category.Name;

                butt.onClick.AddListener(() =>
                {
                    TabMenu.Open(_tabButtons.IndexOf(butt));
                });
            }

            TabMenu.TabButtons = _tabButtons.ToArray();
            TabMenu.SetSubmenus(_elements.ToArray(), false);
        }

        private ITabMenuElement GenerateTab (SettingCategory category)
        {
            GameObject newTab = Instantiate(TabPrefab, TabParent);
            ITabMenuElement element = newTab.GetComponent<ITabMenuElement>();

            var settings = Setting.LoadSettings().Where(x => x.Category.Identifier == category.Identifier).ToArray();
            Array.Sort(settings, (x, y) => x.Order - y.Order);
            
            foreach (var setting in settings)
            {
                var newSetting = GenerateSetting(setting);
                newSetting.transform.SetParent(newTab.transform);
            }
            return element;
        }

        private Transform GenerateSetting (Setting setting)
        {
            CompositeSettingControlFactory factory = new CompositeSettingControlFactory();
            if (factory.CanHandle(setting))
            {
                GameObject control = factory.InstantiateControl(setting);
                return control.transform;
            }
            else
            {
                Debug.LogWarning("No setting control factories available for setting " + setting.Identifier);
            }
            return null;
        }

        public void SaveSettings ()
        {
            GameSettings.Save();
            Alert.Open("Settings succesfully saved and applied.");
        }
    }
}
