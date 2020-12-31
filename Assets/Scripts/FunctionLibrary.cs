using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    public delegate Vector3 Function(float u, float v, float t);

    public enum FunctionName { Wave, MultiWave, Ripple, Sphere, Torus }

    static readonly Function[] functions = { Wave, MultiWave, Ripple, Sphere, Torus };

    public static Function GetFunction(FunctionName name) => functions[(int)name];

    public static Vector3 Wave(float u, float v, float t) => new Vector3(u, Sin(PI * (u + v + t)), v);

    public static Vector3 MultiWave(float u, float v, float t)
    {
        var y = Sin(PI * (u + 0.5f * t));
        y += Sin(2f * PI * (v + t)) * 0.5f;
        y += Sin(PI * (u + v + 0.25f * t));

        return new Vector3(u, y * (1f / 2.5f), v);
    }

    public static Vector3 Ripple(float u, float v, float t)
    {
        var d = Sqrt(u * u + v * v);

        return new Vector3(u, Sin(4f * PI * d - t) / (1f + 10f * d), v);
    }

    public static Vector3 Sphere(float u, float v, float t)
    {
        var r = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v + t));
        var s = r * Cos(0.5f * PI * v);

        return new Vector3(s * Sin(PI * u), r * Sin(PI * 0.5f * v), s * Cos(PI * u));
    }

    public static Vector3 Torus(float u, float v, float t)
    {
        var r1 = (7f + Sin(PI * (6f * u + 0.5f * t)))/10f;
        var r2 = (3f + Sin(PI * (8f * u + 4f * v + 2f * t)))/20f;
        var s = r1 + r2 * Cos(PI * v);

        return new Vector3(s * Sin(PI * u), r2 * Sin(PI * v), s * Cos(PI * u));
    }
}
