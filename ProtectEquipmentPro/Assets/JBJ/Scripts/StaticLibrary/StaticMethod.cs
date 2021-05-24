using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticMethod
{
    public static Vector3 ModifiedX(this Vector3 v, float modifiedX)
    {
        return new Vector3(modifiedX,v.y,v.z);
    }

    public static Vector3 ModifiedY(this Vector3 v, float modifiedY)
    {
        return new Vector3(v.x, modifiedY, v.z);
    }

    public static Vector3 ModifiedZ(this Vector3 v, float modifiedZ)
    {
        return new Vector3(v.x, v.y, modifiedZ);
    }

    public static Vector2 ModifiedX(this Vector2 v, float ModifiedX)
    {
        return new Vector2(ModifiedX, v.y);
    }

    public static Vector2 ModifiedY(this Vector2 v, float ModifiedY)
    {
        return new Vector2(v.x, ModifiedY);
    }
}
