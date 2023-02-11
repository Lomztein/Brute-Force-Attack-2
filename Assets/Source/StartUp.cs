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
            SerializationFileAccess.SetAccessor(new ContentFileAccessor());

            Input.Init();
            Facade.Init();

            Cursor.lockState = CursorLockMode.None; // TODO: Implement as setting
            CustomContentUtils.TryGenerateCustomContentPack();
            ContentManager.ReloadContent();

            Localization.LoadLocalizations(PlayerPrefs.GetString("Culture", "en-US"));
            BattlefieldInitializeInfo.NewSettings = BattlefieldSettings.LoadDefaults();

            InterceptLogs();
            GameSettings.Init();
        }

        private void InterceptLogs()
        {
            if (Application.isEditor || Debug.isDebugBuild)
            {
                Application.logMessageReceived += OnLogMessageRecieved;
            }
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
