﻿using System;
using System.Collections.Generic;
using System.IO;

namespace NoOpRunner.Core
{
    public static class SpritesUriHandler
    {
        private static string spritesDirectory = AppDomain.CurrentDomain.BaseDirectory + @"..\..\Sprites";
        private static string spritesList = "SpritesList.txt";

        private static Dictionary<string, Uri> sprites;

        public static void LoadSprites()
        {
            sprites = new Dictionary<string, Uri>();

            var spritesLines = File.ReadAllLines(spritesDirectory + "\\" + spritesList);

            foreach (var spriteLine in spritesLines)
            {
                sprites.Add(spriteLine.Split(':')[0], new Uri(spritesDirectory + spriteLine.Split(':')[1]));
            }
        }

        public static Uri GetPlatformUri()
        {
            return sprites["platform"];
        }

        public static Uri GetRunningAnimationUri()
        {
            return sprites["run"];
        }

        public static Uri GetIdleAnimationUri()
        {
            return sprites["idle"];
        }

        public static Uri GetLandingAnimationUri()
        {
            return sprites["landing"];
        }

        public static Uri GetJumpingAnimationUri()
        {
            return sprites["jump"];
        }

        public static Uri GetBackground()
        {
            return sprites["background"];
        }
    }
}