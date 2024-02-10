using UnityEngine;

public static class Utilities
{
    public static float GetSpreadValue(float value, float spread)
    {
        return Random.Range(
            value - spread,
            value + spread);
    }
    
    public static int GetSpreadValue(int value, int spread)
    {
        return Random.Range(
            value - spread,
            value + spread);
    }
}
