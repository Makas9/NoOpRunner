using System;
using System.Resources;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core
{
    public static class SpritesUriHandler
    {
        private static readonly ResourceReader resourceReader;

        static SpritesUriHandler()
        {
            resourceReader = new ResourceReader(@"..\..\Resources\SpritesList.resources");
        }

        public static Uri GetPlatformUri()
        {
            resourceReader.GetResourceData("platform", out _, out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                           System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }

        public static Uri GetRunningAnimationUri()
        {
            resourceReader.GetResourceData("run", out _, out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                           System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }

        public static Uri GetIdleAnimationUri()
        {
            resourceReader.GetResourceData("idle", out _, out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                           System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }

        public static Uri GetLandingAnimationUri()
        {
            resourceReader.GetResourceData("landing", out _, out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                           System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }

        public static Uri GetJumpingAnimationUri()
        {
            resourceReader.GetResourceData("jump", out _, out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                           System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }

        public static Uri GetBackground(int backgroundNumber = 5)
        {
            string backgroundType = "background";

            if (backgroundNumber < 1 || backgroundNumber > 5)
            {
                throw new ArgumentException("1-5 (inclusive)");
            }

            backgroundType += backgroundNumber;
            
            resourceReader.GetResourceData(backgroundType, out _, out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                           System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }

        public static Uri GetPlayerPowerUp(VisualElementType visual)
        {
            byte[] data;
            switch (visual)
            {
                case VisualElementType.DoubleJump:
                    resourceReader.GetResourceData("PlayerDoubleJumpVisual", out _, out data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                    
                case VisualElementType.Invulnerability:
                    resourceReader.GetResourceData("PlayerInvulnerabilityVisual", out _, out data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                case VisualElementType.Socks:
                    resourceReader.GetResourceData("PlayerSocksVisual", out _, out data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                default:
                    throw new ArgumentOutOfRangeException(nameof(visual), visual, null);
            }
        }

        public static Uri GetPowerUp(PowerUps powerUp)
        {
            byte[] data;
            switch (powerUp)
            {
                case PowerUps.Speed_Boost:
                    resourceReader.GetResourceData("SpeedBoost", out _, out data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                case PowerUps.Invisibility:
                    resourceReader.GetResourceData("Invisibility", out _, out  data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                case PowerUps.Invulnerability:
                    resourceReader.GetResourceData("Invulnerability", out _, out  data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                    
                case PowerUps.Double_Jump:
                    resourceReader.GetResourceData("DoubleJump", out _, out  data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                case PowerUps.Rocket:
                    resourceReader.GetResourceData("Rocket", out _, out  data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                case PowerUps.Proximity_Mine:
                    resourceReader.GetResourceData("ProximityMine", out _, out  data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                case PowerUps.Saw:
                    resourceReader.GetResourceData("Saw", out _, out  data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                case PowerUps.Knockback_Bomb:
                    resourceReader.GetResourceData("KnockBackBomb", out _, out  data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                default:
                    throw new ArgumentOutOfRangeException(nameof(powerUp), powerUp, null);
            }
        }
    }
}