using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Dtos
{
    public class PowerUpUseDto : CoordinatesDto
    {
        public PowerUps Type { get; set; }
    }
}
