using System;
using System.Windows.Controls;
using System.Windows.Media;
using NoOpRunner.Client.Logic.ViewModels;
using NoOpRunner.Core;

namespace NoOpRunner.Client.MouseClickHandlers
{
    public class PlayerHandler : MouseClickHandler
    {
        private MediaPlayer mediaPlayer;
        
        public PlayerHandler(Core.NoOpRunner game, int volume) : base(game)
        {
            mediaPlayer = new MediaPlayer();
                    
            mediaPlayer.Open(ResourcesUriHandler.GetCharacterClickSound());

            mediaPlayer.MediaEnded += (o, a) =>
            {
                mediaPlayer.Position = TimeSpan.Zero;
                mediaPlayer.Stop();
            };
                
            mediaPlayer.Volume = ((double)volume)/100;
        }

        protected override void HandleMouseClick(int positionX, int positionY)
        {
            Logging.Instance.Write("Chain of responsibility: PlayerHandler", LoggingLevel.ChainOfResponsibility);
            
            if (Game.Player.CenterPosX != positionX || Game.Player.CenterPosY != positionY)
                
                return;

            mediaPlayer.Play();
        }
    }
}