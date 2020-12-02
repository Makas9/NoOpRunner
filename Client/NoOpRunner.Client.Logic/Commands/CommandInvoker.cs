using NoOpRunner.Client.Logic.Interfaces;
using NoOpRunner.Core;
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
            Logging.Instance.Write($"[Command] Command invoked: {command.GetType().Name}", LoggingLevel.Command);

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

                Logging.Instance.Write($"[Command] Undoing command: {command.Value.GetType().Name}", LoggingLevel.Command);

                command.Value.Undo();
            }

            commandQueue.Clear();
        }

        public void UndoLastCommand()
        {
            if (commandQueue.Count == 0) return;

            var command = commandQueue.First;

            Logging.Instance.Write($"[Command] Undoing command: {command.Value.GetType().Name}", LoggingLevel.Command);

            command.Value.Undo();

            commandQueue.RemoveFirst();
        }

        public void Reset()
        {
            Logging.Instance.Write("[Command] Clearing queue", LoggingLevel.Command);

            commandQueue.Clear();
        }
    }
}
