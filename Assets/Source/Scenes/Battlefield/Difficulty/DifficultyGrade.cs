using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.Battlefield.Difficulty
{
    [CreateAssetMenu(fileName = "NewDifficultyGrade", menuName = "BFA2/Difficulty/Grade")]
    public class DifficultyGrade : ScriptableObject
    {
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Identifier;
        [TextArea]
        [ModelProperty]
        public string Description;
        [ModelProperty]
        public int DifficultyIndex;

        private const string GRADE_PATH = "*/Difficulty/Grades";
        private static ContentArrayReference<DifficultyGrade> _dífficulties = new ContentArrayReference<DifficultyGrade>(GRADE_PATH);


        public static DifficultyGrade GetGrade(string identifier) => _dífficulties.FirstOrDefault(x => x.Identifier == identifier);
    }
}
