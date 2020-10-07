using System;
using System.Collections.Generic;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;

namespace NoOpRunner.Core
{
    public class ClientSubject : ISubject
    {
        private IDictionary<MessageType, IObserver> Observers { get; }

        protected ClientSubject()
        {
            Observers = new Dictionary<MessageType, IObserver>();
        }

        public void Notify(NoOpRunner sender, MessageType observerType, object arg = null)
        {
            if (arg == null)
            {
                throw new Exception("Need observer and/or player state");
            }

            if (Observers.TryGetValue(observerType, out var observer))
            {
                observer.Update(sender, arg);
            }
            else
            {
                throw new NullReferenceException("Observer not subscribed");
            }
        }

        public void AddObserver(IObserver observer, MessageType observerType)
        {
            if (observerType == null)
            {
                throw new Exception("Observer type needed");
            }
            
            Observers.Add((MessageType)observerType, observer);
        }

        public void RemoveObserver(IObserver observer, MessageType observerType)
        {

            if (observer.GetType() != Observers[(MessageType)observerType].GetType())
            {
                throw new Exception("Observer mismatch with type");
            }
            
            Observers.Remove(observerType);
        }
    }
}