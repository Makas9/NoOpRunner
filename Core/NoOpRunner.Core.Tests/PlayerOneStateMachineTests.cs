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
            PlayerOneStateMachine player = new PlayerOneStateMachine();
            var expected = player.State;

            // Act
            var actual = new IdleState();

            // Assert
            expected.ShouldBeEquivalentTo(actual);
        }

        [Fact]
        public void SetState_WhenValidStateGiven_ShouldSetCurrentState()
        {
            // Arrange
            PlayerOneStateMachine player = new PlayerOneStateMachine();
            player.SetState(new JumpingState());
            var expected = player.State;

            // Act
            var actual = new JumpingState();

            // Assert
            expected.ShouldBeEquivalentTo(actual);
        }

        [Fact]
        public void GetStateUri_WhenNothingGiven_ShouldReturnAnimationUri()
        {
            // Arrange
            PlayerOneStateMachine player = new PlayerOneStateMachine();
            var expected = player.GetStateUri();

            // Act
            var actual = ResourcesUriHandler.GetIdleAnimationUri();

            // Assert
            expected.ShouldBe(actual);
        }

        [Fact]
        public void Run_WhenNothingGiven_ShouldSetRunningState()
        {
            // Arrange
            PlayerOneStateMachine player = new PlayerOneStateMachine();
            player.Run();
            var expected = player.State;

            // Act
            var actual = new RunningState();

            // Assert
            expected.ShouldBeEquivalentTo(actual);
        }

        [Fact]
        public void TurnLeft_WhenNothingGiven_ShouldSetIsLookingLeft()
        {
            // Arrange
            PlayerOneStateMachine player = new PlayerOneStateMachine();
            player.TurnLeft();
            var expected = player.IsLookingLeft;

            // Act
            var actual = true;

            // Assert
            expected.ShouldBe(actual);
        }
    }
}
