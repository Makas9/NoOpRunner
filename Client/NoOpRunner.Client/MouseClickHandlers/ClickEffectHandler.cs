using System;
using System.Windows.Controls;
using System.Windows.Media;
using NoOpRunner.Core;

namespace NoOpRunner.Client.MouseClickHandlers
{
    public class ClickEffectHandler : MouseClickHandler
    {
        private MediaPlayer mediaPlayer;
        
        public ClickEffectHandler(Core.NoOpRunner game) : base(game)
        {
        }

        protected override void HandleMouseClick(int positionX, int positionY)
        {
            if (mediaPlayer== null)
            {
                mediaPlayer = new MediaPlayer();
                
                mediaPlayer.Open(ResourcesUriHandler.GetP2ClickSound());

                mediaPlayer.Volume = 1;
                
                mediaPlayer.MediaEnded += (o, a) =>
                {
                    mediaPlayer.Position = TimeSpan.Zero;
                    mediaPlayer.Stop();
                };
            }
            
            mediaPlayer.Play();
        }
    }
}