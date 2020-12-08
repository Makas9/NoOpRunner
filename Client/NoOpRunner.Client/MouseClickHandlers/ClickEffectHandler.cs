using System;
using System.Windows.Controls;
using System.Windows.Media;
using NoOpRunner.Client.Logic.ViewModels;
using NoOpRunner.Client.Views;
using NoOpRunner.Core;

namespace NoOpRunner.Client.MouseClickHandlers
{
    public class ClickEffectHandler : MouseClickHandler
    {
        private MediaPlayer mediaPlayer;
        
        public ClickEffectHandler(Core.NoOpRunner game, int volume) : base(game)
        {
            
            mediaPlayer = new MediaPlayer();
            
            mediaPlayer.Open(ResourcesUriHandler.GetP2ClickSound());

            mediaPlayer.Volume = ((double)volume)/100;
            
            mediaPlayer.MediaEnded += (o, a) =>
            {
                mediaPlayer.Position = TimeSpan.Zero;
                mediaPlayer.Stop();
            };
        }

        protected override void HandleMouseClick(int positionX, int positionY)
        {
            Logging.Instance.Write("Chain of responsibility: ClickEffectHandler", LoggingLevel.ChainOfResponsibility);
            
            mediaPlayer.Play();
        }
    }
}