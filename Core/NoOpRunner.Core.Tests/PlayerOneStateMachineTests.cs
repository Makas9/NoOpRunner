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
            var actual = new IdleState();
            PlayerOneStateMachine player = new PlayerOneStateMachine();
            var expected = player.State;

            // Assert
            expected.ShouldBeEquivalentTo(actual);
        }

        [Fact]
        public void SetState_WhenValidStateGiven_ShouldSetCurrentState()
        {
            // Arrange
            var actual = new JumpingState();
            PlayerOneStateMachine player = new PlayerOneStateMachine();

            // Act
            player.SetState(new JumpingState());

            // Assert
            player.State.ShouldBeEquivalentTo(actual);
        }

        [Fact]
        public void GetStateUri_WhenNothingGiven_ShouldReturnAnimationUri()
        {
            // Arrange
            var actual = ResourcesUriHandler.GetIdleAnimationUri();
            PlayerOneStateMachine player = new PlayerOneStateMachine();
            var expected = player.GetStateUri();

            // Assert
            expected.ShouldBe(actual);
        }

        [Fact]
        public void Run_WhenNothingGiven_ShouldSetRunningState()
        {
            // Arrange
            var actual = new RunningState();
            PlayerOneStateMachine player = new PlayerOneStateMachine();

            // Act
            player.Run();

            // Assert
            player.State.ShouldBeEquivalentTo(actual);
        }

        [Fact]
        public void TurnLeft_WhenNothingGiven_ShouldSetIsLookingLeft()
        {
            // Arrange
            var actual = true;
            PlayerOneStateMachine player = new PlayerOneStateMachine();

            // Act
            player.TurnLeft();

            // Assert
            player.IsLookingLeft.ShouldBe(actual);
        }
    }
}
