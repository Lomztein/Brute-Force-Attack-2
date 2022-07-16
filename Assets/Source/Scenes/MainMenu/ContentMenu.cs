using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.Scenes.MainMenu.ContentMenuContentDisplay;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Lomztein.BFA2.MainMenu
{
    public class ContentMenu : MonoBehaviour
    {
        public GameObject ContentPackPrefab;
        public Transform ContentPackParent;

        public Text NameText;
        public Text AuthorText;
        public Text VersionText;
        public Text DescriptionText;
        public RawImage Image;

        public ContentDisplay ContentDisplay;
        public Button ReloadContentButton;

        public Texture2D DefaultImage;

        void Start()
        {
            LoadPacks();
        }

        private void LoadPacks ()
        {
            IEnumerable<IContentPack> packs = ContentManager.Instance.FindContentPacks();
            foreach (IContentPack pack in packs)
            {
                GameObject newButton = Instantiate(ContentPackPrefab, ContentPackParent);
                newButton.GetComponent<ContentPackButton>().Assign(pack, OnContentPackClicked, OnContentPackToggled);
            }
            SetText(packs.FirstOrDefault());
        }

        private void OnContentPackClicked (IContentPack pack)
        {
            SetText(pack);
        }

        private void SetText(IContentPack source)
        {
            if (source == null)
            {
                NameText.text = "No packs loaded.";
                AuthorText.text = string.Empty;
                VersionText.text = string.Empty;
                DescriptionText.text = string.Empty;
                Image.texture = DefaultImage;
                //ContentDisplay.Clear();
            }
            else
            {
                NameText.text = source.Name;
                AuthorText.text = source.Author;
                VersionText.text = source.Version;
                DescriptionText.text = source.Description;
                //ContentDisplay.DisplayContent(source);

                if (source.Image != null)
                {
                    Image.texture = source.Image;
                }
                else
                {
                    Image.texture = DefaultImage;
                }
            }
        }

        private void Update()
        {
            ReloadContentButton.interactable = ContentManager.NeedsContentReload;
        }

        private bool OnContentPackToggled (IContentPack pack, bool enabled)
        {
            return ContentManager.Instance.SetContentPackEnabled(pack.GetUniqueIdentifier(), enabled);
        }

        public void ReloadContent ()
        {
            ContentManager.Instance.ReloadContent();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //Preloader.Instance.BeginPreload();
        }
    }
}
