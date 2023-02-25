using System.Collections.Generic;

namespace TicTacToe;


public static class Fields
{
    public enum FieldType { X, O, NULL }

    public readonly static Dictionary<FieldType, char> dict = new Dictionary<FieldType, char>()
    {
        { FieldType.X, 'X' },
        { FieldType.O, 'O' },
        { FieldType.NULL, '-' }
    };
}