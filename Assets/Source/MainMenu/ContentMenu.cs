using Lomztein.BFA2.ContentSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.MainMenu
{
    public class ContentMenu : MonoBehaviour
    {
        public Text InfoText;
        public GameObject ContentPackPrefab;
        public Transform ContentPackParent;
        public ContentManager Manager;

        void Start()
        {
            LoadPacks();
        }

        private void LoadPacks ()
        {
            IContentPack[] packs = Manager.GetContentPacks();
            foreach (IContentPack pack in packs)
            {
                GameObject newButton = Instantiate(ContentPackPrefab, ContentPackParent);
                newButton.GetComponent<ContentPackButton>().Assign(pack, OnContentPackClicked, OnContentPackToggled);
            }
            SetText(packs[0]);
        }

        private void OnContentPackClicked (IContentPack pack)
        {
            SetText(pack);
        }

        private void SetText(IContentPack source)
        {
            InfoText.text = $"{source.Name}\n\nAuthor: {source.Author}\n\n{source.Description}";
        }

        private bool OnContentPackToggled (IContentPack pack, bool enabled)
        {
            return true;
        }
    }
}
