using System.IO;
using System.Text.Json;
using TicTacToe;

namespace SaveSystem;

public static class FileHandler
{
    public static void OutputFileToConsole()
    {
        using System.IO.StreamReader sr = new(
            new System.IO.FileStream("games.ttc", System.IO.FileMode.OpenOrCreate)
        );

        while (!sr.EndOfStream)
            System.Console.WriteLine(sr.ReadLine());
    }

    public static void WriteToFile(TicTacToe.Game gameInfo)
    {
        using System.IO.StreamWriter sw = new(
            new System.IO.FileStream("games.ttc", System.IO.FileMode.Append)
        );

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
                switch (gameInfo[x, y])
                {
                    case TicTacToe.Fields.NULL:
                        sw.Write("- ");
                        break;

                    case TicTacToe.Fields.X:
                        sw.Write("X ");
                        break;

                    case TicTacToe.Fields.O:
                        sw.Write("O ");
                        break;
                }

            sw.WriteLine();
        }

        sw.WriteLine();
    }

    public static Config LoadConfig()
    {
        return JsonSerializer.Deserialize<Config>(File.ReadAllText("config.json"));
    }
}