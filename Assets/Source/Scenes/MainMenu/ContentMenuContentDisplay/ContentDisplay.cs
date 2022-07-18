using Lomztein.BFA2.ContentSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Lomztein.BFA2.Scenes.MainMenu.ContentMenuContentDisplay
{
    public class ContentDisplay : MonoBehaviour
    {
        public GameObject ElementPrefab;
        public Transform ListParent;

        [System.Serializable]
        public struct Query
        {
            public string Name;
            public string Pattern;
        }

        public Query[] Queries;


        public void DisplayContent (IContentPack contentPack)
        {
            Clear();
            foreach (var query in Queries)
            {
                int count = ContentIndex.Query(contentPack.GetContentPaths(), query.Pattern).Count();
                if (count > 0)
                {
                    GameObject newElementObj = Instantiate(ElementPrefab, ListParent);
                    newElementObj.GetComponentInChildren<Text>().text = $"{count} {query.Name}";
                }
            }
        }

        public void Clear ()
        {
            foreach (Transform child in ListParent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
