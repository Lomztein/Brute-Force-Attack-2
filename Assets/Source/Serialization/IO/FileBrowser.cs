using Lomztein.BFA2.UI.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Serialization.IO
{
    public class FileBrowser : MonoBehaviour, IWindow
    {
        private static string FileBrowserPrefabPath = "Prefabs/FileBrowser";

        public event Action OnClosed;

        public GameObject FileButtonPrefab;
        public Transform ButtonParent;

        private string _path;
        private Action<string> _callback;
        private string _extenison;

        public void Close()
        {
            Destroy(gameObject);
            OnClosed?.Invoke();
        }

        public void Init()
        {
        }

        public void InitBrowser (string path, string extension, Action<string> callback)
        {
            _path = path;
            _callback = callback;
            _extenison = extension;
            UpdateBrowser();
        }

        public void UpdateBrowser ()
        {
            if (Directory.Exists(_path))
            {
                foreach (Transform child in ButtonParent)
                {
                    Destroy(child.gameObject);
                }

                string[] files = Directory.GetFiles(_path, "*" + _extenison);
                foreach (string file in files)
                {
                    GameObject go = Instantiate(FileButtonPrefab, ButtonParent);
                    Button button = go.GetComponent<Button>();
                    button.GetComponentInChildren<Text>().text = Path.GetFileName(file);
                    button.onClick.AddListener(() => Select(file));
                }
            }
        }

        public void Select(string file)
        {
            _callback(file);
            Close();
        }

        public static FileBrowser Create (string path, string extension, Action<string> callback)
        {
            GameObject prefab = Resources.Load<GameObject>(FileBrowserPrefabPath);
            FileBrowser browser = WindowManager.OpenWindowAboveOverlay(prefab).GetComponent<FileBrowser>();
            browser.InitBrowser(path, extension, callback);
            return browser;
        }
    }
}
