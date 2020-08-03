using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Research.UI
{
    public class CurrentResearchList : MonoBehaviour
    {
        public ResearchController Controller;
        public Transform ResearchBarParent;

        public GameObject NoCurrentResearch;
        private List<GameObject> _emptySlots = new List<GameObject>();

        public GameObject _researchBarPrefab;
        public GameObject _emptySlotPrefab;

        private Dictionary<ResearchOption, ResearchBar> _currentBars = new Dictionary<ResearchOption, ResearchBar>();

        public void Awake()
        {
            Controller.OnResearchBegun += OnResearchBegun;
            Controller.OnResearchCancelled += OnResearchCancelled;
            Controller.OnMaxResearchSlotsSet += OnMaxResearchSlotsSet;
        }

        private void OnMaxResearchSlotsSet(int obj)
        {
            InstantiateEmptySlotPlaceholders(obj);
            UpdatePlaceholders();
        }

        private void InstantiateEmptySlotPlaceholders (int max) 
        {
            for (int i = Mathf.Max(1, _emptySlots.Count); i < max; i++)
            {
                _emptySlots.Add(Instantiate(_emptySlotPrefab, ResearchBarParent));
            }
        }

        private GameObject[] GetPlaceholders ()
        {
            List<GameObject> placeholders = new List<GameObject>
            {
                NoCurrentResearch
            };
            placeholders.AddRange(_emptySlots);
            return placeholders.ToArray();
        }

        public void UpdatePlaceholders ()
        {
            GameObject[] placeholders = GetPlaceholders();

            for (int i = 0; i < _currentBars.Count; i++)
            {
                _currentBars.ElementAt(i).Value.transform.SetSiblingIndex(i);
                placeholders[i].SetActive(false);
            }
            for (int i = _currentBars.Count; i < Controller.MaxResearchSlots; i++)
            {
                placeholders[i].transform.SetSiblingIndex(i);
                placeholders[i].SetActive(true);
            }
        }

        public void OnResearchMenuOpened ()
        {
            ClearFinished();
        }

        public void ClearFinished ()
        {
            List<ResearchBar> toDestroy = _currentBars.Where(x => x.Value.IsCompleted).Select(x => x.Value).ToList();
            foreach (ResearchBar bar in toDestroy)
            {
                _currentBars.Remove(bar.Research);
                bar.Destroy();
            }
            UpdatePlaceholders();
        }

        private void OnResearchCancelled(ResearchOption obj)
        {
            _currentBars[obj].Destroy();
            _currentBars.Remove(obj);
            UpdatePlaceholders();
        }

        private void OnResearchBegun(ResearchOption obj)
        {
            GameObject newBar = Instantiate(_researchBarPrefab, ResearchBarParent);
            ResearchBar bar = newBar.GetComponent<ResearchBar>();
            bar.Assign(obj);

            _currentBars.Add(obj, bar);

            UpdatePlaceholders();
        }
    }
}