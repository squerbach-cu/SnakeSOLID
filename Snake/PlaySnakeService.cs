using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using SnakeSOLID.GameHandling.Interfaces;
using SnakeSOLID.Snake.Output;

namespace SnakeSOLID.Snake
{
    internal class PlaySnakeService : IGame
    {
        public GameState GameStatus { get; set; }
        private Random _random;
        private Stopwatch _stopwatch;
        public PlaySnakeService()
        {
            GameStatus = new GameState
            {
                GamePrinter = new PrintGameService()
            };
            _random = new Random();
            InitSnake();
            CreateApple();
            _stopwatch = Stopwatch.StartNew();
        }
        public void HandelLoop()
        {
            if (_stopwatch.ElapsedMilliseconds >= GameStatus.GameSpeed)
            {
                _stopwatch.Restart();
                MoveSnake();
            }
        }
        
        public void PrintGame()
        {
            GameStatus.GamePrinter.PrintGame(GameStatus);
        }

        public void HandleInput(ConsoleKeyInfo consoleKeyInfo)
        {
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if (GameStatus.MoveDirection != Direction.Down)
                    {
                        GameStatus.MoveDirection = Direction.Up;
                    }                                        
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    if (GameStatus.MoveDirection != Direction.Up)
                    {
                        GameStatus.MoveDirection = Direction.Down;
                    }                    
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    if (GameStatus.MoveDirection != Direction.Right)
                    {
                        GameStatus.MoveDirection = Direction.Left;
                    }
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    if (GameStatus.MoveDirection != Direction.Left)
                    {
                        GameStatus.MoveDirection = Direction.Right;
                    }
                    break;
            }
        }

        public bool HasEnded()
        {
            //Check if snake hit itself
            foreach (var s in GameStatus.Snake.Skip(1))
            {
                if (s.X == GameStatus.Snake.First.Value.X && s.Y == GameStatus.Snake.First.Value.Y)
                {
                    GameStatus.HitCoordinates =
                        new Coordinates(GameStatus.Snake.First.Value.X, GameStatus.Snake.First.Value.Y);
                    return true;
                }
            }
            //Check if snake hit border
            if (GameStatus.Snake.First.Value.X == 0 || GameStatus.Snake.First.Value.X == GameStatus.Width|| GameStatus.Snake.First.Value.Y == 0 || GameStatus.Snake.First.Value.Y == GameStatus.Height)
            {
                GameStatus.HitCoordinates =
                    new Coordinates(GameStatus.Snake.First.Value.X, GameStatus.Snake.First.Value.Y);
                return true;
            }
            return false; 
        }

        private void InitSnake()
        {
            GameStatus.Snake = new LinkedList<Coordinates>();
            
            GameStatus.InitialSnakeLength = 4;

            for (int i = 0; i < GameStatus.InitialSnakeLength; i++)
            {
                Coordinates coordinatesNode = new Coordinates(GameStatus.Width / 2, GameStatus.Height / 2 + i);
                LinkedListNode<Coordinates> lln = new LinkedListNode<Coordinates>(coordinatesNode);
                GameStatus.Snake.AddLast(lln);
            }
        }

        /// <summary>
        /// Extends the Snake LL by one. If the methode was called with a false parameter the last ll node is removed other wise nothing happens
        ///
        /// Macht noch zu viel
        /// </summary>
        public void MoveSnake()
        {
            Coordinates firstCoordinates = GameStatus.Snake.First.Value;

            switch (GameStatus.MoveDirection)
            {
                case Direction.Up:
                    Coordinates coordinatesNode = new Coordinates(firstCoordinates.X, firstCoordinates.Y - 1);
                    LinkedListNode<Coordinates> up = new LinkedListNode<Coordinates>(coordinatesNode);
                    GameStatus.Snake.AddFirst(up);
                    break;
                case Direction.Down:
                    coordinatesNode = new Coordinates(firstCoordinates.X, firstCoordinates.Y + 1);
                    LinkedListNode<Coordinates> down = new LinkedListNode<Coordinates>(coordinatesNode);
                    GameStatus.Snake.AddFirst(down);
                    break;
                case Direction.Left:
                    coordinatesNode = new Coordinates(firstCoordinates.X - 1, firstCoordinates.Y);
                    LinkedListNode<Coordinates> left = new LinkedListNode<Coordinates>(coordinatesNode);
                    GameStatus.Snake.AddFirst(left); 
                    break;
                case Direction.Right:
                    coordinatesNode = new Coordinates(firstCoordinates.X + 1, firstCoordinates.Y);
                    LinkedListNode<Coordinates> right = new LinkedListNode<Coordinates>(coordinatesNode);
                    GameStatus.Snake.AddFirst(right);
                    break;
            }

            if (HitApple())
            {
                GameStatus.Score++;
                if (GameStatus.GameSpeed >= 100) GameStatus.GameSpeed -= 10;
                CreateApple();
                return;
            }

            GameStatus.RemovedEnd = GameStatus.Snake.Last.Value;
            GameStatus.Snake.RemoveLast();
            GameStatus.HasSnakeChanged = true;
        }

        private bool HitApple()
        {
            return GameStatus.Snake.First.Value.X == GameStatus.Apple.X && GameStatus.Snake.First.Value.Y == GameStatus.Apple.Y;
        }

        
        private void CreateApple()
        {
            int AppleX = _random.Next(1, GameStatus.Width);
            int AppleY = _random.Next(1, GameStatus.Height);

            foreach (var s in GameStatus.Snake)
            {
                if (s.X == AppleX && s.Y == AppleY)
                {
                    CreateApple();
                    return;
                }
            }
            
            if (GameStatus.Apple == null)
            {
                GameStatus.Apple = new Coordinates(AppleX,AppleY);
                return;
            }
            
            GameStatus.Apple.X = AppleX;
            GameStatus.Apple.Y = AppleY;
        }
    }
}
