﻿using NoOpRunner.Client.Logic.Interfaces;
using NoOpRunner.Core;
using System;

namespace NoOpRunner.Client.Logic.Commands
{
    public abstract class BaseCommand : ICommand
    {
        protected virtual bool PreExecute() => throw new NotImplementedException();

        protected virtual bool ExecuteInternal() => throw new NotImplementedException();

        public bool Execute()
        {
            if (PreExecute())
                return ExecuteInternal();

            return false;
        }

        public virtual void Undo() => throw new NotImplementedException();
    }

    public abstract class BaseCommand<TRequest> : BaseCommand, ICommand<TRequest>
    {
        protected virtual bool PreExecute(TRequest request) => throw new NotImplementedException();

        protected virtual bool ExecuteInternal(TRequest request) => throw new NotImplementedException();

        public bool Execute(TRequest request)
        {
            Logging.Instance.Write($"Template method Execute called from type {this.GetType()}.", LoggingLevel.TemplateMethod);
            if (PreExecute(request))
                return ExecuteInternal(request);

            return false;
        }
    }
}
