using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Battlefield.Difficulty
{
    public enum Grade
    {
        Easy, Medium, Hard, Brutal, Impossible
    }

    public class DifficultySettings
    {
        public string Name;
        public string Description;
        public Grade Grading;

        private List<IDifficultyAspect> _difficultyAspects = new List<IDifficultyAspect>()
        {
            new EnemyScalerDifficultyAspect(),
            new EnemySpawnDifficultyAspect(),
            new ResourcesDifficultyAspect(),
        };

        public IDifficultyAspect[] GetAspects => _difficultyAspects.ToArray();

        public void AddAspects(IEnumerable<IDifficultyAspect> aspects) => _difficultyAspects.AddRange(aspects);
        public void AddAspects(params IDifficultyAspect[] aspects) => AddAspects(aspects as IEnumerable<IDifficultyAspect>);

        public void Apply ()
        {
            foreach (var aspect in _difficultyAspects)
            {
                aspect.Apply();
            }
        }
    }
}
