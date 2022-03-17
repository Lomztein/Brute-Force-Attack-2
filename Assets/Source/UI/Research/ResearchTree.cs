using Lomztein.BFA2.Research;
using Lomztein.BFA2.Research.UI;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Research
{
    public class ResearchTree : MonoBehaviour
    {
        public ResearchController ResearchController;

        public GameObject ButtonPrefab;
        public GameObject EdgePrefab;

        public Vector2 TreeScale = Vector2.one;
        public RectTransform TreeWindow;

        private bool _completedSophamore;

        public IEnumerator GenerateTree()
        {
            LayeredGraphOrganizer organizer = new LayeredGraphOrganizer();

            var all = ResearchController.GetAll();

            foreach (var researchOption in all)
            {
                organizer.AddNode(researchOption.Identifier);
            }

            foreach (var researchOption in all)
            {
                foreach (var prerequisiteIdentifier in researchOption.PrerequisiteIdentifiers)
                {
                    organizer.AddEdge(organizer.GetNode(prerequisiteIdentifier), organizer.GetNode(researchOption.Identifier));
                }
            }

            DoOrganize(organizer);
            while (_completedSophamore == false)
            {
                yield return new WaitForSeconds(0.1f);
            }
            
            foreach (var option in all)
            {
                var node = organizer.GetNode(option.Identifier);
                Vector2 position = new Vector2(node.X * TreeScale.x, node.Layer * TreeScale.y);

                GameObject newButton = Instantiate(ButtonPrefab, TreeWindow);
                newButton.transform.position = position;

                Transform image = newButton.transform.Find("Image");
                image.GetComponentInChildren<Image>().sprite = option.Sprite.Get();
                image.GetComponentInChildren<Image>().color = option.SpriteTint;

                foreach (var child in node.Children)
                {
                    Vector2 childPos = new Vector2(child.X * TreeScale.x, child.Layer * TreeScale.y);

                    GameObject newEdge = Instantiate(EdgePrefab, TreeWindow);
                    newEdge.transform.position = Vector2.Lerp(position, childPos, 0.5f);
                    float angle = Mathf.Rad2Deg * Mathf.Atan2(childPos.y - position.y, childPos.x - position.x);

                    newEdge.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                    (newEdge.transform.transform as RectTransform).sizeDelta = new Vector2(Vector2.Distance(position, childPos), 16);
                }
            }
        }

        private void DoOrganize(LayeredGraphOrganizer organizer)
        {
            new Thread(() =>
            {
                organizer.Organize();
                _completedSophamore = true;
            }).Start();
        }
    }
}
