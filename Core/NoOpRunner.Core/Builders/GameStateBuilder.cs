using NoOpRunner.Core.Configurators;
using NoOpRunner.Core.Interfaces;
using System;

namespace NoOpRunner.Core.Builders
{
    public class GameStateBuilder
    {
        private IGameStateConfigurator configurator;

        public IGameStateConfigurator Configure()
        {
            return configurator = new GameStateConfigurator();
        }

        public IMapPart Build()
        {
            if (configurator == null) throw new Exception("Configurator uninitialized");

            return configurator.Build();
        }
    }
}
