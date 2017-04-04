using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Console;

namespace WSAD_Walker
{
    class Program
    {
        public enum Direction { UP, DOWN, LEFT, RIGHT };

        static  List<Schuss> shots = new List<Schuss>();
        static int left = WindowWidth / 2;
        static int top = WindowHeight / 2;

        static void Main(string[] args)
        {
            Direction lastDirection = Direction.UP;
            Stopwatch timer = new Stopwatch();

            SetCursorPosition(left, top);
            CursorVisible = false;
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.Green;
            Write("O");

            timer.Start();
            ConsoleKey key = ConsoleKey.P;

            while (key != ConsoleKey.Escape)
            {
                while (timer.Elapsed.Milliseconds < 50)
                {
                    while (KeyAvailable)
                    {
                        key = ReadKey(true).Key;
                        switch (key)
                        {
                            // schuss
                            case ConsoleKey.Spacebar:
                                shots.Add(new Schuss(left, top, lastDirection));
                                break;
                            // up
                            case ConsoleKey.W:
                                if (top > 0)
                                {
                                    top--;
                                }
                                lastDirection = Direction.UP;
                                break;
                            // down
                            case ConsoleKey.S:
                                if (top < WindowHeight)
                                {
                                    top++;
                                }
                                lastDirection = Direction.DOWN;
                                break;
                            // left
                            case ConsoleKey.A:
                                if (left > 0)
                                {
                                    left--;
                                }
                                lastDirection = Direction.LEFT;
                                break;
                            // right
                            case ConsoleKey.D:
                                if (left < WindowWidth - 1)
                                {
                                    left++;
                                }
                                lastDirection = Direction.RIGHT;
                                break;

                        }
                        PrintScreen();
                    }
                }
                TickShots();
                PrintScreen();
                timer.Restart();
            }
        }

        private static void TickShots()
        {
            List<Schuss> invalidShots = new List<Schuss>();
            foreach (Schuss s in shots)
            {
                s.Move();
                if (InvalidPosition(s))
                {
                    invalidShots.Add(s);
                }
            }
            foreach (Schuss invalidShot in invalidShots)
            {
                shots.Remove(invalidShot);
            }
        }

        private static bool InvalidPosition(Schuss s)
        {
            return s.Left < 0 || s.Left > WindowWidth - 1 || s.Top < 0 || s.Top > WindowHeight - 1;
        }

        private static void PrintScreen()
        {
            Clear();
            foreach (Schuss s in shots)
            {
                SetCursorPosition(s.Left, s.Top);
                if (s.Direction == Direction.UP || s.Direction == Direction.DOWN)
                {
                    Write("|");
                }
                else
                {
                    Write("-");
                }
            }
            SetCursorPosition(left, top);
            Write("O");
        }
    }
}
