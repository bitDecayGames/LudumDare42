using UnityEngine;

public class VectorMath
{
    public static bool AreVectorsEqual(Vector2 a, Vector2 b){
        return Vector2.SqrMagnitude(a - b) < 0.0001;
    }
}