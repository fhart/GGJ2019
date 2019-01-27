using UnityEngine;
using System.Collections.Generic;

enum Direction
{
    Left,
    Right,
    Up,
    Down,
    Invalid
}

public class Grid
{
    // true if can walk on the tile (x,y)
    public MapObject[,] tiles; // [y, x]

    public bool CanWalkOn(int x, int y)
    {
        if (y < 0 || y >= tiles.GetLength(0) ||
            x < 0 || x >= tiles.GetLength(1))
        {
            return false;
        }
        else
        {
            return tiles[y, x] == null;
        }
    }

    public Vector2 GetPositionToGo(Vector2 pos)
    {
        var x = (int)pos.x;
        var y = (int)pos.y;
        return tiles[y, x] != null ? tiles[y, x].GetPositionToGo(pos) : pos;
    }
}

class Step
{
    public Direction direction;
    public int stepCount;
    public Step prev;
}

class PathFinder
{
    // Llamar esto para obeter en path (direcciones) desde (startX, startY) a (endX, endY)
    public static List<Direction> GetFasterRoute(Grid grid, int startX, int startY, int endX, int endY)
    {
        // error si empieza o termina fuera del mapa o en un lugar inaccesible
        if (!grid.CanWalkOn(endX, endY))
        {
            return null;
        }

        Step[,] steps = new Step[grid.tiles.GetLength(0), grid.tiles.GetLength(1)];

        steps[startY, startX] = new Step()
        {
            direction = Direction.Left, // bucause it can't be null
            stepCount = 0,
            prev = null,
        };

        // start calculation
        ContinueCheckingSteps(grid, startX, startY, steps);

        // check results
        Step end = steps[endY, endX];
        if (end == null)
        {
            return null;
        }
        else
        {
            List<Direction> result = new List<Direction>();
            // recorrer hasta llegar al inicio
            while (end.prev != null)
            {
                result.Add(end.direction);
                end = end.prev;
            }
            result.Reverse();

            result.Add(Direction.Invalid);
            return result;
        }
    }

    private static void ContinueCheckingSteps(Grid grid, int x, int y, Step[,] steps)
    {
        Step prev = steps[y, x];

        // izquierda
        ContinueCheckingStepsOnDirection(grid, x - 1, y, steps, prev, Direction.Left);

        // derecha
        ContinueCheckingStepsOnDirection(grid, x + 1, y, steps, prev, Direction.Right);

        // abajo
        ContinueCheckingStepsOnDirection(grid, x, y - 1, steps, prev, Direction.Down);

        // arriba
        ContinueCheckingStepsOnDirection(grid, x, y + 1, steps, prev, Direction.Up);
    }

    private static void ContinueCheckingStepsOnDirection(Grid grid, int x, int y, Step[,] steps, Step prev, Direction direction)
    {
        if (grid.CanWalkOn(x, y))
        {
            Step alreadyWalked = steps[y, x];
            if (alreadyWalked == null || alreadyWalked.stepCount > prev.stepCount + 1)
            {
                steps[y, x] = new Step()
                {
                    direction = direction,
                    stepCount = prev.stepCount + 1,
                    prev = prev,
                };

                ContinueCheckingSteps(grid, x, y, steps);
            }
        }
    }
}
