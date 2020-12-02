using NoOpRunner.Client.Logic.Interfaces;
using NoOpRunner.Core;
using System.Collections.Generic;

namespace NoOpRunner.Client.Logic
{
    public class MementoCaretaker
    {
        private Stack<IMemento> mementos;

        public MementoCaretaker()
        {
            mementos = new Stack<IMemento>();
        }

        public void AddMemento(IMemento memento)
        {
            Logging.Instance.Write($"[Memento] Memento added to caretaker.", LoggingLevel.Memento);
            mementos.Push(memento);
        }

        public IMemento GetLastMemento()
        {
            if (mementos.Count == 0)
            {
                Logging.Instance.Write($"[Memento] Restore attempted with no mementos present.", LoggingLevel.Memento);
                return null;
            }

            Logging.Instance.Write($"[Memento] Memento created at {mementos.Peek().CreationTime} retreived from caretaker.", LoggingLevel.Memento);
            return mementos.Pop();
        }
    }
}
