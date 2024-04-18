using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitiveContainer
{
    // Constructor
    public PrimitiveContainer(int type = 0, float rotation = 0f, float length = 0.0f, bool concaveUp = false,
                              float completeness = 0.0f)
    {
        Type = type;
        Rotation = rotation;
        Length = length;
        ConcaveUp = concaveUp;
        Completeness = completeness;
    }

    // General
    public int Type;       // 0 = Line, 1 = Arc
    public float Rotation; // Rotation of the primitive
    public float Length;   // Length of the primitive

    // Arc/Circle Specific
    public bool ConcaveUp;     // Whether the arc is concave up or down
    public float Completeness; // How much of the circle is drawn
}
