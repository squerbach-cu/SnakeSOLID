using System;
using SnakeSOLID.GameHandling.Interfaces;
using SnakeSOLID.Snake;

namespace SnakeSOLID.GameHandling
{
    internal class GameEngine : IGameEngine
    {
        /// <summary>
        /// "Starts the Engine" by creating a new IGame instance. If there would be more games they would have to be selected here.
        /// </summary>
        public void StartEngine()
        {
            IGame game = new PlaySnakeService();
            RunLoop(game);
        }
        /// <summary>
        /// This loop will run the IGame game by continuously asking for input and handling it according to the tim
        /// </summary>
        /// <param name="game"></param>
        private void RunLoop(IGame game)
        {
            while (true)
            {
                game.PrintGame();

                if (Console.KeyAvailable)
                {
                    game.HandleInput(Console.ReadKey(true));
                }
                
                game.HandelLoop();

                if (game.HasEnded())
                {
                    game.PrintGame();
                    break;
                } 
            }
        }
    }
}
