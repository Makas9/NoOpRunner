using System;
using System.Collections.Generic;

namespace NoOpRunner.Core
{
    /// <summary>
    /// Disposed object holds here, take if needed
    /// </summary>
    public static class DisposedObjectsPool
    {
        private static Dictionary<Type, List<object>> ObjectPool { get;}
        
        static DisposedObjectsPool()
        {
            ObjectPool = new Dictionary<Type, List<object>>();
        }

        public static void Push(object objectToPush)
        {
            if (!ObjectPool.ContainsKey(objectToPush.GetType()))
            
                ObjectPool.Add(objectToPush.GetType(), new List<object>());
            
            ObjectPool[objectToPush.GetType()].Add(objectToPush);
        }

        public static T Pop<T>()
        {
            if (!ObjectPool.ContainsKey(typeof(T)) && ObjectPool[typeof(T)].Count == 0)
            {
                throw new NullReferenceException();
            }
            
            var objectToPop = ObjectPool[typeof(T)][0];
            
            ObjectPool[typeof(T)].RemoveAt(0);
            
            return (T)objectToPop;
        }

        public static bool Contains<T>()
        {
            if (ObjectPool.ContainsKey(typeof(T)))

                return ObjectPool[typeof(T)].Count > 0; 
            
            return false;
        }
    }
}