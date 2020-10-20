using System;
using System.Resources;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core
{
    public static class ResourcesUriHandler
    {
        private static readonly ResourceReader spritesReader;

        private static readonly ResourceReader soundsReader;


        static ResourcesUriHandler()
        {
            spritesReader = new ResourceReader(@"..\..\Resources\SpritesList.resources");

            soundsReader = new ResourceReader(@"..\..\Resources\SoundsList.resources");
        }

        public static Uri GetPowerUpSound(PowerUps powerUpType)
        {
            switch (powerUpType)
            {
                case PowerUps.Speed_Boost:
                    soundsReader.GetResourceData("SpeedBoost", out _, out byte[] speedBoostData);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(speedBoostData).Substring(1));
                case PowerUps.Invisibility:
                    break;
                case PowerUps.Invulnerability:
                    break;
                case PowerUps.Double_Jump:
                    break;
                case PowerUps.Rocket:
                    break;
                case PowerUps.Proximity_Mine:
                    break;
                case PowerUps.Saw:
                    break;
                case PowerUps.Knockback_Bomb:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(powerUpType), powerUpType, null);
            }

            return null;
        }

        public static Uri GetPlatformUri()
        {
            spritesReader.GetResourceData("platform", out _, out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                           System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }

        public static Uri GetRunningAnimationUri()
        {
            spritesReader.GetResourceData("run", out _, out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                           System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }

        public static Uri GetIdleAnimationUri()
        {
            spritesReader.GetResourceData("idle", out _, out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                           System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }

        public static Uri GetLandingAnimationUri()
        {
            spritesReader.GetResourceData("landing", out _, out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                           System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }

        public static Uri GetJumpingAnimationUri()
        {
            spritesReader.GetResourceData("jump", out _, out byte[] data);

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

            spritesReader.GetResourceData(backgroundType, out _, out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                           System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }

        public static Uri GetPlayerPowerUp(VisualElementType visual)
        {
            byte[] data;
            switch (visual)
            {
                case VisualElementType.DoubleJump:
                    spritesReader.GetResourceData("PlayerDoubleJumpVisual", out _, out data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));

                case VisualElementType.Invulnerability:
                    spritesReader.GetResourceData("PlayerInvulnerabilityVisual", out _, out data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                case VisualElementType.SpeedBoost:
                    spritesReader.GetResourceData("PlayerSpeedBoostVisual", out _, out data);

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
                    spritesReader.GetResourceData("SpeedBoost", out _, out data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                case PowerUps.Invisibility:
                    spritesReader.GetResourceData("Invisibility", out _, out data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                case PowerUps.Invulnerability:
                    spritesReader.GetResourceData("Invulnerability", out _, out data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));

                case PowerUps.Double_Jump:
                    spritesReader.GetResourceData("DoubleJump", out _, out data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                case PowerUps.Rocket:
                    spritesReader.GetResourceData("Rocket", out _, out data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                case PowerUps.Proximity_Mine:
                    spritesReader.GetResourceData("ProximityMine", out _, out data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                case PowerUps.Saw:
                    spritesReader.GetResourceData("Saw", out _, out data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                case PowerUps.Knockback_Bomb:
                    spritesReader.GetResourceData("KnockBackBomb", out _, out data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                default:
                    throw new ArgumentOutOfRangeException(nameof(powerUp), powerUp, null);
            }
        }
    }
}