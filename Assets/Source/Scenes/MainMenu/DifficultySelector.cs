using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.ContentSystem;
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
    public class DifficultySelector : MonoBehaviour
    {
        public GameObject DifficultyButtonPrefab;
        public Transform DifficultyButtonParent;
        public PropertyMenu PropertyMenu;
        private Button _selectedButton;

        private IEnumerable<Difficulty> _difficulties;
        private string DIFFICULTY_PATH = "*/Difficulty";
        private string DEFAULT_IDENTIFIER = "Core.Medium";

        private void Start ()
        {
            _difficulties = LoadDifficulties();
            InstantiateDifficultyButtons();

            Difficulty defaultDifficulty = _difficulties.FirstOrDefault(x => x.Identifier == DEFAULT_IDENTIFIER);
            if (defaultDifficulty == null)
            {
                defaultDifficulty = _difficulties.First();
            }

            SelectDifficulty(defaultDifficulty);

            PropertyMenu.OnPropertyChanged += OnPropertyMenuPropertyChanged;
        }

        private void OnPropertyMenuPropertyChanged()
        {
            _selectedButton.interactable = true;
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

                AddButtonListener(button, difficulty);
                if (difficulty.Identifier == DEFAULT_IDENTIFIER)
                {
                    button.interactable = false; // Kind of hacky but can't be bothered with the alternative. Will be fixed if it ever becomes a problem.
                    _selectedButton = button;
                }
            }
        }

        private void AddButtonListener (Button button, Difficulty difficulty)
        {
            button.onClick.AddListener(() => SelectDifficulty(button, difficulty));
        }

        private void SelectDifficulty(Button button, Difficulty difficulty)
        {
            _selectedButton.interactable = true;
            SelectDifficulty(difficulty);
            _selectedButton = button;
            _selectedButton.interactable = false;
        }

        private void SelectDifficulty (Difficulty difficulty)
        {
            Difficulty newDifficulty = difficulty.DeepClone();
            BattlefieldSettings.CurrentSettings.Difficulty = newDifficulty;
            PropertyMenu.Clear();
            newDifficulty.AddPropertiesTo(PropertyMenu);
        }

        private IEnumerable<Difficulty> LoadDifficulties ()
        {
            Difficulty[] loaded = Content.GetAll<Difficulty>(DIFFICULTY_PATH);
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
    }
}
