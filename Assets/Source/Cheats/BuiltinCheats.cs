using Lomztein.BFA2.Player.Health;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.UI.Messages;
using Lomztein.BFA2.UI.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Lomztein.BFA2.Cheats
{
    public static class BuiltinCheats
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void RegisterCheats ()
        {
            CheatCode.RegisterCheat("motherlode", () => Player.Player.Instance.Earn(Resource.GetResource("Core.Credits"), 100000));
            CheatCode.RegisterCheat("inthecode", () => Player.Player.Instance.Earn(Resource.GetResource("Core.Binaries"), 100));
            CheatCode.RegisterCheat("smartypants", () => Player.Player.Instance.Earn(Resource.GetResource("Core.Credits"), 100));
            CheatCode.RegisterCheat("4bab2ab52bdcdb88e62e8063f6fa7dd95e16a1be75ec04c3a1eb0537898b4e84", CheatCode.EncryptionMethod.Sha256, NoGameOver);
        }

        private static void NoGameOver ()
        {
            GameOverWindow existing = Object.FindObjectOfType<GameOverWindow>();
            if (existing)
            {
                FuckIt(existing);
            }
            WindowManager.OnWindowOpened += (IWindow window) =>
            {
                if (window is GameOverWindow goWindow)
                {
                    FuckIt(goWindow);
                }
            };
        }

        private static void FuckIt (GameOverWindow window)
        {
            if (window.transform.Find("Options/FuckIt(Clone)") == null)
            {
                Transform options = window.transform.Find("Options");
                GameObject newOption = Object.Instantiate(Resources.Load<GameObject>("UI/Misc/FuckIt"), options);
                newOption.transform.SetAsFirstSibling();
                newOption.GetComponent<Button>().onClick.AddListener(() => window.HurrDurr());
            }
        }
    }
}
