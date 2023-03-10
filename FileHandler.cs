using System;
using System.IO;
using System.Text.Json;
using TicTacToe;

namespace SaveSystem;

public static class FileHandler
{
    const string games = "games.ttc";
    const string config = "config.json";

    public static void OutputFileToConsole()
    {
        using StreamReader sr = new(new FileStream(games, FileMode.OpenOrCreate));

        while (!sr.EndOfStream)
            Console.WriteLine(sr.ReadLine());
    }

    public static void WriteToFile(Game gameInfo)
    {
        using StreamWriter sw = new(new FileStream(games, FileMode.Append));

        for (int y = 0; y < Config.Instance.FieldSize; y++)
        {
            for (int x = 0; x < Config.Instance.FieldSize; x++)
                Console.Write("{0} ", Fields.dict[gameInfo[y, x]]);

            sw.WriteLine();
        }

        sw.WriteLine();
    }

    public static Config LoadConfig()
    {
        return JsonSerializer.Deserialize<Config>(File.ReadAllText(config));
    }
}