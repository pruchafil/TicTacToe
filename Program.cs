using System;
using TicTacToe;

const string invalidInput = "Neplatné zadání souřadnic, zadejte prosím platné souřadnice.";
const string inputColumn = "Zadejte sloupec, kam chcete umístit svůj znak: ";
const string inputRow = "Zadejte řádek, kam chcete umístit svůj znak: ";

bool appActive = true, bot = false;

Console.WriteLine("Vypsat minulé hry? (y/_)");

if (Console.ReadKey().Key == ConsoleKey.Y)
{
    Console.Clear();
    SaveSystem.FileHandler.OutputFileToConsole();
}

Console.WriteLine("Bot? (y/_)");

if (Console.ReadKey().Key == ConsoleKey.Y)
    bot = true;

Console.Clear();

do
{
    PlayGame();
} while (appActive);

void PlayGame()
{
    Game gameInfo = new Game();

    // empty field
    for (int y = 0; y < Config.Instance.FieldSize; y++)
        for (int x = 0; x < Config.Instance.FieldSize; x++)
            gameInfo[y, x] = Fields.FieldType.NULL;

    bool firstPlayer = true;
    bool gameActive = true;

    do
    {
        gameInfo.WriteToConsole();
        int arg = firstPlayer ? 1 : 2;
        Console.WriteLine("\nNa řadě je hráč číslo {0}.", arg);
        int y, x;

        bool incorrect = true;
        do
        {
            if (bot && arg == 2)
                Bot.MakeMove(gameInfo);
            else
            {
                var getInput = (string msg) =>
                {
                    int arg;
                    Console.Write(msg);
                    while (!int.TryParse(Console.ReadLine(), out arg))
                    {
                        Console.WriteLine(invalidInput);
                        Console.Write(msg);
                    }
                    return arg;
                };

                x = getInput(inputColumn) - 1;
                y = getInput(inputRow) - 1;

                if
                (
                    y < 0 || y >= Config.Instance.FieldSize ||
                    x < 0 || x >= Config.Instance.FieldSize ||
                    gameInfo[y, x] != Fields.FieldType.NULL
                )
                {
                    Console.WriteLine(invalidInput);
                    continue;
                }

                gameInfo[y, x] = arg == 1 ? Fields.FieldType.X : Fields.FieldType.O;
            }

            incorrect = false;
        } while (incorrect);

        Fields.FieldType f = arg == 1 ? Fields.FieldType.X : Fields.FieldType.O;

        if (gameInfo.CheckWin(f))
        {
            Console.Clear();
            gameInfo.WriteToConsole();
            Console.WriteLine("Hráč {0} vyhrál!", arg);
            SaveSystem.FileHandler.WriteToFile(gameInfo);
            Console.ReadKey();
            Console.Clear();
            gameActive = false;
        }
        else if (gameInfo.IsFull())
        {
            Console.Clear();
            Console.WriteLine("Nikdo nevyhrál!");
            SaveSystem.FileHandler.WriteToFile(gameInfo);
            Console.ReadKey();
            gameActive = false;
        }

        firstPlayer = !firstPlayer;
        Console.Clear();
    } while (gameActive);
}