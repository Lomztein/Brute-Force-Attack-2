using Lomztein.BFA2.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Settings.Controls
{
    public abstract class SettingControlFactory
    {
        public abstract bool CanHandle(Setting setting);

        public abstract GameObject InstantiateControl(Setting setting);
    }

    public abstract class SettingControlFactory<T> : SettingControlFactory where T : Setting
    {
        public override bool CanHandle (Setting setting)
            => typeof (T).IsAssignableFrom (setting.GetType ());

        public override GameObject InstantiateControl(Setting setting)
            => InstantiateControl(setting as T);

        public abstract GameObject InstantiateControl(T setting);
    }
}
