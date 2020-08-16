using Lomztein.BFA2.Grid;
using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Modifiers.ModProviders;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets.Attachment;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.Connectors
{
    // This class is nothing but null checks.
    public class Connector : TurretComponent, IGlobalUpdateReciever<ModdableAddedMessage>
    {
        [ModelProperty]
        public Vector2 LocalTargetPosition;

        private IModdable _target;
        private IEnumerable<ConnectorModProvider> _providers;

        [ModelProperty]
        public Size UpperAttachmentPointWidth;
        [ModelProperty]
        public Size UpperAttachmentPointHeight;

        public override TurretComponentCategory Category => TurretComponentCategories.Connector;

        private void Cache()
        {
            if (NeedsRecache())
            {
                _target = GetTarget();
                if (_target != null)
                {
                    _providers = GetProviders();
                }
            }
        }

        private IModdable GetTarget ()
        {
            foreach (Transform child in transform)
            {
                if (Vector2.Distance(child.localPosition, LocalTargetPosition) < 0.1f)
                {
                    IModdable moddable = child.GetComponent<IModdable>();
                    if (moddable != null)
                    {
                        return moddable;
                    }
                }
            }
            return null;
        }

        private IEnumerable<ConnectorModProvider> GetProviders ()
        {
            List<ConnectorModProvider> providers = new List<ConnectorModProvider>();
            foreach (Transform child in transform)
            {
                if (child != (_target as Component).transform)
                {
                    ConnectorModProvider prov = child.GetComponent<ConnectorModProvider>();
                    if (prov)
                    {
                        providers.Add(prov);
                    }
                }
            }
            return providers;
        }

        private bool NeedsRecache() => _target == null || _providers == null || _providers.Any(x => x == null);

        public void ApplyMod ()
        {
            Cache();
            if (_target != null && _providers != null)
            {
                foreach (ConnectorModProvider providers in _providers)
                {
                    if (_target.IsCompatableWith(providers.Mod))
                    {
                        _target.Mods.AddMod(providers.Mod);
                        providers.DisableComponents();
                    }
                }
            }
        }

        public void Refresh ()
        {
            RemoveMod();
            ApplyMod();
        }

        public void RemoveMod ()
        {
            Cache();
            if (_providers != null && _target != null)
            {
                foreach (ConnectorModProvider provider in _providers)
                {
                    if (provider != null)
                    {
                        _target.Mods?.RemoveMod(provider.Mod.Identifier);
                        provider.EnableComponents();
                    }
                }
            }
        }

        public override void Init()
        {
            _upperAttachmentPoints = new SquareAttachmentPointSet(UpperAttachmentPointWidth, UpperAttachmentPointHeight);
            StartCoroutine(DelayedApplyMod());
        }

        private IEnumerator DelayedApplyMod ()
        {
            yield return new WaitForEndOfFrame();
            Refresh();
        }

        public override void Tick(float deltaTime)
        {
        }

        public override void End()
        {
            RemoveMod();
        }

        public void OnGlobalUpdateRecieved(ModdableAddedMessage message)
        {
            StartCoroutine(DelayedApplyMod());
        }
    }
}
