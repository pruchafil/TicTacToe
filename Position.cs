namespace TicTacToe;

internal static partial class Bot
{
    private struct Position
    {
        public int y, x;

        public Position(int y, int x)
        {
            this.y = y;
            this.x = x;
        }
    }
}