using Lomztein.BFA2.UI;
using Lomztein.BFA2.UI.Messages;
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
    public class SaveFileDialog : MonoBehaviour, IWindow
    {
        private static string PrefabPath = "Prefabs/SaveFileDialog";
        public FileBrowser Browser;

        private string _directory;
        private string _extenion;
        private Action<string, string> _callback;

        public InputField FileName;

        public event Action OnClosed;
        private Confirm _currentConfirm;

        public void Close()
        {
            Destroy(gameObject);
            OnClosed?.Invoke();
        }

        public static SaveFileDialog Create (string path, string extension, Action<string, string> callback)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            GameObject go = Resources.Load<GameObject>(PrefabPath);
            SaveFileDialog dialog = WindowManager.OpenWindowAboveOverlay(go).GetComponent<SaveFileDialog>();
            dialog.InitDialog(path, extension, callback);
            return dialog;
        }

        public void Save ()
        {
            string file = _directory + "/" + FileName.text + _extenion;
            if (IsPathValid(file))
            {
                if (File.Exists(file))
                {
                    _currentConfirm = Confirm.Open($"File '{FileName.text + _extenion}' already exists. Overwrite?", () =>
                    {
                        _callback(FileName.text, file);
                        Close();
                    });
                }
                else
                {
                    _callback(FileName.text, file);
                    Close();
                }
            }
            else
            {
                Message.Send("File name invalid. Please use another name.", Message.Type.Minor);
            }
        }

        private bool IsPathValid (string path)
        {
            var chars = Path.GetInvalidPathChars();
            return !chars.Any(x => path.Contains(x));
        }

        public void Init()
        {
        }

        public void InitDialog (string path, string extension, Action<string, string> callback)
        {
            _directory = path;
            _callback = callback;
            _extenion = extension;
            Browser.InitBrowser("Save File", path, extension, OnFileSelected, false);
        }

        private void OnFileSelected(string obj)
        {
            FileName.text = Path.ChangeExtension(Path.GetFileName(obj), null);
            if (_currentConfirm) _currentConfirm.Close();
        }
    }
}
