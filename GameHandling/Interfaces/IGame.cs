using System;
using SnakeSOLID.Snake;

namespace SnakeSOLID.GameHandling.Interfaces;

public interface IGame
{
    void PrintGame();
    bool HasEnded();
    void HandleInput(ConsoleKeyInfo consoleKeyInfo);
    void HandelLoop();
    GameState GameStatus { get; set; }
}