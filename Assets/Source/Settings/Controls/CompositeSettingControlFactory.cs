using Lomztein.BFA2.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Settings.Controls
{
    public class CompositeSettingControlFactory : SettingControlFactory
    {
        private static IEnumerable<SettingControlFactory> _factories;

        private IEnumerable<SettingControlFactory> GetFactories ()
        {
            if (_factories == null)
                _factories = ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<SettingControlFactory>(GetType());
            return _factories;
        }

        public override bool CanHandle(Setting setting)
            => GetFactories().Any(x => x.CanHandle(setting));

        public override GameObject InstantiateControl(Setting setting)
        {
            foreach (var factory in GetFactories())
            {
                if (factory.CanHandle(setting))
                {
                    return factory.InstantiateControl(setting);
                }
            }

            throw new InvalidOperationException($"No control factory available for setting type {setting}.");
        }
    }
}
