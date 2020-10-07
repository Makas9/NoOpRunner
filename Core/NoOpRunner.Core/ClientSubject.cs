using System;
using System.Collections.Generic;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;

namespace NoOpRunner.Core
{
    public class ClientSubject : ISubject
    {
        private IDictionary<MessageType, IObserver> Observers { get; set; }

        protected ClientSubject()
        {
            Observers = new Dictionary<MessageType, IObserver>();
        }

        public void Notify(object sender, object observerType = null, object arg = null)
        {
            if (observerType == null || arg == null)
            {
                throw new Exception("Need observer and/or player state");
            }

            if (Observers.TryGetValue((MessageType)observerType, out var observer))
            {
                observer.Update(sender, arg);
            }
            else
            {
                throw new NullReferenceException("Observer not subscribed");
            }
        }

        public void AddObserver(IObserver observer, object arg = null)
        {
            if (arg == null)
            {
                throw new Exception("Observer type needed");
            }
            
            Observers.Add((MessageType)arg, observer);
        }

        public void RemoveObserver(IObserver observer, object arg = null)
        {
            if ( arg == null)
            {
                throw new Exception("Observer type needed");
            }

            if (observer.GetType() != Observers[(MessageType)arg].GetType())
            {
                throw new Exception("Observer mismatch with type");
            }
            
            Observers.Remove((MessageType) arg);
        }
    }
}