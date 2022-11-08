using System.Collections.Generic;
using SnakeSOLID.GameHandling.Interfaces;

namespace SnakeSOLID.Snake
{
    public class GameState : IGameState
    {
        private int _score;
        private LinkedList<Coordinates> _snake;

        public int Height { get; set; } = 25;
        public int Width { get; set; } = 25;
        public bool IsBoardPrinted { get; set; }
        public bool HasSnakeChanged { get; set; }
        public bool HasScoreChanged { get; set; }
        public bool HasEnded { get; set; }
        public bool AskForRestart { get; set; }
        public int InitialSnakeLength { get; set; }
        public Coordinates RemovedEnd { get; set; }
        public Coordinates Apple { get; set; }
        public Coordinates HitCoordinates { get; set; }
        public int Score
        {
            get => _score;
            set
            {
                HasScoreChanged = true;
                _score = value;
            }
        }
        public int GameSpeed { get; set; } = 250;
        public LinkedList<Coordinates> Snake
        {
            get => _snake;
            set
            {
                //HasSnakeChanged = true;
                _snake = value;
            }
        }
        public Direction MoveDirection { get; set; } = Direction.Up;
        public IPrintGameService GamePrinter { get; set; }
        
    }
}
