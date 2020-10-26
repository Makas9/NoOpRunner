using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using NoOpRunner.Client.PlayerAnimationDecorators;
using NoOpRunner.Client.Rendering;
using NoOpRunner.Core;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;

namespace NoOpRunner.Client
{
    public class RenderGameFramesFacade
    {
        private IVisualElement PlayerRenderer { get; set; }

        private IVisualElement PlatformsRenderer { get; set; }

        private IVisualElement PowerUpsRenderer { get; set; }

        private IList<PowerUps> DisplayingPlayerOnePowerUps { get; set; }
        
        private int CountBetweenFrames { get; set; }
        public RenderGameFramesFacade()
        {
            DisplayingPlayerOnePowerUps = new List<PowerUps>();

            CountBetweenFrames = 0;
        }


        public async Task CycleHostGame(Core.NoOpRunner game, Canvas playerCanvas, Canvas powerUpsCanvas,
            Canvas platformsCanvas)
        {
            if (CountBetweenFrames == GameSettings.TimeBetweenMapStep)
            {
                CountBetweenFrames = 0;

                await game.OnMapMoveLoopFired();
            }

            CountBetweenFrames++;
            
            game.Player.OnLoopFired((WindowPixel[,]) game.PlatformsContainer.GetShapes().Clone());
            
            await game.UpdateClientsGame();
            
            BaseCycle(game, playerCanvas, platformsCanvas, powerUpsCanvas);
        }

        public void CycleClientGame(Core.NoOpRunner game, Canvas playerCanvas, Canvas platformsCanvas,
            Canvas powerUpsCanvas)
        {
            //More incoming
            BaseCycle(game, playerCanvas, platformsCanvas, powerUpsCanvas);
        }

        private void BaseCycle(Core.NoOpRunner game, Canvas playerCanvas, Canvas platformsCanvas, Canvas powerUpsCanvas)
        {
            if (PlayerRenderer == null || PlatformsRenderer == null || PowerUpsRenderer == null)
            {
                PlayerRenderer = new PlayerRenderer(game.Player);

                PlatformsRenderer = new PlatformsRenderer(game.PlatformsContainer);

                PowerUpsRenderer = new PowerUpsRenderer(game.PowerUpsContainer);
            }
            
            game.Player.LoopPowerUps();

            var powerUp = game.PowerUpsContainer.GetPowerUpAt(game.Player.CenterPosX, game.Player.CenterPosY);

            if (powerUp != null)
            {
                if (powerUp.PowerUpType == PowerUps.Double_Jump)
                {
                    AddDecoratorLayer(powerUp.PowerUpType); //Add decorator layer    
                }

                if (game.IsHost)
                {
                    game.Player.TakePowerUp(powerUp.PowerUpType); //Player pick up power up    
                }

                game.PowerUpsContainer.RemovePowerUp(game.Player.CenterPosX,
                    game.Player.CenterPosY); //Remove power up from display
            }

            foreach (var usingPowerUp in game.Player.ActivePowerUps)
            {
                if (DisplayingPlayerOnePowerUps.Contains(usingPowerUp))

                    continue;

                DisplayingPlayerOnePowerUps.Add(usingPowerUp);

                AddDecoratorLayer(usingPowerUp);
            }

            var playerUsedPowerUp = game.Player.ExhaustedPowerUp;

            if (playerUsedPowerUp != null)
            {
                DisplayingPlayerOnePowerUps.Remove((PowerUps)playerUsedPowerUp);
                    
                GifImage animation;
                //Remove layer
                switch (playerUsedPowerUp)
                {
                    case PowerUps.Speed_Boost:
                        PlayerRenderer = ((PlayerDecorator) PlayerRenderer).RemoveLayer(VisualElementType.SpeedBoost);

                        animation = playerCanvas.Children.OfType<GifImage>()
                            .FirstOrDefault(x => x.VisualType == VisualElementType.SpeedBoost);

                        playerCanvas.Children.Remove(animation);
                        break;
                    case PowerUps.Invisibility:

                        break;
                    case PowerUps.Invulnerability:
                        PlayerRenderer =
                            ((PlayerDecorator) PlayerRenderer).RemoveLayer(VisualElementType.Invulnerability);

                        animation = playerCanvas.Children.OfType<GifImage>()
                            .FirstOrDefault(x => x.VisualType == VisualElementType.Invulnerability);

                        playerCanvas.Children.Remove(animation);
                        break;
                    case PowerUps.Double_Jump:
                        PlayerRenderer = ((PlayerDecorator) PlayerRenderer).RemoveLayer(VisualElementType.DoubleJump);

                        animation = playerCanvas.Children.OfType<GifImage>()
                            .FirstOrDefault(x => x.VisualType == VisualElementType.DoubleJump);

                        playerCanvas.Children.Remove(animation);

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            PowerUpsRenderer.Display(platformsCanvas);

            PlatformsRenderer.Display(powerUpsCanvas);

            PlayerRenderer.Display(playerCanvas);
        }

        private void AddDecoratorLayer(PowerUps elementType)
        {
            switch (elementType)
            {
                case PowerUps.Speed_Boost:
                    PlayerRenderer = new PlayerSpeedBoostDecorator(PlayerRenderer);
                    break;
                case PowerUps.Invisibility:
                    break;
                case PowerUps.Invulnerability:
                    PlayerRenderer = new PlayerInvulnerabilityDecorator(PlayerRenderer);

                    return;
                case PowerUps.Double_Jump:
                    PlayerRenderer = new PlayerDoubleJumpDecorator(PlayerRenderer);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null);
            }
        }
    }
}