using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Scenes.Battlefield;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.UI.Windows;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.World;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.UI.Menus
{
    public class PauseMenu : MonoBehaviour, IWindow
    {
        public event Action OnClosed;

        public void Close()
        {
            if (gameObject.activeSelf)
            {
                OnClosed?.Invoke();
                Destroy(gameObject);
                Resume();
            }
        }

        public void Init()
        {
            Pause();
        }

        public void Pause ()
        {
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            Time.timeScale = 1f;
        }

        public void Save ()
        {
            var window = SaveFileDialog.Create(BattlefieldSave.PATH_ROOT, ".json", OnSave);
            window.OnClosed += Window_OnClosed;
            Hide();
        }

        private void OnSave(string filename, string path)
        {
            var save = BattlefieldSave.SaveFromBattlefield(BattlefieldController.Instance);
            var json = BattlefieldSave.ToJSON(save);
            json[FileBrowser.FILE_DESCRIPTION] = GetSaveDescription();
            json[FileBrowser.FILE_IMAGE] = TakeScreenshot().ToBase64();
            File.WriteAllText(path, json.ToString());
        }

        private Texture2D TakeScreenshot()
        {
            MapData data = BattlefieldController.Instance.MapController.MapData;
            Rect screenRect = new Rect(
                -data.Width / 2f,
                -data.Height / 2f,
                data.Width,
                data.Height
                );
            return CameraCapture.CaptureOrtho(screenRect, new Vector2Int(128, 128));
        }

        private string GetSaveDescription ()
        {
            var settings = BattlefieldController.Instance.CurrentSettings;
            return $"Map: {BattlefieldController.Instance.MapController.MapData.Name}" +
                $"\nDifficulty: {settings.Difficulty.Name}" +
                $"\nWave: {RoundController.Instance.NextIndex}";
        }

        public void Load ()
        {
            var window = FileBrowser.Create("Select Save File", BattlefieldSave.PATH_ROOT, ".json", OnLoad);
            window.OnClosed += Window_OnClosed;
            Hide();
        }

        private void Window_OnClosed()
        {
            Unhide();
        }

        private void OnLoad(string path)
        {
            BattlefieldInitializeInfo.InitType = BattlefieldInitializeInfo.InitializeType.Load;
            BattlefieldInitializeInfo.LoadFileName = path;
            Resume();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Hide()
            => gameObject.SetActive(false);

        public void Unhide()
            => gameObject.SetActive(true);

        public void Restart ()
        {
            Resume();
            BattlefieldInitializeInfo.InitType = BattlefieldInitializeInfo.InitializeType.New;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit ()
        {
            Resume();
            SceneManager.LoadScene(0);
        }
    }
}