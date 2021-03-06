﻿using Lomztein.BFA2.Scenes.Battlefield.Mutators;
using Lomztein.BFA2.UI.Tooltip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Scenes.MainMenu
{
    public class AvailableMutator : MonoBehaviour, ITooltip
    {
        public Text NameLabel;
        public Button EnableButton;
        public Mutator Mutator { get; private set; }

        public string Title => Mutator.Name;
        public string Description => Mutator.Description;
        public string Footnote => null;

        public void Assign (Mutator mutator, Action<Mutator> onClickEnable)
        {
            Mutator = mutator;
            NameLabel.text = mutator.Name;
            EnableButton.onClick.AddListener(() => onClickEnable(mutator));
        }
    }
}
