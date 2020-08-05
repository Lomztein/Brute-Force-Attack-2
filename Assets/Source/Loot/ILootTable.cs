namespace Lomztein.BFA2.Loot
{
    public interface ILootTable
    {
        RandomizedLoot GetRandomLoot(float chanceScalar, float amountScalar);
    }
}