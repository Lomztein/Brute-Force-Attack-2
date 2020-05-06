namespace Lomztein.BFA2.Modification.Events
{
    public interface IEventCaller<T> where T : IEventArgs
    {
        void CallEvent(T args);
    }
}