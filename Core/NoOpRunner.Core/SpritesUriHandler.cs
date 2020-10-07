using System;
using System.Resources;

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
    }
}