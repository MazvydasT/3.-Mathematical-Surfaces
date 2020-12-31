using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    [SerializeField]
    FunctionLibrary.FunctionName function = default;

    Transform[] points;

    private void Awake()
    {
        var step = 2f / resolution;
        var scale = Vector3.one * step;

        points = new Transform[resolution * resolution];

        for (int i = 0; i < points.Length; ++i)
        {
            var point = Instantiate(pointPrefab);
            point.localScale = scale;

            point.SetParent(transform, false);

            points[i] = point;
        }
    }

    void Update()
    {
        var f = FunctionLibrary.GetFunction(function);

        var time = Time.time;
        var step = 2f / resolution;

        var v = 0.5f * step - 1f;

        for (int i = 0,x = 0, z = 0; i < points.Length; ++i, ++x)
        {
            if (x == resolution)
            {
                x = 0;
                ++z;
                v = (z + 0.5f) * step - 1f;
            }

            var u = (x + 0.5f) * step - 1f;

            points[i].localPosition = f(u, v, time);
        }
    }
}
