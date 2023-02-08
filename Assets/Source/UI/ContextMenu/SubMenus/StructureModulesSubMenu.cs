using Lomztein.BFA2.Structures;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.UI.ToolTip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.ContextMenu.SubMenus
{
    public class StructureModulesSubMenu : MonoBehaviour
    {
        public GameObject PartPrefab;
        public Transform PartParent;

        private readonly Dictionary<TurretAssemblyModule, GameObject> _partDict = new Dictionary<TurretAssemblyModule, GameObject>();
        private TurretAssembly _assembly;

        public void Assign (Structure structure)
        {
            // TODO: Refactor modules to work with structures rather than assemblies only.
            if (structure is TurretAssembly assembly)
            {
                _assembly = assembly;
                if (_assembly.Modules.Count() == 0)
                {
                    Destroy(gameObject);
                }
                foreach (var module in assembly.Modules)
                {
                    AddModulePart(module);
                }
            }
            else
            {
                throw new NotImplementedException("Modules are currently only supported for assemblies, though this will be refactored.");
            }
        }

        private void AddModulePart (TurretAssemblyModule module)
        {
            GameObject newPart = Instantiate(PartPrefab, PartParent);
            newPart.transform.Find("ImgBackground/Image").GetComponent<Image>().sprite = module.Item.Sprite.Get();
            newPart.transform.Find("Name").GetComponent<Text>().text = module.Item.Name;
            newPart.transform.Find("TrashBackground/Trash").GetComponent<Button>().onClick.AddListener(() => RemoveModule(module));
            newPart.GetComponent<SimpleToolTip>().Title = module.Item.Name;
            newPart.GetComponent<SimpleToolTip>().Description = module.Item.Description;
            _partDict.Add(module, newPart);
        }

        private void RemoveModule (TurretAssemblyModule module)
        {
            _assembly.RemoveModule(module);
            Destroy(_partDict[module]);
            Destroy(module.gameObject);
            _partDict.Remove(module);
            if (_partDict.Count == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
