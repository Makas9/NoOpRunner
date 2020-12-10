using System;

namespace NoOpRunner.Core.Exceptions
{
    public class GameOverException : Exception
    {
        public bool PlayerOneWon { get; set; }

        public GameOverException(bool playerOneWon)
        {
            PlayerOneWon = playerOneWon;
        }
    }
}
