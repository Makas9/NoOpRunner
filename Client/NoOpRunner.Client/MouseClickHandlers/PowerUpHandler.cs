using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Client.MouseClickHandlers
{
    public class PowerUpHandler : MouseClickHandler
    {
        public PowerUpHandler(Core.NoOpRunner game) : base(game)
        {
        }

        protected override void HandleMouseClick(int positionX, int positionY)
        {
            if ((Game.Player.CenterPosX == positionX && Game.Player.CenterPosY == positionY) || (Game.Player.CenterPosX == positionX && Game.Player.CenterPosY+1 == positionY))
                
                return;
                
            var powerUp = Game.PlayerTwo.UseActivePowerUp;

            if (powerUp == null) 
                
                return;
            
            var powerUpEntity = new PowerUp(positionX, positionY, (PowerUps)powerUp);
                
            Game.PowerUpsContainer.AddMapPart(powerUpEntity);
            
            Game.SendHostActivePowerUp(positionX, positionY, (PowerUps)powerUp);
        }
    }
}