using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.Inventory.Items;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Scenes.Battlefield.BattlefieldAssemblerParts
{
    // Stores information about the player, such as resources and inventory.
    public class PlayerAssemblerPart : IBattlefieldAssemblerPart
    {
        public string Identifier => "Core.Player";
        public int AssemblyOrder => 10;

        public void AssemblePart(BattlefieldController controller, ValueModel partData, AssemblyContext context)
        {
            ObjectModel model = partData as ObjectModel;
            var resources = model.GetObject("Resources");
            foreach (var resource in resources)
            {
                Resource res = Resource.GetResource(resource.Name);
                var value = (resource.Model as PrimitiveModel).ToObject<int>();
                Player.Player.Resources.SetResource(res, value);
            }

            // Clear existing inventory..
            var inventory = model.GetArray("Inventory");
            var items = Player.Player.Inventory.ToArray();
            foreach (var item in items)
            {
                Player.Player.Inventory.RemoveItem(item);
            }

            // Put in saved inventory.
            ObjectAssembler assembler = new ObjectAssembler();
            foreach (var itemModel in inventory)
            {
                var item = assembler.Assemble(itemModel, typeof(Item), context) as Item;
                Player.Player.Inventory.AddItem(item);
            }
        }

        public ValueModel DisassemblePart(BattlefieldController controller, DisassemblyContext context)
        {
            ArrayModel inventory = new ArrayModel(Player.Player.Inventory.Select(x => ValueModelFactory.Create(x, context).MakeExplicit(x.GetType())));
            var resourceTypes = Resource.GetResources();
            var fields = new List<ObjectField>();

            foreach (var resourceType in resourceTypes)
            {
                int amount = Player.Player.Resources.GetResource(resourceType);
                fields.Add(new ObjectField(resourceType.Identifier, new PrimitiveModel(amount)));
            }
            ObjectModel resources = new ObjectModel(fields.ToArray());

            return new ObjectModel(
                new ObjectField("Resources", resources),
                new ObjectField("Inventory", inventory)
                );
        }
    }
}
