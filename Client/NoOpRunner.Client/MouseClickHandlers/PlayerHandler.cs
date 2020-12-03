using System;
using System.Windows.Controls;
using System.Windows.Media;
using NoOpRunner.Core;

namespace NoOpRunner.Client.MouseClickHandlers
{
    public class PlayerHandler : MouseClickHandler
    {
        private MediaPlayer mediaPlayer;
        
        public PlayerHandler(Core.NoOpRunner game) : base(game)
        {
        }

        protected override void HandleMouseClick(int positionX, int positionY)
        {
            if (Game.Player.CenterPosX != positionX || Game.Player.CenterPosY != positionY)
                
                return;
            
            if (mediaPlayer == null)
            {
                mediaPlayer = new MediaPlayer();
                    
                mediaPlayer.Open(ResourcesUriHandler.GetCharacterClickSound());

                mediaPlayer.MediaEnded += (o, a) =>
                {
                    mediaPlayer.Position = TimeSpan.Zero;
                    mediaPlayer.Stop();
                };
                
                mediaPlayer.Volume = 0.5;
            }
                
            mediaPlayer.Play();
        }
    }
}