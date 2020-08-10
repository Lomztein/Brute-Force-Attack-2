using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Misc
{
    public static class GlobalUpdate
    {
        public static void BroadcastUpdate()
        {
            var recievers = UnityEngine.Object.FindObjectsOfType<Component>().Where(x => x is IGlobalUpdateReciever);
            foreach (IGlobalUpdateReciever reciever in recievers)
            {
                reciever.OnGlobalUpdateRecieved();
            }
        }

        public static void BroadcastUpdate<T>(T message)
        {
            var recievers = UnityEngine.Object.FindObjectsOfType<Component>().Where(x => x is IGlobalUpdateReciever<T>);
            foreach (IGlobalUpdateReciever<T> reciever in recievers)
            {
                reciever.OnGlobalUpdateRecieved(message);
            }
        }
    }

    public interface IGlobalUpdateReciever
    {
        void OnGlobalUpdateRecieved();
    }

    public interface IGlobalUpdateReciever<T>
    {
        void OnGlobalUpdateRecieved(T message);
    }

}
