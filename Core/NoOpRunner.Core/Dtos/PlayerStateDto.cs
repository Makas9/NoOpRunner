using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Dtos
{
    public class PlayerStateDto
    {
        public PlayerOneState State { get; set; }
        
        public bool IsLookingLeft { get; set; }
        
        public int XPosition { get; set; }
        public int YPosition { get; set; }
    }
}