using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using NoOpRunner.Core.Decorators;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Rendering;

namespace NoOpRunner.Core
{
    public class GameFrameCycleFacade
    {
        private IVisualElement Player { get; set; }
        
        private IList<PowerUps> DisplayingPlayerOnePowerUps { get; set; }

        public GameFrameCycleFacade()
        {
            DisplayingPlayerOnePowerUps = new List<PowerUps>();
        }

        public async Task HostGameCycle(NoOpRunner game, Canvas playerCanvas, Canvas powerUpsCanvas,
            Canvas platformsCanvas)
        {

            game.Player.OnLoopFired((WindowPixel[,]) game.PlatformsContainer.GetShapes().Clone());

            BaseCycle(game, playerCanvas, platformsCanvas, powerUpsCanvas);

            await game.UpdateClientsGame();
        }

        public void ClientGameCycle(NoOpRunner game, Canvas playerCanvas, Canvas platformsCanvas, Canvas powerUpsCanvas)
        {
            //More incoming
            BaseCycle(game, playerCanvas, platformsCanvas, powerUpsCanvas);
        }

        private void BaseCycle(NoOpRunner game, Canvas playerCanvas, Canvas platformsCanvas, Canvas powerUpsCanvas)
        {
            if (Player == null)
            {
                Player = game.Player;
            }
            
            game.Player.LoopPowerUps();
            
            var powerUp = game.PowerUpsContainer.GetPowerUpAt(game.Player.CenterPosX, game.Player.CenterPosY);

            if (powerUp != null)
            {
                if (powerUp.PowerUpType == PowerUps.Double_Jump)
                {
                    AddDecoratorLayer(powerUp.PowerUpType);//Add decorator layer    
                }
                
                game.Player.TakePowerUp(powerUp.PowerUpType);//Player pick up power up

                game.PowerUpsContainer.RemovePowerUp(game.Player.CenterPosX, game.Player.CenterPosY);//Remove power up from display
            }

            foreach (var usingPowerUp in game.Player.UsingPowerUps)
            {
                if (DisplayingPlayerOnePowerUps.Contains(usingPowerUp)) 
                    
                    continue;
                
                DisplayingPlayerOnePowerUps.Add(usingPowerUp);

                AddDecoratorLayer(usingPowerUp);
            }

            var playerUsedPowerUp = game.Player.UsedPowerUp;
            
            if (playerUsedPowerUp != null)
            {
                // playerCanvas.Children.Clear();
                GifImage animation;
                //Remove layer
                switch (playerUsedPowerUp)
                {
                    case PowerUps.Speed_Boost:
                        Player = ((PlayerDecorator) Player).RemoveLayer(VisualElementType.SpeedBoost);
                        
                        animation = playerCanvas.Children.OfType<GifImage>()
                            .FirstOrDefault(x => x.VisualType == VisualElementType.SpeedBoost);
                        
                        playerCanvas.Children.Remove(animation);
                        break;
                    case PowerUps.Invisibility:
                        
                        break;
                    case PowerUps.Invulnerability:
                        Player = ((PlayerDecorator) Player).RemoveLayer(VisualElementType.Invulnerability);
                        
                        animation = playerCanvas.Children.OfType<GifImage>()
                            .FirstOrDefault(x => x.VisualType == VisualElementType.Invulnerability);
                        
                        playerCanvas.Children.Remove(animation);
                        break;
                    case PowerUps.Double_Jump:
                        Player = ((PlayerDecorator) Player).RemoveLayer(VisualElementType.DoubleJump);
                        
                        animation = playerCanvas.Children.OfType<GifImage>()
                            .FirstOrDefault(x => x.VisualType == VisualElementType.DoubleJump);
                        
                        playerCanvas.Children.Remove(animation);
                        
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            game.PlatformsContainer.Display(platformsCanvas);

            game.PowerUpsContainer.Display(powerUpsCanvas);

            Player.Display(playerCanvas);
        }

        private void AddDecoratorLayer(PowerUps elementType)
        {
            switch (elementType)
            {
                case PowerUps.Speed_Boost:
                    Player = new PlayerSpeedBoostDecorator(Player);
                    break;
                case PowerUps.Invisibility:
                    break;
                case PowerUps.Invulnerability:
                    Player = new PlayerInvulnerabilityDecorator(Player);

                    return;
                case PowerUps.Double_Jump:
                    Player = new PlayerDoubleJumpDecorator(Player);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null);
            }
        }
    }
}