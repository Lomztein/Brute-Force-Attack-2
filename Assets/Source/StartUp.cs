using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.LocalizationSystem;
using Lomztein.BFA2.Plugins;
using Lomztein.BFA2.Scenes.Battlefield;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lomztein.BFA2
{
    public class StartUp : MonoBehaviour
    {
        public const string UNIVERSAL_RESOURCE_PATH = "Universal";

        private static bool _hasStartedUp = false;
        public ContentManager ContentManager;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void OnStartUp ()
        {
            if (!_hasStartedUp)
            {
                GameObject startup = Instantiate(Resources.Load<GameObject>(UNIVERSAL_RESOURCE_PATH));
                startup.GetComponentInChildren<StartUp>().InitializeGame();
                DontDestroyOnLoad(startup);
                _hasStartedUp = true;
            }
        }

        internal void InitializeGame()
        {
            InitializeCriticalFolders();
            SerializationFileAccess.SetAccessor(new ContentFileAccessor());

            Input.Init();
            Facade.Init();

            Cursor.lockState = CursorLockMode.None; // TODO: Implement as setting
            CustomContentUtils.TryGenerateCustomContentPack();
            ContentManager.ReloadContent();

            Localization.LoadLocalizations(PlayerPrefs.GetString("Culture", "en-US"));
            BattlefieldInitializeInfo.NewSettings = BattlefieldSettings.LoadDefaults();

            GameSettings.Init();

            GameSettings.AddOnChangedListener("Dev.DeveloperMode", HandleDevMode);
            GameSettings.AddOnChangedListener("Dev.UserContentPack", HandleUserContentPack);
            HandleDevMode("Dev.DeveloperMode", GameSettings.GetValue("Dev.DeveloperMode", 0));
            HandleUserContentPack("Dev.UserContentPack", GameSettings.GetValue("Dev.UserContentPack", -1));
        }

        private void HandleUserContentPack(string identifier, object value)
        {
            if ((int)value == -1)
            {
                Content.SetUserContentPack(null);
            }
            else
            {
                var pack = ContentManager.Instance.FindContentPacks().Where(x => x is ContentPack).ToArray()[(int)value];
                Content.SetUserContentPack((pack as ContentPack).Path);
            }
        }

        private void HandleDevMode(string identifier, object value)
        {
            bool isOn = (int)value == 1;
            if (isOn)
                InterceptLogs();
            else UnsubscribeLogs();
        }

        internal void InitializeCriticalFolders ()
        {
            System.IO.Directory.CreateDirectory(BattlefieldSave.PATH_ROOT);
        }

        private void InterceptLogs()
        {
            Application.logMessageReceived += OnLogMessageRecieved;
        }

        private void UnsubscribeLogs()
        {
            Application.logMessageReceived -= OnLogMessageRecieved;
        }

        private void OnLogMessageRecieved(string condition, string stackTrace, LogType type)
        {
            try
            {
                switch (type)
                {
                    case LogType.Warning:
                        MessageLogger.Warning($"{type}: {condition} -> {stackTrace}");
                        break;

                    case LogType.Error:

                    case LogType.Exception:
                        MessageLogger.Error($"{type}: {condition} -> {stackTrace}");
                        break;

                    default:
                        MessageLogger.Write($"{type}: {condition}");
                        break;
                }
            }catch (Exception e)
            {
                Debug.LogError("Something went wrong while attempting to display an exception.");
                Debug.LogException(e);
            }
        }
    }
}
