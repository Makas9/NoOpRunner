using System;
using System.Windows.Controls;
using System.Windows.Media;
using NoOpRunner.Core;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Client.MouseClickHandlers
{
    public class StaticShapeHandler : MouseClickHandler
    {
        private MediaPlayer mediaPlayer;
        
        public StaticShapeHandler(Core.NoOpRunner game, int volume) : base(game)
        {
            mediaPlayer = new MediaPlayer();
                    
            mediaPlayer.Open(ResourcesUriHandler.GetPowerUp(PowerUps.Saw));

            mediaPlayer.MediaEnded += (o, a) =>
            {
                mediaPlayer.Position = TimeSpan.Zero;
                mediaPlayer.Stop();
            };
            
            mediaPlayer.Volume = volume;
        }

        protected override void HandleMouseClick(int positionX, int positionY)
        {
            if (Game.PlatformsContainer.GetShapes(true)[positionX, positionY] == default) 
                
                return;

            mediaPlayer.Play();
        }
    }
}