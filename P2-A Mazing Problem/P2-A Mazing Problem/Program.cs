using System;
using System.Collections.Generic;

namespace RatInAMaze
{
    class Program
    {
        static void Main(string[] args)
        {

            int[,] maze = new int[,]
            {
                { 1, 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 0, 1 },
                { 1, 0, 1, 1, 0, 1 },
                { 1, 0, 0, 1, 0, 1 },
                { 1, 1, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 1, 1 }
            };

            int entryX = 1, entryY = 1;
            int exitX = 4, exitY = 4;

            Console.WriteLine("Maze Layout:");
            PrintMaze(maze, exitX, exitY);

            // Running the maze solving algorithm
            bool pathFound = SolveMaze(maze, entryX, entryY, exitX, exitY);

            if (pathFound)
            {
                Console.WriteLine("\nA path to the exit has been found!");
            }
            else
            {
                Console.WriteLine("\nNo path to the exit found.");
            }
        }

        // Directions: Northwest, North, Northeast, West, East, Southwest, South, Southeast
        static readonly int[] dx = { -1, 0, 1, -1, 1, -1, 0, 1 };
        static readonly int[] dy = { -1, -1, -1, 0, 0, 1, 1, 1 };

        // Solving the maze using Depth-First Search (DFS)
        static bool SolveMaze(int[,] maze, int startX, int startY, int exitX, int exitY)
        {
            int rows = maze.GetLength(0);
            int cols = maze.GetLength(1);

            bool[,] visited = new bool[rows, cols];
            Stack<(int, int)> pathStack = new Stack<(int, int)>();
            pathStack.Push((startX, startY));

            while (pathStack.Count > 0)
            {
                var (x, y) = pathStack.Pop();

                if (x == exitX && y == exitY)
                {
                    Console.WriteLine("Path to the exit:");
                    while (pathStack.Count > 0)
                    {
                        var (px, py) = pathStack.Pop();
                        Console.WriteLine($"({px},{py})");
                    }
                    Console.WriteLine($"({x},{y})");
                    return true;
                }
                // Exploring 8 possible directions
                for (int i = 0; i < 8; i++)
                {
                    int newX = x + dx[i];
                    int newY = y + dy[i];

                    if (IsValidMove(maze, newX, newY, visited))
                    {
                        visited[newX, newY] = true;
                        pathStack.Push((newX, newY));
                    }
                }
            }

            return false;
        }

        // Checking if the move is valid (within bounds, not a wall, and not visited)
        static bool IsValidMove(int[,] maze, int x, int y, bool[,] visited)
        {
            int rows = maze.GetLength(0);
            int cols = maze.GetLength(1);

            return x >= 0 && x < rows && y >= 0 && y < cols && maze[x, y] == 0 && !visited[x, y];
        }

        static void PrintMaze(int[,] maze, int exitX, int exitY)
        {
            int rows = maze.GetLength(0);
            int cols = maze.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (i == exitX && j == exitY)
                    {
                        Console.Write("E ");
                    }
                    else
                    {
                        Console.Write(maze[i, j] == 1 ? "1 " : "0 ");  // '1' for walls, '0' for paths
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
