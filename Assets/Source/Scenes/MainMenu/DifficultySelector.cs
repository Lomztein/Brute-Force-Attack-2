using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.Scenes.Battlefield;
using Lomztein.BFA2.Scenes.Battlefield.Difficulty;
using Lomztein.BFA2.UI.Menus.PropertyMenus;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Scenes.MainMenu
{
    public class DifficultySelector : MonoBehaviour, ICustomGameAspectController
    {
        public GameObject DifficultyButtonPrefab;
        public Transform DifficultyButtonParent;
        public PropertyMenu PropertyMenu;
        private Button _selectedButton;

        private IEnumerable<Difficulty> _difficulties;
        private Dictionary<string, Button> _difficultyButtonMap = new Dictionary<string, Button>();

        private string DIFFICULTY_PATH = "*/Difficulty/*";
        private string DEFAULT_IDENTIFIER = "Core.Medium";

        private void Awake ()
        {
            _difficulties = LoadDifficulties();
            InstantiateDifficultyButtons();
            PropertyMenu.OnPropertyChanged += OnPropertyMenuPropertyChanged;
        }

        private void OnPropertyMenuPropertyChanged()
        {
            if (_selectedButton)
            {
                _selectedButton.interactable = true;
            }
            BattlefieldInitializeInfo.NewSettings.Difficulty.IsModified = true;
        }

        private void InstantiateDifficultyButtons ()
        {
            foreach (Transform child in DifficultyButtonParent)
            {
                Destroy(child.gameObject);
            }

            foreach (Difficulty difficulty in _difficulties)
            {
                GameObject newButton = Instantiate(DifficultyButtonPrefab, DifficultyButtonParent);
                newButton.GetComponentInChildren<Text>().text = difficulty.Name;
                Button button = newButton.GetComponentInChildren<Button>();

                if (_difficultyButtonMap.ContainsKey(difficulty.Identifier))
                {
                    _difficultyButtonMap[difficulty.Identifier] = button;
                }
                else
                {
                    _difficultyButtonMap.Add(difficulty.Identifier, button);
                }

                AddButtonListener(button, difficulty);
            }
        }

        private Button GetDifficultyButton(string identifier) => _difficultyButtonMap[identifier];

        private void AddButtonListener (Button button, Difficulty difficulty)
        {
            button.onClick.AddListener(() => SelectDifficulty(button, difficulty));
        }

        private void SelectDifficulty(Button button, Difficulty difficulty)
        {
            if (_selectedButton)
            {
                _selectedButton.interactable = true;
            }
            SelectDifficulty(difficulty);
            _selectedButton = button;
            _selectedButton.interactable = false;
        }

        private void SelectDifficulty (Difficulty difficulty)
        {
            Difficulty newDifficulty = Instantiate(difficulty);
            BattlefieldInitializeInfo.NewSettings.Difficulty = newDifficulty;
            PropertyMenu.Clear();
            newDifficulty.AddPropertiesTo(PropertyMenu);
        }

        private IEnumerable<Difficulty> LoadDifficulties ()
        {
            Difficulty[] loaded = Content.GetAll<Difficulty>(DIFFICULTY_PATH).ToArray();
            var grouped = loaded.GroupBy(x => x.Identifier);
            List<Difficulty> result = new List<Difficulty>();

            foreach (var grouping in grouped)
            {
                Difficulty combined = Difficulty.Combine(grouping);
                result.Add(combined);
            }

            result.Sort(new DifficultyComparer());
            return result;
        }

        public void ApplyBattlefieldSettings(BattlefieldSettings settings)
        {
            if (string.IsNullOrEmpty(settings.Difficulty.Identifier))
            {
                SelectDifficulty(GetDifficultyButton(DEFAULT_IDENTIFIER), _difficulties.First(x => x.Identifier == DEFAULT_IDENTIFIER));
            }else if (settings.Difficulty.IsModified)
            {
                SelectDifficulty(settings.Difficulty);
            }
            else
            {
                SelectDifficulty(GetDifficultyButton(settings.Difficulty.Identifier), settings.Difficulty);
            }
        }
    }
}
