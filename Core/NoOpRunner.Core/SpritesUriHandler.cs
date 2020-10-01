using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Automation;

namespace NoOpRunner.Core
{
    public static class SpritesUriHandler
    {
        private static readonly ResourceReader resourceReader;

        /// <summary>
        /// Static but still bad
        /// </summary>
        static SpritesUriHandler()
        {
            resourceReader = new System.Resources.ResourceReader(@"..\..\Resources\SpritesList.resources");
        }

        public static Uri GetPlatformUri()
        {
            resourceReader.GetResourceData("platform", out string dataType,  out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory+@"..\.."+System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }

        public static Uri GetRunningAnimationUri()
        {
            resourceReader.GetResourceData("run", out string dataType,  out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory+@"..\.."+System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }

        public static Uri GetIdleAnimationUri()
        {
            resourceReader.GetResourceData("idle", out string dataType,  out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory+@"..\.."+System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }

        public static Uri GetLandingAnimationUri()
        {
            resourceReader.GetResourceData("landing", out string dataType,  out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory+@"..\.."+System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }

        public static Uri GetJumpingAnimationUri()
        {
            resourceReader.GetResourceData("jump", out string dataType,  out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory+@"..\.."+System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }

        public static Uri GetBackground()
        {
            resourceReader.GetResourceData("background", out string dataType,  out byte[] data);

            return new Uri(AppDomain.CurrentDomain.BaseDirectory+@"..\.."+System.Text.Encoding.UTF8.GetString(data).Substring(1));
        }
    }
}
