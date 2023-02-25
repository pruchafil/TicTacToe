using SaveSystem;

namespace TicTacToe;

public class Config
{
    static Config()
    {
        if (Instance == null)
        {
            Instance = FileHandler.LoadConfig();
        }
    }

    public static Config Instance { get; } // singleton

    public int FieldSize { get; set; }
    public int PointsToWin { get; set; }
}