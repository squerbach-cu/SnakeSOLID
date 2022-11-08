using System;
using SnakeSOLID.GameHandling;
using SnakeSOLID.GameHandling.Interfaces;

namespace SnakeSOLID
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IGameEngine engine = new GameEngine();
            engine.StartEngine();
        }
    }
}
