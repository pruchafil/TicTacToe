﻿using System;

namespace TicTacToe;

public class Game
{
    private readonly Fields[,] _field = new Fields[Config.Instance.FieldSize, Config.Instance.FieldSize];

    public Fields this[int y, int x]
    {
        get
        {
            if (y >= Config.Instance.FieldSize || y < 0)
                throw new System.IndexOutOfRangeException($"min {nameof(y)} is {0}, max is {Config.Instance.FieldSize - 1}");
            if (x >= Config.Instance.FieldSize || x < 0)
                throw new System.IndexOutOfRangeException($"min {nameof(x)} is {0}, max is {Config.Instance.FieldSize - 1}");
            return _field[y, x];
        }
        set
        {
            if (y >= Config.Instance.FieldSize || y < 0)
                throw new System.IndexOutOfRangeException($"min {nameof(y)} is {0}, max is {Config.Instance.FieldSize - 1}");
            if (x >= Config.Instance.FieldSize || x < 0)
                throw new System.IndexOutOfRangeException($"min {nameof(x)} is {0}, max is {Config.Instance.FieldSize - 1}");

            _field[y, x] = value;
        }
    }

    internal void WriteToConsole()
    {
        for (int i = 0; i < 4; i++)
        {
            Console.Write(' ');
        }
        for (int i = 0; i < Config.Instance.FieldSize; i++)
        {
            Console.Write($"|{{0,{(i + 1).ToString().Length + 1}}} ", i + 1);
        }
        Console.WriteLine('|');

        for (int y = 0; y < Config.Instance.FieldSize; y++)
        {
            for (int i = 0; i < 4; i++)
            {
                Console.Write(' ');
            }
            for (int i = 0; i < Config.Instance.FieldSize * 4 + 1; i++)
            {
                Console.Write('-');
            }
            Console.WriteLine();

            Console.Write("{0,3} |", y + 1);
            for (int x = 0; x < Config.Instance.FieldSize; x++)
            {
                switch (_field[y, x])
                {
                    default:
                        Console.Write(" - ");
                        break;

                    case Fields.X:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(" X ");
                        break;

                    case Fields.O:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" O ");
                        break;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write('|');
            }

            Console.WriteLine();
        }
    }

    internal bool IsFull()
    {
        foreach (Fields item in _field)
            if (item == Fields.NULL)
                return false;

        return true;
    }

    internal bool CheckWin(Fields f)
    {
        for (int y = 0, index = 0; y < Config.Instance.FieldSize; y++, index = 0)
        {
            for (int x = 0; x < Config.Instance.FieldSize; x++)
                if (_field[x, y] == f)
                    index++;

            if (index == Config.Instance.PointsToWin)
                return true;
        }

        for (int x = 0, index = 0; x < Config.Instance.FieldSize; x++, index = 0)
        {
            for (int y = 0; y < Config.Instance.FieldSize; y++)
                if (_field[x, y] == f)
                    index++;

            if (index == Config.Instance.PointsToWin)
                return true;
        }

        for (int x = 0, index = 0; x < Config.Instance.FieldSize; x++)
        {
            if (_field[x, x] == f)
                index++;
            if (index == Config.Instance.PointsToWin)
                return true;
        }

        for (int x = Config.Instance.FieldSize - 1, index = 0; x >= 0; x--)
        {
            int ind;
            if (x == 0)
                ind = (Config.Instance.FieldSize - 1);
            else
                ind = x % (Config.Instance.FieldSize - 1);

            if (_field[x, ind] == f)
                index++;

            if (index == Config.Instance.PointsToWin)
                return true;
        }

        return false;
    }
}