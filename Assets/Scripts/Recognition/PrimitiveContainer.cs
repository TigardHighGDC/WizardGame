using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitiveContainer
{
    // General
    public int Type = 0;          // 0 = Line, 1 = Arc
    public float Rotation = 0.0f; // Rotation of the primitive
    public float Length = 0.0f;   // Length of the primitive

    // Arc/Circle Specific
    public bool ConcaveUp = false;    // Whether the arc is concave up or down
    public float Completeness = 0.0f; // How much of the circle is drawn
}
