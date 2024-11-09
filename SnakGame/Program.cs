using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static int width = 20;
    static int height = 10;
    static int score = 0;
    static bool gameOver = false;

    static void Main()
    {
        Console.CursorVisible = false;
        Console.SetWindowSize(width + 1, height + 1);

        var snake = new List<Point> { new Point(5, 5) };
        var food = GenerateFood(snake);

        var direction = Direction.Right;
        while (!gameOver)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                direction = key switch
                {
                    ConsoleKey.W when direction != Direction.Down => Direction.Up,
                    ConsoleKey.S when direction != Direction.Up => Direction.Down,
                    ConsoleKey.A when direction != Direction.Right => Direction.Left,
                    ConsoleKey.D when direction != Direction.Left => Direction.Right,
                    _ => direction
                };
            }

            MoveSnake(snake, direction);
            if (snake.First().Equals(food))
            {
                score++;
                snake.Add(new Point(-1, -1)); // Add a new segment
                food = GenerateFood(snake);
            }

            Draw(snake, food);
            Thread.Sleep(100);
        }

        Console.SetCursorPosition(0, height + 1);
        Console.WriteLine("Game Over! Your score: " + score);
    }

    static void MoveSnake(List<Point> snake, Direction direction)
    {
        var head = snake.First();
        Point newHead = head;

        switch (direction)
        {
            case Direction.Up: newHead = new Point(head.X, head.Y - 1); break;
            case Direction.Down: newHead = new Point(head.X, head.Y + 1); break;
            case Direction.Left: newHead = new Point(head.X - 1, head.Y); break;
            case Direction.Right: newHead = new Point(head.X + 1, head.Y); break;
        }

        if (newHead.X < 0 || newHead.X >= width || newHead.Y < 0 || newHead.Y >= height || snake.Skip(1).Contains(newHead))
        {
            gameOver = true; // Check for collisions
            return;
        }

        snake.Insert(0, newHead);
        if (snake.Count > score + 1)
        {
            snake.RemoveAt(snake.Count - 1); // Remove the tail
        }
    }

    static Point GenerateFood(List<Point> snake)
    {
        var random = new Random();
        Point food;
        do
        {
            food = new Point(random.Next(0, width), random.Next(0, height));
        } while (snake.Contains(food));
        return food;
    }

    static void Draw(List<Point> snake, Point food)
    {
        Console.Clear();
        foreach (var segment in snake)
        {
            // Ensure the segment is within bounds before drawing
            if (segment.X >= 0 && segment.X < width && segment.Y >= 0 && segment.Y < height)
            {
                Console.SetCursorPosition(segment.X, segment.Y);
                Console.Write("O");
            }
        }

        // Ensure the food is within bounds before drawing
        if (food.X >= 0 && food.X < width && food.Y >= 0 && food.Y < height)
        {
            Console.SetCursorPosition(food.X, food.Y);
            Console.Write("X");
        }
    }

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false; // Check for null
            if (obj is not Point point) return false; // Check type

            return X == point.X && Y == point.Y; // Compare values
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
