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
            spritesReader = new ResourceReader(@"..\..\..\..\Client\NoOpRunner.Client\Resources\SpritesList.resources");

            soundsReader = new ResourceReader(@"..\..\..\..\Client\NoOpRunner.Client\Resources\SoundsList.resources");
        }

        public static Uri GetCharacterClickSound()
        {
            soundsReader.GetResourceData("ClickP2", out _, out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                           System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }
        
        public static Uri GetP2ClickSound()
        {
            soundsReader.GetResourceData("Click", out _, out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                           System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }
        
        public static Uri GetPowerUpSound(PowerUps powerUpType)
        {
            byte[] data;
            switch (powerUpType)
            {
                case PowerUps.Speed_Boost:
                    soundsReader.GetResourceData("SpeedBoost", out _, out data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
                case PowerUps.Invisibility:
                    break;
                case PowerUps.Health_Crystal:
                    break;
                case PowerUps.Double_Jump:
                    break;
                case PowerUps.Rocket:
                    break;
                case PowerUps.Proximity_Mine:
                    break;
                case PowerUps.Saw:
                    soundsReader.GetResourceData("Saw", out _, out data);

                    return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                                   System.Text.Encoding.UTF8.GetString(data).Substring(1));
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

            string resource;
            switch (visual)
            {
                case VisualElementType.DoubleJump:
                    resource = "PlayerDoubleJumpVisual";
                    break;

                case VisualElementType.Invulnerability:
                    resource = "PlayerInvulnerabilityVisual";
                    break;

                case VisualElementType.SpeedBoost:
                    resource = "PlayerSpeedBoostVisual";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(visual), visual, null);
            }

            spritesReader.GetResourceData(resource, out _, out data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                           System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }

        public static Uri GetPowerUp(PowerUps powerUp)
        {
            byte[] data;

            string resource;
            switch (powerUp)
            {
                case PowerUps.Speed_Boost:
                    resource = "SpeedBoost";
                    break;

                case PowerUps.Invisibility:
                    resource = "Invisibility";
                    break;

                case PowerUps.Health_Crystal:
                    resource = "Invulnerability";
                    break;

                case PowerUps.Double_Jump:
                    resource = "DoubleJump";
                    break;

                case PowerUps.Rocket:
                    resource = "Rocket";
                    break;

                case PowerUps.Proximity_Mine:
                    resource = "ProximityMine";
                    break;

                case PowerUps.Saw:
                    resource = "Saw";
                    break;

                case PowerUps.Knockback_Bomb:
                    resource = "KnockBackBomb";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(powerUp), powerUp, null);
            }

            spritesReader.GetResourceData(resource, out _, out data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\.." +
                           System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }
    }
}