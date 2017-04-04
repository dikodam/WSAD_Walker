using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Console;

namespace WSAD_Walker
{
    class Program
    {
        public enum Direction
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        };

        static List<Schuss> shotList = new List<Schuss>();
        static int leftPosO = WindowWidth / 2;
        static int topPosO = WindowHeight / 2;

        static void Main(string[] args)
        {
            Direction lastDirection = Direction.UP;
            Stopwatch timer = new Stopwatch();

            SetCursorPosition(leftPosO, topPosO);
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
                                shotList.Add(new Schuss(leftPosO, topPosO, lastDirection));
                                break;
                            // up
                            case ConsoleKey.W:
                                if (topPosO > 0)
                                {
                                    topPosO--;
                                }
                                lastDirection = Direction.UP;
                                break;
                            // down
                            case ConsoleKey.S:
                                if (topPosO < WindowHeight)
                                {
                                    topPosO++;
                                }
                                lastDirection = Direction.DOWN;
                                break;
                            // leftPosO
                            case ConsoleKey.A:
                                if (leftPosO > 0)
                                {
                                    leftPosO--;
                                }
                                lastDirection = Direction.LEFT;
                                break;
                            // right
                            case ConsoleKey.D:
                                if (leftPosO < WindowWidth - 1)
                                {
                                    leftPosO++;
                                }
                                lastDirection = Direction.RIGHT;
                                break;
                        }
                        PrintScreen();
                    }
                }
                MoveShots();
                PrintScreen();
                timer.Restart();
            }
        }

        private static void MoveShots()
        {
            List<Schuss> invalidShots = new List<Schuss>();
            foreach (Schuss s in shotList)
            {
                s.Move();
                if (InvalidPosition(s))
                {
                    invalidShots.Add(s);
                }
            }

            foreach (Schuss invalidShot in invalidShots)
            {
                shotList.Remove(invalidShot);
            }
        }

        private static bool InvalidPosition(Schuss s)
        {
            return s.Left < 0 || s.Left > WindowWidth - 1 || s.Top < 0 || s.Top > WindowHeight - 1;
        }

        private static void PrintScreen()
        {
            Clear();
            foreach (Schuss s in shotList)
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
            SetCursorPosition(leftPosO, topPosO);
            Write("O");
        }
    }
}