﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using NoOpRunner.Core.Decorators;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Rendering;

namespace NoOpRunner.Core
{
    public class GameStepCycleFacade
    {
        private IVisualElement Player { get; set; }

        public async Task HostGameCycle(NoOpRunner game, Canvas playerCanvas, Canvas powerUpsCanvas,
            Canvas platformsCanvas)
        {
            if (Player == null)
            {
                Player = game.Player;
            }

            game.Player.OnLoopFired((WindowPixel[,]) game.PlatformsContainer.GetShapes().Clone());

            var powerUp = game.PowerUpsContainer.GetPowerUpAt(game.Player.CenterPosX, game.Player.CenterPosY);

            if (powerUp != null)
            {
                AddDecoratorLayer(powerUp.PowerUpType);//Add decorator layer
                
                game.Player.TakePowerUp(powerUp.PowerUpType);//Player pick up power up

                game.PowerUpsContainer.RemovePowerUp(game.Player.CenterPosX, game.Player.CenterPosY);//Remove power up from display
            }

            var playerUsedPowerUp = game.Player.UsedPowerUp;
            
            if (playerUsedPowerUp != null)
            {
                playerCanvas.Children.Clear();
                
                //Remove layer
                switch (playerUsedPowerUp)
                {
                    case PowerUps.Speed_Boost:
                        Player = ((PlayerDecorator) Player).RemoveLayer(VisualElementType.SpeedBoost);
                        break;
                    case PowerUps.Invisibility:
                        
                        break;
                    case PowerUps.Invulnerability:
                        Player = ((PlayerDecorator) Player).RemoveLayer(VisualElementType.Invulnerability);
                        break;
                    case PowerUps.Double_Jump:
                        Player = ((PlayerDecorator) Player).RemoveLayer(VisualElementType.DoubleJump);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            game.PlatformsContainer.Display(platformsCanvas);

            game.PowerUpsContainer.Display(powerUpsCanvas);

            Player.Display(playerCanvas);

            await game.UpdateClientsGame();
        }

        private void AddDecoratorLayer(PowerUps elementType)
        {
            switch (elementType)
            {
                case PowerUps.Speed_Boost:
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