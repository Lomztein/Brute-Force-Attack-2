using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lomztein.BFA2.UI.ContextMenu
{
    public class SubMenuHandler : MonoBehaviour, IPointerEnterHandler
    {
        public GameObject SubMenuPrefab;
        private Func<GameObject> _subMenuPrefabOverride;
        public RectTransform SubMenuParent;
        private GameObject _currentMenu;
        private ContextMenu.Side _side;

        private float _createTime;
        private const float ACTIVE_DELAY = 0.1f;

        private void Start()
        {
            _createTime = Time.time;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            GetComponentInParent<SubMenuRoot>().CloseChildren(this);
            if ((SubMenuPrefab || _subMenuPrefabOverride != null) && Time.time > _createTime + ACTIVE_DELAY)
            {
                if (!_currentMenu)
                {
                    if (_subMenuPrefabOverride != null)
                    {
                        _currentMenu = _subMenuPrefabOverride();
                    }
                    else
                    {
                        _currentMenu = Instantiate(SubMenuPrefab);
                    }
                    _currentMenu.transform.SetParent(SubMenuParent, false);
                    var subSubMenus = _currentMenu.GetComponentsInChildren<SubMenuHandler>();
                    foreach (var subSubMenu in subSubMenus)
                    {
                        subSubMenu.SetSide(_side);
                    }
                }
            }
        }

        public void OverrideSubmenuPrefab (Func<GameObject> subMenuOverride)
        {
            _subMenuPrefabOverride = subMenuOverride;
        }

        public void SetSide (ContextMenu.Side side)
        {
            _side = side;
            if (_side == ContextMenu.Side.Right)
            {
                SubMenuParent.pivot = new Vector2(0f, 0.5f);
                SubMenuParent.anchorMax = new Vector2(1f, 0.5f);
                SubMenuParent.anchorMin = new Vector2(1f, 0.5f);
                SubMenuParent.anchoredPosition = new Vector2(0f, 0f);
            }
            if (_side == ContextMenu.Side.Left)
            {
                SubMenuParent.pivot = new Vector2(1f, 0.5f);
                SubMenuParent.anchorMax = new Vector2(0f, 0.5f);
                SubMenuParent.anchorMin = new Vector2(0f, 0.5f);
                SubMenuParent.anchoredPosition = new Vector2(0f, 0f);
            }
        }

        public void Close ()
        {
            if (_currentMenu)
            {
                DestroyImmediate(_currentMenu, false);
            }
        }
    }
}
