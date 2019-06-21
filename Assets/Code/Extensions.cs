using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Extensions
{
    public static Vector2 ToUnity(this DevMath.Vector2 v)
    {
        return new Vector2(v.x, v.y);
    }

    public static DevMath.Vector2 ToDevMath(this Vector2 v)
    {
        return new DevMath.Vector2(v.x, v.y);
    }

    public static Vector3 ToUnity(this DevMath.Vector3 v)
    {
        return new Vector3(v.x, v.y, v.z);
    }

    public static DevMath.Vector3 ToDevMath(this Vector3 v)
    {
        return new DevMath.Vector3(v.x, v.y, v.z);
    }

    //public static Matrix4x4 ToUnity(this DevMath.Matrix4x4 m)
    //{
    //    return Matrix4x4(m);
    //}

    //public static DevMath.Matrix4x4 ToDevMath(this Matrix4x4 m)
    //{
    //    return DevMath.Matrix4x4(m);
    //}
}
