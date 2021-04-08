using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.Turrets.Attachment;
using Lomztein.BFA2.World;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets.Connectors
{
    public class Connector : TurretComponent
    {
        private const float TARGET_DIST_MARGIN = 0.1f;

        [ModelProperty]
        public Vector2 LocalTargetPosition;

        [ModelProperty]
        public Size UpperAttachmentPointWidth;
        [ModelProperty]
        public Size UpperAttachmentPointHeight;

        public override StructureCategory Category => StructureCategories.Connector;

        private IModdable GetTarget ()
        {
            foreach (Transform child in transform)
            {
                if (IsTargetPosition(child.localPosition))
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
                if (!IsTargetPosition(child.localPosition))
                {
                    ConnectorModProvider provider = child.GetComponent<ConnectorModProvider>();
                    if (provider)
                    {
                        providers.Add(provider);
                    }
                }
            }

            return providers;
        }

        private bool IsTargetPosition(Vector3 position)
            => Vector2.Distance(position, LocalTargetPosition) < TARGET_DIST_MARGIN;

        public override void Init()
        {
            _upperAttachmentPoints = new SquareAttachmentPointSet(UpperAttachmentPointWidth, UpperAttachmentPointHeight);

            if (_assembly)
            {
                _assembly.Changed += OnAssemblyChanged;
            }

            ApplyMods();
        }

        private void OnAssemblyChanged(Structure obj)
        {
            Refresh();
        }

        private void ApplyMods ()
        {
            IModdable target = GetTarget();
            if (target != null)
            {
                foreach (var provider in GetProviders())
                {
                    target.Mods.AddMod(provider.Mod);
                    provider.DisableComponents();
                }
            }
        }

        private void RemoveMods()
        {
            IModdable target = GetTarget();
            if (target != null)
            {
                foreach (var provider in GetProviders())
                {
                    target.Mods.RemoveMod(provider.Mod.Identifier);
                    provider.EnableComponents();
                }
            }
        }

        private void Refresh ()
        {
            RemoveMods();
            ApplyMods();
        }

        public override void Tick(float deltaTime)
        {
        }

        public override void End()
        {
            if (_assembly)
            {
                _assembly.Changed -= OnAssemblyChanged;
            }
            RemoveMods();
        }
    }
}
