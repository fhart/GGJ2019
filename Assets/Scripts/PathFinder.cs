using UnityEngine;
using System;
using System.Collections.Generic;

enum Direction
{
    Left,
    Right,
    Up,
    Down,
}

class Grid
{
    // true if can walk on the tile (x,y)
    public bool[,] tiles; // [y, x]

    public bool canWalkOn(int x, int y)
    {
        if (y < 0 || y >= tiles.GetLength(0) ||
            x < 0 || x >= tiles.GetLength(1))
        {
            return false;
        }
        else
        {
            return tiles[y, x];
        }
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
    public static List<Direction> getFasterRoute(Grid grid, int startX, int startY, int endX, int endY)
    {
        // error si empieza o termina fuera del mapa o en un lugar inaccesible
        if (!grid.canWalkOn(startX, startY) || !grid.canWalkOn(endX, endY)) {
            return null;
        }

        Step[,] steps = new Step[grid.tiles.GetLength(0), grid.tiles.GetLength(1)];
        
        steps[startY, startX] = new Step() {
            direction = Direction.Left, // bucause it can't be null
            stepCount = 0,
            prev = null,
        };

        // start calculation
        continueCheckingSteps(grid, startX, startY, steps);

        // check results
        Step end = steps[endY, endX];
        if (end == null) {
            return null;
        } else {
            List<Direction> result = new List<Direction>();
            // recorrer hasta llegar al inicio
            while (end.prev != null) {
                result.Add(end.direction);
                end = end.prev;
            }
            result.Reverse();
            return result;
        }
    }

    private static void continueCheckingSteps(Grid grid, int x, int y, Step[,] steps) {
        Step prev = steps[y, x];

        // izquierda
        continueCheckingStepsOnDirection(grid, x - 1, y, steps, prev, Direction.Left);
        
        // derecha
        continueCheckingStepsOnDirection(grid, x + 1, y, steps, prev, Direction.Right);
        
        // arriba
        continueCheckingStepsOnDirection(grid, x, y - 1, steps, prev, Direction.Up);
        
        // abajo
        continueCheckingStepsOnDirection(grid, x, y + 1, steps, prev, Direction.Down);
    }

    private static void continueCheckingStepsOnDirection(Grid grid, int x, int y, Step[,] steps, Step prev, Direction direction) {
        if (grid.canWalkOn(x, y)) {
            Step alreadyWalked = steps[y, x];
            if (alreadyWalked == null || alreadyWalked.stepCount > prev.stepCount + 1) {
                steps[y, x] = new Step() {
                    direction = direction,
                    stepCount = prev.stepCount + 1,
                    prev = prev,
                };

                continueCheckingSteps(grid, x, y, steps);
            }
        }
    }

    public static void test() {
        Grid grid = new Grid {
            tiles = new bool[5, 4] {
                { true, true, true, true },
                { false, true, true, false },
                { false, true, true, true },
                { true, false, true, false },
                { true, true, true, true },
            }
        };

        List<Direction> result = PathFinder.getFasterRoute(grid, 0, 0, 0, 4);
        Debug.Log(result != null);
        if (result != null) {
            Debug.Log(result.Count);
            foreach (var r in result) {
                Debug.Log(r.ToString());
            }
        }
    }
}
