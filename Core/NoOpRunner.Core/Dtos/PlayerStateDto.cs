using NoOpRunner.Core.PlayerStates;

namespace NoOpRunner.Core.Dtos
{
    public class PlayerStateDto
    {
        public PlayerState State { get; set; }
        
        public bool IsLookingLeft { get; set; }
        
        public int CenterPosX { get; set; }
        public int CenterPosY { get; set; }

        public int HealthPoints { get; set; }
        
        public PlayerOnePowerUps PlayerOnePowerUps { get; set; }
    }
}