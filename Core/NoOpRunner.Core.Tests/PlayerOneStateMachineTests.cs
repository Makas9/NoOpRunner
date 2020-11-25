using NoOpRunner.Core.Enums;
using Xunit;
using Shouldly;
using NoOpRunner.Core.Shapes.ShapeFactories;
using NoOpRunner.Core.Shapes.EntityShapes;
using NoOpRunner.Core.PlayerStates;
using System;

namespace NoOpRunner.Core.Tests
{
    public class PlayerOneStateMachineTests
    {
        [Fact]
        public void Initial_WhenNothingGiven_ShouldHaveIdleState()
        {
            // Arrange
            var expected = new IdleState();
            PlayerOneStateMachine player = new PlayerOneStateMachine();
            var actual = player.State;

            // Act

            // Assert
            expected.ShouldBeEquivalentTo(actual);
        }

        [Fact]
        public void SetState_WhenValidStateGiven_ShouldSetCurrentState()
        {
            // Arrange
            var expected = new JumpingState();
            PlayerOneStateMachine player = new PlayerOneStateMachine();

            // Act
            player.SetState(new JumpingState());

            // Assert
            expected.ShouldBeEquivalentTo(player.State);
        }

        [Fact]
        public void GetStateUri_WhenNothingGiven_ShouldReturnAnimationUri()
        {
            // Arrange
            var expected = ResourcesUriHandler.GetIdleAnimationUri();
            PlayerOneStateMachine player = new PlayerOneStateMachine();
            var actual = player.GetStateUri();

            // Assert
            expected.ShouldBe(actual);
        }

        [Fact]
        public void Run_WhenNothingGiven_ShouldSetRunningState()
        {
            // Arrange
            var expected = new RunningState();
            PlayerOneStateMachine player = new PlayerOneStateMachine();

            // Act
            player.Run();

            // Assert
            expected.ShouldBeEquivalentTo(player.State);
        }

        [Fact]
        public void TurnLeft_WhenNothingGiven_ShouldSetIsLookingLeft()
        {
            // Arrange
            var expected = true;
            PlayerOneStateMachine player = new PlayerOneStateMachine();

            // Act
            player.TurnLeft();

            // Assert
            expected.ShouldBe(player.IsLookingLeft);
        }
    }
}
