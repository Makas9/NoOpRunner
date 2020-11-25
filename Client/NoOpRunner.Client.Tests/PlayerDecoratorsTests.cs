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
            IVisualElement player = new PlayerRenderer(new Player(0, 0));
            
            player = new PlayerDoubleJumpDecorator(player);

            IVisualElement afterLayerRemove = ((PlayerDecorator) player).RemoveLayer(visualElementType);
            
            player.ShouldBe(afterLayerRemove);
        }

        [Theory]
        [InlineData(VisualElementType.DoubleJump)]
        [InlineData(VisualElementType.Invulnerability)]
        [InlineData(VisualElementType.SpeedBoost)]
        public void RemoveLayer_WhenRemovingLayerExistsWithOneLayerOnRenderer_ShouldReturnRenderer(
            VisualElementType visualElementType)
        {
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

            IVisualElement afterLayerRemove =
                ((PlayerDecorator) decoratorLayerOnRenderer).RemoveLayer(visualElementType);
            
            playerRenderer.ShouldBe(afterLayerRemove);
        }

        [Fact]
        public void RemoveLayer_WhenRemovingAllLayers_ShouldReturnRenderer()
        {
            PlayerRenderer root = new PlayerRenderer(new Player(0, 0));
            
            //Prepare
            IVisualElement layer = new PlayerInvulnerabilityDecorator(root);
            
            layer = new PlayerDoubleJumpDecorator(layer);
            
            layer = new PlayerSpeedBoostDecorator(layer);
            //
            
            //Remove layers
            layer = ((PlayerDecorator) layer).RemoveLayer(VisualElementType.Invulnerability);
            
            layer = ((PlayerDecorator) layer).RemoveLayer(VisualElementType.SpeedBoost);
            
            layer = ((PlayerDecorator) layer).RemoveLayer(VisualElementType.DoubleJump);
            //
            
            layer.ShouldBe(root);//test
        }
    }
}