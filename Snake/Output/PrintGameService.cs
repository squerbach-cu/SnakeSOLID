using System;
using System.Collections.Generic;
using SnakeSOLID.GameHandling.Interfaces;

namespace SnakeSOLID.Snake.Output;

public class PrintGameService : IPrintGameService
{
    public void PrintGame(GameState gameState)
    {
        if (!gameState.IsBoardPrinted)
        {
            PrintBoard(gameState.Width, gameState.Height);
            PrintSnakeInit(gameState.Snake);
            PrintApple(gameState.Apple);
            gameState.IsBoardPrinted = true;
        }

        if (gameState.HasSnakeChanged)
        {
            PrintSnakeMovement(gameState.Snake.First.Value, gameState.RemovedEnd);
            gameState.HasSnakeChanged = false;
        }

        if (gameState.HasScoreChanged)
        {
            UpdateScore(gameState.Score, gameState.Height);
            PrintApple(gameState.Apple);
            gameState.HasScoreChanged = false;
        }

        if (gameState.HasEnded)
        {
            ColorHitLocation(gameState.HitCoordinates);
            PrintGameOver(gameState.Width, gameState.Height);
        }

        if (gameState.AskForRestart) PrintRestart(gameState.Width, gameState.Height);
    }

    private void PrintGameOver(int gameWidth, int gameHeight)
    {
        const string gameOver = "Game Over!";
        Console.SetCursorPosition((gameWidth / 2) - (gameOver.Length / 2), gameHeight / 2);
        Console.WriteLine(gameOver);
    }

    private void PrintRestart(int gameWidth, int gameHeight)
    {
        const string restart = "Restart? (y/n)";
        Console.SetCursorPosition((gameWidth / 2) - (restart.Length / 2), (gameHeight / 2) + 1);
        Console.WriteLine(restart); 
    }

    private void PrintBoard(int width, int height)
    {
        Console.ForegroundColor = ConsoleColor.White;
        
        for (int i = 1; i < width; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.WriteLine("═");
        }
        for (int i = 1; i < width; i++)
        {
            Console.SetCursorPosition(i, height);
            Console.WriteLine("═");
        }
        for (int i = 1; i < height +2; i++)
        {
            Console.SetCursorPosition(width, i);
            Console.WriteLine("║");
        }
        for (int i = 1; i < height +2; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.WriteLine("║");
        }

        Console.SetCursorPosition(0, 0);
        Console.WriteLine("╔");
        Console.SetCursorPosition(width, 0);
        Console.WriteLine("╗");
        Console.SetCursorPosition(0, height);
        Console.WriteLine("╟");
        Console.SetCursorPosition(width, height);
        Console.WriteLine("╢");

        //Score Box
        Console.SetCursorPosition(0, height + 2);
        Console.WriteLine("╚");
        Console.SetCursorPosition(width, height + 2);
        Console.WriteLine("╝");
        for (int i = 1; i < width; i++)
        {
            Console.SetCursorPosition(i, height + 2);
            Console.WriteLine("═");
        }
        Console.SetCursorPosition(2, height + 1);
        Console.WriteLine("Score: 0");
    }
    
    private void UpdateScore(int score, int height)
    {
        Console.SetCursorPosition(2, height + 1);
        Console.WriteLine("Score: {0}", score);
    } 

    private void PrintSnakeMovement(Coordinates newFirst, Coordinates oldLast)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(newFirst.X, newFirst.X);
        Console.Write("■");
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(oldLast.X, oldLast.Y);
        Console.Write(" ");
    }

    private void PrintSnakeInit(LinkedList<Coordinates> snake)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (Coordinates item in snake)
        {
            Console.SetCursorPosition(item.X, item.Y);
            Console.Write("■");
        }
        Console.ForegroundColor = ConsoleColor.White;
    }

    private void ColorHitLocation(Coordinates coordinates)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(coordinates.X, coordinates.Y);
        Console.Write("■");
        Console.ForegroundColor = ConsoleColor.White;
    }

    private void PrintApple(Coordinates appleCoordinates)
    {
        Console.SetCursorPosition(appleCoordinates.X, appleCoordinates.Y);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("■");
        Console.ForegroundColor = ConsoleColor.White; 
    }
}