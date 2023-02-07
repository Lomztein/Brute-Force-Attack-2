using Lomztein.BFA2.Structures.Turrets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.ContextMenu.SubMenus
{
    public class TargetPrioritySubMenu : MonoBehaviour
    {
        public GameObject SlotPrefab;
        public Transform SlotParent;
        public Transform AddRemovePanel;
        private List<Transform> _slots = new List<Transform>();
        private TargetEvaluatorChanger _assignedChanger;
        
        public void Assign (TargetEvaluatorChanger changer)
        {
            var current = changer.CurrentEvaluator;
            _assignedChanger = changer;
            for (int i = 0; i < current.Length; i++)
            {
                _slots.Add(CreateSlotObject(i));
                UpdateSlot(i);
            }
        }

        private Transform CreateSlotObject (int index)
        {
            GameObject newSlot = Instantiate(SlotPrefab, SlotParent);
            AddRemovePanel.SetAsLastSibling();
            Button left = newSlot.transform.Find("Left").GetComponent<Button>();
            Button right = newSlot.transform.Find("Right").GetComponent<Button>();
            left.onClick.AddListener(() =>
            {
                _assignedChanger.ChangeEvaluator(index, -1);
                UpdateSlot(index);
            });
            right.onClick.AddListener(() =>
            {
                _assignedChanger.ChangeEvaluator(index, 1);
                UpdateSlot(index);
            });

            return newSlot.transform;
        }

        private void UpdateSlot (int index)
        {
            Text name = _slots[index].Find("Name").GetComponent<Text>();
            name.text = _assignedChanger.CurrentEvaluator[index].Name;
        }

        public void AddSlot ()
        {
            int index = _assignedChanger.AddSlot();
            _slots.Add(CreateSlotObject(index));
            UpdateSlot (index);
        }

        public void RemoveSlot ()
        {
            int lastIndex = _assignedChanger.CurrentEvaluator.Length - 1;
            _assignedChanger.RemoveSlot(lastIndex);
            Destroy(_slots[lastIndex].gameObject);
            _slots.RemoveAt(lastIndex);
        }
    }
}
