using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NoOpRunner.Core.Shapes
{
    public class PowerUp : BaseShape
    {
        // Player 1 PowerUp | Player 2 PowerUp
        Dictionary<string, string> powerUps = new Dictionary<string, string>(){
            { "speed_boost", "rocket" },
            { "invisibility", "proximity_mine" },
            { "invulnerablity", "saw" },
            { "double_jump", "knockback_bomb" }
        };

        public PowerUp(int centerPosX, int centerPosY, int[] platformXCoords, int[] platformYCoords) : base(centerPosX, centerPosY)
        {
            int randomLocation = RandLocation(platformXCoords, platformYCoords);

            SpawnPowerUp(platformXCoords[randomLocation], platformYCoords[randomLocation] + 2);
        }

        public void SpawnPowerUp(int x, int y) // TODO (Image instead of color)
        {
            Color powerup = RandomPowerUp();

            MapShapeX(x, y, 1, powerup);
        }

        public Color RandomPowerUp()
        {
            int powerUp = RandomNumber.Instance.GetRandom(0, powerUps.Count);
            String key = powerUps.Keys.ElementAt(powerUp);

            switch (key)
            {
                case "speed_boost": return Color.Red;
                case "invisibility": return Color.Blue;
                case "invulnerablity": return Color.Green;
                case "double_jump": return Color.Yellow;
            }

            return Color.Black;
        }

        public int RandLocation(int[] platformXCoords, int[] platformYCoords)
        {
            int found = -1, x = -1;
            while (found == -1)
            {
                x = RandomNumber.Instance.GetRandom(2, platformXCoords.Length - 2);
                if (platformYCoords[x - 2] == platformYCoords[x] &&
                    platformYCoords[x - 1] == platformYCoords[x] &&
                    platformYCoords[x + 1] == platformYCoords[x] &&
                    platformYCoords[x + 2] == platformYCoords[x])
                {
                    found = x; // Spawn power up between flat platform
                }
            }
            return x;
        }
    }
}
