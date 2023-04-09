using Lomztein.BFA2.UI.Windows;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lomztein.BFA2.Serialization.IO
{
    public class FileBrowser : MonoBehaviour, IWindow
    {
        public const string FILE_NAME = "FileName";
        public const string FILE_DESCRIPTION = "FileDescription";
        public const string FILE_IMAGE = "ImageBase64";

        private static string FileBrowserPrefabPath = "Prefabs/FileBrowser";

        public event Action OnClosed;

        public Text HeaderText;
        public GameObject FileButtonPrefab;
        public Transform ButtonParent;

        private string _path;
        private Action<string> _callback;
        private string _extenison;
        private string _name;
        private bool _closeOnSelection;

        public Text FileNameText;
        public Text FileDescriptionText;
        public Image FileImage;
        public Text TimeStampText;
        public GameObject Padding;

        public void Close()
        {
            Destroy(gameObject);
            OnClosed?.Invoke();
        }

        public void Init()
        {
        }

        public void InitBrowser (string name, string path, string extension, Action<string> callback, bool closeOnSelection)
        {
            _name = name;
            _path = path;
            _callback = callback;
            _extenison = extension;
            _closeOnSelection = closeOnSelection;
            UpdateBrowser();
        }

        public void UpdateBrowser ()
        {
            HeaderText.text = _name;
            if (Directory.Exists(_path))
            {
                foreach (Transform child in ButtonParent)
                {
                    Destroy(child.gameObject);
                }

                string[] files = Directory.GetFiles(_path, "*" + _extenison);
                Array.Sort(files, (x, y) => Math.Sign((File.GetLastWriteTime(y) - File.GetLastWriteTime(x)).Ticks));

                foreach (string file in files)
                {
                    GameObject go = Instantiate(FileButtonPrefab, ButtonParent);
                    Button button = go.GetComponent<Button>();
                    button.transform.Find("Text").GetComponentInChildren<Text>().text = Path.ChangeExtension(Path.GetFileName(file), null);
                    button.transform.Find("Timestamp").GetComponentInChildren<Text>().text = File.GetLastWriteTime(file).ToShortDateString();
                    button.onClick.AddListener(() => Select(file));
                    go.GetComponent<HoverCallback>().OnEnter += (x) => FileBrowser_OnEnter(file);
                }
            }
        }

        private void FileBrowser_OnEnter(string file)
        {
            // TODO: Consider caching these for optimization, so that we're not constantly reading a ton of files from disk.
            FileNameText.transform.parent.gameObject.SetActive(true);
            TimeStampText.transform.parent.gameObject.SetActive(true);

            if (Path.GetExtension(file) == ".json")
            {
                string content = File.ReadAllText(file);
                JObject fileObj = JObject.Parse(content);

                string name = fileObj["FileName"]?.ToString() ?? Path.ChangeExtension(Path.GetFileName(file), null);
                string description = fileObj["FileDescription"]?.ToString();
                string imageBase64 = fileObj["ImageBase64"]?.ToString();
                string timestamp = File.GetLastWriteTime(file).ToString();

                FileNameText.text = name;
                TimeStampText.text = timestamp;
                bool padding = false;

                if (description == null)
                {
                    FileDescriptionText.transform.parent.gameObject.SetActive(false);
                    padding = true;
                }
                else
                {
                    FileDescriptionText.transform.parent.gameObject.SetActive(true);
                    FileDescriptionText.text = description;
                }

                if (imageBase64 == null)
                {
                    FileImage.transform.parent.parent.parent.gameObject.SetActive(false);
                    padding = true;
                }
                else
                {
                    FileImage.transform.parent.parent.parent.gameObject.SetActive(true);
                    Texture2D tex = imageBase64.ToTexture2D();
                    FileImage.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
                }

                Padding.SetActive(padding);
            }
        }

        public void Select(string file)
        {
            _callback(file);
            if (_closeOnSelection)
                Close();
        }

        public static FileBrowser Create (string name, string path, string extension, Action<string> callback, bool closeOnSelection = true)
        {
            GameObject prefab = Resources.Load<GameObject>(FileBrowserPrefabPath);
            FileBrowser browser = WindowManager.OpenWindowAboveOverlay(prefab).GetComponent<FileBrowser>();
            browser.InitBrowser(name, path, extension, callback, closeOnSelection);
            return browser;
        }
    }
}
