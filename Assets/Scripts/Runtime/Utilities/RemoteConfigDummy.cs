using System.Collections.Generic;
using System.Linq;

public static class RemoteConfigDummy
{
    public static List<int> levels = new List<int> { 0, 1, 2, 3, 4 };
    public static List<int> timers = new List<int> { 60, 80, 90, 100, 120 };
    public static List<int> moves = new List<int> { 35, 32, 35, 40, 45 };

    public const int LevelLoopStart = 5;
    public const int DefaultTimer = 60;
    public const int defaultMoveCounter = 60;
    
    public static bool hasTimer = true;
    public static bool hasMoveCounter = true;


    public static void RemoveLevel(int level)
    {
        int index = levels.IndexOf(level);
        if (index != -1)
        {
            levels.RemoveAt(index);
            timers.RemoveAt(index);
            moves.RemoveAt(index);
        }
    }
}