using SnakeSOLID.GameHandling.Interfaces;

namespace SnakeSOLID.Snake;

public class Coordinates : ICoordinates
{
    public Coordinates(int xCoordinate, int yCoordinate)
    {
        X = xCoordinate;
        Y = yCoordinate;
    }
    public int X { get; set; }
    public int Y { get; set; }
}