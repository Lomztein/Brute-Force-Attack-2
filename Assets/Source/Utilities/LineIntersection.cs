// C# program to check if two given line segments intersect
using UnityEngine;

public class LineIntersection
{
    public static bool LinesIntersect (Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, float endMargin)
    {
        // Get the segments' parameters.
        float dx12 = p2.x - p1.x;
        float dy12 = p2.y - p1.y;
        float dx34 = p4.x - p3.x;
        float dy34 = p4.y - p3.y;

        // Solve for t1 and t2
        float denominator = (dy12 * dx34 - dx12 * dy34);

        float t1 =
            ((p1.x - p3.x) * dy34 + (p3.y - p1.y) * dx34)
                / denominator;
        if (float.IsInfinity(t1))
        {
            // The lines are parallel (or close enough to it).
            return false;
        }

        float t2 =
            ((p3.x - p1.x) * dy12 + (p1.y - p3.y) * dx12)
                / -denominator;

        // The segments intersect if t1 and t2 are between 0 and 1.
        return 
            ((t1 >= endMargin) && (t1 <= 1 - endMargin) &&
             (t2 >= endMargin) && (t2 <= 1 - endMargin));
    }
}

/* This code shamelessly stolen from http://csharphelper.com/blog/2014/08/determine-where-two-lines-intersect-in-c/ */
/* Authored by Rod Stephens */