using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Dtos
{
    public class PlayerStateDto
    {
        public PlayerOneState State { get; set; }
        
        public bool IsLookingLeft { get; set; }
        
        public int CenterPosX { get; set; }
        public int CenterPosY { get; set; }
    }
}