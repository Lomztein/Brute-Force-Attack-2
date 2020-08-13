namespace Lomztein.BFA2.Enemies.Scalers
{
    public interface IEnemyScaler
    {
        bool CanScale(IEnemy enemy);
        void Scale(IEnemy enemy);
    }
}