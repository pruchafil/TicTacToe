using System.Collections.Generic;
using System.Linq;

namespace TicTacToe;

internal static partial class Bot
{
    public static void MakeMove(Game gameInfo)
    {
        Position move = GetBestMove(gameInfo);

        gameInfo[move.y, move.x] = Fields.O;
    }

    private static Position GetBestMove(Game gameInfo)
    {
        for (int i = 1; i < 3 && i < Config.Instance.PointsToWin; i++)
        {
            // we win in i
            Position? ind = EdgePositionToComplete(Config.Instance.PointsToWin - i, Fields.O);
            if (ind != null)
                return ind.Value;

            // we win in i
            ind = BetweenPositionToComplete(Config.Instance.PointsToWin - i, Fields.O);
            if (ind != null)
                return ind.Value;

            // oponent wins in i
            ind = EdgePositionToComplete(Config.Instance.PointsToWin - i, Fields.X);
            if (ind != null)
                return ind.Value;

            // oponent wins in i
            ind = BetweenPositionToComplete(Config.Instance.PointsToWin - i, Fields.X);
            if (ind != null)
                return ind.Value;
        }





        // edges
        Position[] positions = new Position[] {
                new Position(0,0),
                new Position(Config.Instance.FieldSize - 1,0),
                new Position(0,Config.Instance.FieldSize - 1),
                new Position(Config.Instance.FieldSize - 1,Config.Instance.FieldSize - 1),
            };

        foreach (Position item in positions)
            if (gameInfo[item.y, item.x] == Fields.NULL)
                return item;

        System.Random rnd = new();

        int y = (Config.Instance.FieldSize - 1) / 2,
            x = y;

        while (gameInfo[y, x] != Fields.NULL)
        {
            y = rnd.Next(0, Config.Instance.FieldSize);
            x = rnd.Next(0, Config.Instance.FieldSize);
        }

        return new Position(y, x);

        Position? EdgePositionToComplete(int row, Fields field)
        {
            //
            // Horizontal
            //

            int included;

            for (int x = 0; x < Config.Instance.FieldSize; x++)
            {
                included = 0;

                for (int y = 0; y < Config.Instance.FieldSize; y++)
                {
                    if (gameInfo[y, x] == field)
                    {
                        included++;
                    }
                    else
                    {
                        included = 0;
                    }

                    if (included == row)
                    {
                        if (y != Config.Instance.FieldSize - 1 && gameInfo[y + 1, x] == Fields.NULL)
                            return new Position(y + 1, x);
                        else if (y >= row && gameInfo[y - row, x] == Fields.NULL)
                            return new Position(y - row, x);
                    }
                }
            }

            //
            // Vertical
            //

            for (int y = 0; y < Config.Instance.FieldSize; y++)
            {
                included = 0;

                for (int x = 0; x < Config.Instance.FieldSize; x++)
                {
                    if (gameInfo[y, x] == field)
                    {
                        included++;
                    }
                    else
                    {
                        included = 0;
                    }

                    if (included == row)
                    {
                        if (x != Config.Instance.FieldSize - 1 && gameInfo[y, x + 1] == Fields.NULL)
                            return new Position(y, x + 1);
                        else if (x >= row && gameInfo[y, x - row] == Fields.NULL)
                            return new Position(y, x - row);
                    }
                }
            }

            //
            // Diagonal right down
            //

            included=0;

            for (int x = 0; x < Config.Instance.FieldSize; x++)
            {
                if (gameInfo[x, x] == field)
                {
                    included++;
                }
                else
                {
                    included = 0;
                }

                if (included == row)
                {
                    if (x != Config.Instance.FieldSize - 1 && gameInfo[x + 1, x + 1] == Fields.NULL)
                        return new Position(x + 1, x + 1);
                    else if (x >= row && gameInfo[x - row, x - row] == Fields.NULL)
                        return new Position(x - row, x - row);
                }
            }

            //
            // Diagonal left down
            //

            included = 0;

            for (int x = Config.Instance.FieldSize - 1; x >= 0; x--)
            {
                if (gameInfo[x, x != 0 ? x % (Config.Instance.FieldSize - 1) : (Config.Instance.FieldSize - 1)] == field)
                {
                    included++;
                }
                else
                {
                    included = 0;
                }

                if (included == row)
                {
                    if (gameInfo[0, Config.Instance.FieldSize - 1] == Fields.NULL)
                        return new Position(0, Config.Instance.FieldSize - 1);
                    else if (gameInfo[Config.Instance.FieldSize - 1, 0] == Fields.NULL)
                        return new Position(Config.Instance.FieldSize - 1, 0);
                }
            }

            return null;
        }

        Position? BetweenPositionToComplete(int row, Fields field)
        {
            //
            // Horizontal
            //

            int included = 0;

            for (int x = 0; x < Config.Instance.FieldSize; x++, included = 0)
            {
                List<int> spaces = new();
                for (int y = 0; y < Config.Instance.FieldSize; y++)
                {
                    if (gameInfo[y, x] == field)
                    {
                        included++;
                    }

                    if (gameInfo[y, x] == Fields.NULL)
                    {
                        spaces.Add(y);
                    }
                }

                if (included == row)
                {
                    foreach (int space in spaces)
                    {
                        int arround = 0;
                        for (int i = space + 1; spaces.Contains(i); i++, arround++) ;
                        for (int i = space - 1; spaces.Contains(i); i--, arround++) ;
                        if (arround == row)
                            return new Position(space, x);
                    }
                }
            }

            //
            // Vertical
            //

            for (int y = 0; y < Config.Instance.FieldSize; y++, included = 0)
            {
                List<int> spaces = new();
                for (int x = 0; x < Config.Instance.FieldSize; x++)
                {
                    if (gameInfo[y, x] == field)
                    {
                        included++;
                    }

                    if (gameInfo[y, x] == Fields.NULL)
                    {
                        spaces.Add(x);
                    }
                }

                if (included == row)
                {
                    foreach (int space in spaces)
                    {
                        int arround = 0;
                        for (int i = space + 1; spaces.Contains(i); i++, arround++) ;
                        for (int i = space - 1; spaces.Contains(i); i--, arround++) ;
                        if (arround == row)
                            return new Position(y, space);
                    }
                }
            }
            //
            // Diagonal right down
            //

            included = 0;

            {
                List<int> spaces = new();
                for (int x = 0; x < Config.Instance.FieldSize; x++)
                {
                    if (gameInfo[x, x] == field)
                    {
                        included++;
                    }

                    if (gameInfo[x, x] == Fields.NULL)
                    {
                        spaces.Add(x);
                    }
                }

                if (included == row)
                {
                    foreach (int space in spaces)
                    {
                        int arround = 0;
                        for (int i = space + 1; spaces.Contains(i); i++, arround++) ;
                        for (int i = space - 1; spaces.Contains(i); i--, arround++) ;
                        if (arround == row)
                            return new Position(space, space);
                    }
                }
            }

            //
            // Diagonal left down
            //

            included = 0;

            {
                List<int> spaces = new();
                for (int x = Config.Instance.FieldSize - 1; x >= 0; x--)
                {
                    if (gameInfo[x % (Config.Instance.FieldSize - 1), x] == field)
                    {
                        included++;
                    }

                    if (gameInfo[x % (Config.Instance.FieldSize - 1), x] == Fields.NULL)
                    {
                        spaces.Add(x);
                    }
                }

                if (included == row)
                {
                    foreach (int space in spaces)
                    {
                        int arround = 0;
                        for (int i = space + 1; spaces.Contains(i); i++, arround++) ;
                        for (int i = space - 1; spaces.Contains(i); i--, arround++) ;
                        if (arround == row)
                            return new Position(space % (Config.Instance.FieldSize - 1), space);
                    }
                }
            }

            return null;
        }
    }
}