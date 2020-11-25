using System;
using NoOpRunner.Client.PlayerAnimationDecorators;
using NoOpRunner.Client.Rendering;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;
using Shouldly;
using Xunit;

namespace NoOpRunner.Client.Tests
{
    public class PlayerDecoratorsTests
    {
        [Theory]
        [InlineData(VisualElementType.Invulnerability)]
        [InlineData(VisualElementType.SpeedBoost)]
        [InlineData(VisualElementType.Player)]
        public void RemoveLayer_WhenLayerDoesntExist_ShouldReturnSameIVisual(VisualElementType visualElementType)
        {
            //Arrange
            IVisualElement player = new PlayerRenderer(new Player(0, 0));
            
            player = new PlayerDoubleJumpDecorator(player);
            //
            
            //Act
            IVisualElement afterLayerRemove = ((PlayerDecorator) player).RemoveLayer(visualElementType);
            //
            
            //Assert
            player.ShouldBe(afterLayerRemove);
        }

        [Theory]
        [InlineData(VisualElementType.DoubleJump)]
        [InlineData(VisualElementType.Invulnerability)]
        [InlineData(VisualElementType.SpeedBoost)]
        public void RemoveLayer_WhenRemovingLayerExistsWithOneLayerOnRenderer_ShouldReturnRenderer(
            VisualElementType visualElementType)
        {
            //Arrange
            PlayerRenderer playerRenderer = new PlayerRenderer(new Player(0, 0));

            IVisualElement decoratorLayerOnRenderer;
            
            switch (visualElementType)
            {
                case VisualElementType.DoubleJump:
                    decoratorLayerOnRenderer = new PlayerDoubleJumpDecorator(playerRenderer);
                    break;
                case VisualElementType.Invulnerability:
                    decoratorLayerOnRenderer = new PlayerInvulnerabilityDecorator(playerRenderer);
                    break;
                case VisualElementType.SpeedBoost:
                    decoratorLayerOnRenderer = new PlayerSpeedBoostDecorator(playerRenderer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(visualElementType), visualElementType, null);
            }
            //
            
            //Act
            IVisualElement afterLayerRemove =
                ((PlayerDecorator) decoratorLayerOnRenderer).RemoveLayer(visualElementType);
            //
            
            //Assert
            playerRenderer.ShouldBe(afterLayerRemove);
        }

        [Fact]
        public void RemoveLayer_WhenRemovingAllLayers_ShouldReturnRenderer()
        {
            //Arrange
            PlayerRenderer root = new PlayerRenderer(new Player(0, 0));
            
            IVisualElement layer = new PlayerInvulnerabilityDecorator(root);
            
            layer = new PlayerDoubleJumpDecorator(layer);
            
            layer = new PlayerSpeedBoostDecorator(layer);
            //
            
            //Act
            layer = ((PlayerDecorator) layer).RemoveLayer(VisualElementType.Invulnerability);
            
            layer = ((PlayerDecorator) layer).RemoveLayer(VisualElementType.SpeedBoost);
            
            layer = ((PlayerDecorator) layer).RemoveLayer(VisualElementType.DoubleJump);
            //
            
            //Assert
            layer.ShouldBe(root);
        }
        
        [Fact]
        public void RemoveLayer_WhenRemovingMiddleLayer_ShouldReturnTopLayer()
        {
            //Arrange
            IVisualElement layer = new PlayerRenderer(new Player(0, 0));
            
            layer = new PlayerInvulnerabilityDecorator(layer);

            layer = new PlayerSpeedBoostDecorator(layer);
            //
            
            //Act
            IVisualElement afterLayerRemoved = ((PlayerDecorator) layer).RemoveLayer(VisualElementType.Invulnerability);
            //
            
            //Assert
            layer.ShouldBe(afterLayerRemoved);
        }
    }
}