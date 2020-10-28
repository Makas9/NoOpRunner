using NoOpRunner.Client.Logic.Interfaces;
using System.Collections.Generic;

namespace NoOpRunner.Client.Logic.Commands
{
    public class CommandInvoker<TCommand> where TCommand : ICommand
    {
        private LinkedList<ICommand> commandQueue;

        public CommandInvoker()
        {
            commandQueue = new LinkedList<ICommand>();
        }

        public void InvokeCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            var result = command.Execute(request);

            if (result)
                commandQueue.AddFirst(command);
        }

        public void UndoCommands()
        {
            if (commandQueue.Count == 0) return;

            var command = commandQueue.First;

            command.Value.Undo();

            while (command.Next != null)
            {
                command = command.Next;

                command.Value.Undo();
            }

            commandQueue.Clear();
        }

        public void UndoLastCommand()
        {
            if (commandQueue.Count == 0) return;

            var command = commandQueue.First;

            command.Value.Undo();

            commandQueue.RemoveFirst();
        }

        public void Reset()
        {
            commandQueue.Clear();
        }
    }
}
