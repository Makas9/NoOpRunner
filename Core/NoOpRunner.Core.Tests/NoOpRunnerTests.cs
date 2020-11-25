using Moq;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Interfaces;
using Shouldly;
using System;
using Xunit;

namespace NoOpRunner.Core.Tests
{
    public class NoOpRunnerTests
    {
        [Fact]
        public void NoOpRunner_WhenHostingStarted_ShouldCallConnectionManager()
        {
            // Arrange
            var connectionManagerMock = new Mock<IConnectionManager>();
            var game = new NoOpRunner(connectionManagerMock.Object);

            // Act
            game.StartHosting();

            //Assert
            connectionManagerMock.Verify(x => x.Start(It.IsAny<string>(), It.IsAny<Action<MessageDto>>()), Times.Once);
            game.IsHost.ShouldBe(true);
        }

        [Fact]
        public void NoOpRunner_WhenConnecting_ShouldCallConnectionManager()
        {
            // Arrange
            var connectionManagerMock = new Mock<IConnectionManager>();
            var game = new NoOpRunner(connectionManagerMock.Object);

            // Act
            game.ConnectToHub().GetAwaiter().GetResult();

            //Assert
            connectionManagerMock.Verify(x => x.Connect(It.IsAny<string>(), It.IsAny<Action<MessageDto>>()), Times.Once);
            game.IsHost.ShouldBe(false);
        }

        [Fact]
        public void NoOpRunner_OnMapLoopFired_ShouldSendPlatformsUpdate()
        {
            // Arrange
            var connectionManagerMock = new Mock<IConnectionManager>();
            var game = new NoOpRunner(connectionManagerMock.Object);
            game.StartHosting();

            // Act
            game.OnMapMoveLoopFired().GetAwaiter().GetResult();

            //Assert
            connectionManagerMock.Verify(x => x.SendMessageToClient(It.Is<MessageDto>(m => m.MessageType == Enums.MessageType.PlatformsUpdate)), Times.Once);
        }
    }
}
