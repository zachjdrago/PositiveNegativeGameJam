using System.Collections.Generic;

public static class DataManagement
{
    public static float InvertRatio(float value)
    {
        return 1 - value;
    }

    public static int FromBack(List<Wormhole> list, int index)
    {
        if (index <= list.Count) return list.Count - index;
        else return 0;
    }
}