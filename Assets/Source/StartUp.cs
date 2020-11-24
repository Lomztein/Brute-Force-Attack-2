using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2
{
    public class StartUp : MonoBehaviour
    {
        private static bool _hasStartedUp = false;

        private void Awake()
        {
            if (!_hasStartedUp)
            {
                InterceptLogs();

                _hasStartedUp = true;
            }
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
                    MessageLogger.Write($"{type}: {condition} -> {stackTrace}");
                    break;
            }
        }
    }
}
