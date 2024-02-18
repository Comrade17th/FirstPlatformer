using System.Collections.Generic;
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

    public static Transform GetClosestTransform(Transform source, List<Transform> objects)
    {
        Transform result = null;
        Vector3 currentPosition = source.position;
        float minDistance = Mathf.Infinity;
        
        foreach (Transform transform in objects)
        {
            float distance = Vector3.Distance(transform.position, currentPosition);
            
            if (distance < minDistance)
            {
                result = transform;
                minDistance = distance;
            }
        }
        
        return result;
    }
}
