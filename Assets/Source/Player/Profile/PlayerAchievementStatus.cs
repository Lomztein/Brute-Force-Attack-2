using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Player.Profile
{
    public class PlayerAchievementStatus : IAssemblable
    {
        public string Identifier;
        public bool IsCompleted;
        public DateTime CompletedOn;
        public ValueModel Progression;

        public PlayerAchievementStatus (string identifier)
        {
            Identifier = identifier;
        }

        public PlayerAchievementStatus()
        {
        }

        public void Assemble(ValueModel source)
        {
            ObjectModel model = source as ObjectModel;
            Identifier = model.GetValue<string>("Identifier");
            IsCompleted = model.GetValue<bool>("IsCompleted");
            CompletedOn = model.GetValue<DateTime>("CompletedOn");
            Progression = model.GetProperty("Progression");
        }

        public ValueModel Disassemble()
        {
            return new ObjectModel(
                new ObjectField("Identifier", new PrimitiveModel(Identifier)),
                new ObjectField("IsCompleted", new PrimitiveModel(IsCompleted)),
                new ObjectField("CompletedOn", new PrimitiveModel(CompletedOn)),
                new ObjectField("Progression", Progression));
        }
    }
}
