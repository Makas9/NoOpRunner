using NoOpRunner.Client.Logic.Interfaces;
using System.Collections.Generic;

namespace NoOpRunner.Client.Logic.Commands
{
    public class CommandInvoker
    {
        private LinkedList<ICommand> commandQueue;

        public CommandInvoker()
        {
            commandQueue = new LinkedList<ICommand>();
        }

        public void InvokeCommand(ICommand command)
        {
            var result = command.Execute();

            if (result)
                commandQueue.AddFirst(command);
        }

        public void UndoCommands()
        {
            if (commandQueue.Count == 0) return;

            var last = commandQueue.Last;

            last.Value.Undo();

            while (last.Previous != null)
            {
                last = last.Previous;

                last.Value.Undo();
            }

            commandQueue.Clear();
        }

        public void UndoLastCommand()
        {
            if (commandQueue.Count == 0) return;

            var last = commandQueue.Last;

            last.Value.Undo();

            commandQueue.RemoveLast();
        }
    }
}
